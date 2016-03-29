using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using XinemaActual.Models;

namespace XinemaActual.DataScrapingLogic
{
    internal class GoogleMoviesScraper
    {
        private bool isComplete;
        private string targetURL;
        private List<Cinema> allCinemas;
        private List<Movie> allMovies;
        private List<ShowTime> allShowTime;
        private List<string> uniqueMovies;
        private IWebDriver driver;
        public string TargetURL
        {
            get
            {
                return targetURL != null ? targetURL : null;
            }

            set
            {
                targetURL = value;
            }
        }

        public GoogleMoviesScraper()
        {
            allCinemas = new List<Cinema>();
            allMovies = new List<Movie>();
            allShowTime = new List<ShowTime>();
            uniqueMovies = new List<string>();
            driver = new PhantomJSDriver();
            isComplete = false;
        }

        public void scrapCinemaInfo()
        {

            string url = TargetURL;
            if (targetURL == null)
            {
                Console.WriteLine("No supplied URL");
                return;
            }
            driver.Navigate().GoToUrl(TargetURL);


            //scrapAllPageCinema(driver);
            scrapOnePageCinema(driver);
            isComplete = true;
            



        }

        private void scrapAllPageCinema(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(TargetURL);
            for (;;)
            {
                //add current page cinemas 
                var cinemasname = ScrapCinemaAndMovies(driver);

                //add all
                allCinemas.AddRange(cinemasname);

                ////go goto next on current page
                try

                {

                    var nexturl = driver.FindElements(By.PartialLinkText("Next")).Last().GetAttribute("href");
                    driver.Navigate().GoToUrl(nexturl);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Out of pages " + e.Source);
                    break;
                }

            }
        }

        private void scrapOnePageCinema(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(TargetURL);
            for (;;)
            {
                //add current page cinemas 
                var cinemasName = ScrapCinemaAndMovies(driver);

                //add all
                allCinemas.AddRange(cinemasName);
                break;

            }
        }


        private List<Cinema> ScrapCinemaAndMovies(IWebDriver driver)
        {
            string[] separators = { "\r", "\n" };
            DateTime thisDay = DateTime.Today;
            var cinemaList = new List<Cinema>();
            var showtimeList = new List<ShowTime>();
            var theaters = driver.FindElements(By.CssSelector(".theater"));

            foreach (var theater in theaters)
            {
                var currentCinemaName = theater.FindElement(By.CssSelector("a[id^='link_1_theater_']")).Text;
                var currentCinemaAddress = theater.FindElement(By.CssSelector(".info")).Text;
                var currentCinemaListofMovies = theater.FindElements(By.CssSelector("div[class^='show'] > div[class='movie']"));

                var movieList = new List<Movie>();
                showtimeList.Clear();//clear list before adding again
                foreach (var movie in currentCinemaListofMovies)
                {

                    Movie currMov = new Movie();
                    ShowTime showtime = new ShowTime();

                    //movie details
                    currMov.movieTitle = movie.FindElement(By.CssSelector(".name")).Text;
                    var movieInfo = movie.FindElement(By.CssSelector(".info"));

                    //if movie is not parseable skip this movie loop
                    if (!IsValidMovieInfo(movieInfo.Text))
                    {
                        break;
                    }
                    try
                    {
                        var link = movieInfo.FindElement(By.LinkText("Trailer"));
                        currMov.movieTrailerURL = link.GetAttribute("href");
                    }
                    catch (NoSuchElementException e)
                    {
                        Console.WriteLine("No trailer link found for " + currMov.movieTitle);
                        currMov.movieTrailerURL = null;
                    }
    
                    currMov.movieRunningTime = spiltMovieInfo(movieInfo.Text, 0);
                    currMov.movieRating = spiltMovieInfo(movieInfo.Text, 1);
                    currMov.movieGenre = spiltMovieInfo(movieInfo.Text, 2);
                    currMov.movieLanguage = spiltMovieInfo(movieInfo.Text, 3);
                    //Console.WriteLine("Movie scrapped : " + currMov.ToString());
                    showtime.showtimeStartTime = movie.FindElement(By.CssSelector(".times")).Text;
                    showtime.showtimeDate = thisDay.ToString("d/MM/yyyy");
                    showtime.showtimeTitle = currMov.movieTitle;
                    showtime.showtimeCinemaName = currentCinemaName;

                    movieList.Add(currMov);
                    showtimeList.Add(showtime);
                    if (!uniqueMovies.Contains(currMov.movieTitle))
                    {
                        uniqueMovies.Add(currMov.movieTitle);
                    }

                }
                //accumulate all results
                allShowTime.AddRange(showtimeList);

                allMovies.AddRange(movieList);
                cinemaList.Add(new Cinema() { cinemaName = currentCinemaName, cinemaAddress = currentCinemaAddress});
            }

            return cinemaList;
        }

        public List<ShowTime> getShowtimes()
        {

            return allShowTime;
        }
        public List<Movie> getMovies()
        {

            return allMovies;
        }

        public List<string> getUniqueMovieNames()
        {
            return uniqueMovies;
        }

        public List<Cinema> getCinemas()
        {

            return allCinemas;
        }

        //use offset to get which part of the spiltted string.
        public string spiltMovieInfo(string str, int offset)
        {

            string[] spiltted = str.Split('-');
            return spiltted[offset];
        }

        public bool IsValidMovieInfo(string str)
        {
            if (str.Split('-').Length < 4)
            {
                return false;//movie info part less than 4 return -1 to skip this
            }
            else
            {
                return true;//success
            }
        }

        public bool getIsComplete()
        {
            return isComplete;
        }
    }
}



