using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XinemaActual.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using Abot.Crawler;
using Abot.Poco;

namespace XinemaActual.DAL
{
    public class CinemaGateway : DataGateway<Cinema>
    {
        private List<SelectListItem> CinemaOrderByDropDownItems;

        public CinemaGateway()
        {

            CinemaOrderByDropDownItems = new List<SelectListItem>();
            var firstItem = new SelectListItem { Text = "All", Value = "0" };
            var secondItem = new SelectListItem { Text = "Shaw", Value = "1" };
            var thirdItem = new SelectListItem { Text = "Golden Village", Value = "2" };
            var fourthItem = new SelectListItem { Text = "Cathay", Value = "3" };
            CinemaOrderByDropDownItems.Add(firstItem);
            CinemaOrderByDropDownItems.Add(secondItem);
            CinemaOrderByDropDownItems.Add(thirdItem);
            CinemaOrderByDropDownItems.Add(fourthItem);


        }
        public List<SelectListItem> GetCinemaOrderByNames()
        {
            return CinemaOrderByDropDownItems;
        }
        public IEnumerable<Cinema> SortBy(int id)
        {
            var cinemas = from t in SelectAllCinemas()
                          select t;
            switch (id)
            {

                case 1:

                    cinemas = from t in SelectAllCinemas()
                              where t.cinemaName.Contains("Shaw") || t.cinemaAddress.Contains("Shaw")
                              select t;
                    return cinemas;
                case 2:
                    cinemas = from t in SelectAllCinemas()
                              where t.cinemaName.Contains("Golden Village") || t.cinemaAddress.Contains("Golden Village")
                              select t;
                    return cinemas;

                case 3:
                    cinemas = from t in SelectAllCinemas()
                              where t.cinemaName.Contains("Cathay") || t.cinemaAddress.Contains("Cathay")
                              select t;
                    return cinemas;



            }
            //MovieGateway showtime = new MovieGateway();
            //var test = showtime.SelectAll();

            //var viewModels = (from m in cinemas
            //                  join r in test on m.cinemaID equals r.movieID
            //                  select new ShowtimeVM()
            //                  {
            //                      movieTitle = r.movieTitle;
                            
            //                  }).ToList();
            return cinemas;
        }
        public IEnumerable<Cinema> SelectAllCinemas()
        {
            IEnumerable<Cinema> groupedCinemas = data.GroupBy(t => t.cinemaName).Select(t => t.FirstOrDefault());
            return groupedCinemas;

        }

        public List<Cinema> GetExternalCinemasList(string googleURL) {
            List<Cinema> allCinemas = new List<Cinema>();
            IWebDriver driver = new PhantomJSDriver();
            var url = googleURL;

            driver.Navigate().GoToUrl(url);

            for (int i = 0; i < 5; i++)
            {
                //add current page cinemas 
                var cinemasName = scrapOnePageCinema(driver);

                //add all
                allCinemas.AddRange(cinemasName);

                //Go goto next on current page
                try
                {
                    var nextUrl = driver.FindElements(By.PartialLinkText("Next")).Last().GetAttribute("href");
                    driver.Navigate().GoToUrl(nextUrl);
                }
                catch (InvalidOperationException e)
                {
                    //Console.WriteLine(e.Source);
                    System.Diagnostics.Debug.WriteLine("Cinema scraping exception: "+e.Source);
                }

            }

            //close driver
            driver.Dispose();

            return allCinemas;

        }

        private List<Cinema> scrapOnePageCinema(IWebDriver driver)
        {
            var cinemaList = new List<Cinema>();
            var cinemas = driver.FindElements(By.CssSelector(".theater"));

            foreach (var cinema in cinemas)
            {
                var currCinemaName = cinema.FindElement(By.CssSelector("a[id^='link_1_theater_']")).Text;
                var currCinemaAddress = cinema.FindElement(By.CssSelector(".info")).Text;

                cinemaList.Add(new Cinema() { cinemaName = currCinemaName, cinemaAddress = currCinemaAddress });
            }

            return cinemaList;
        }


    }
}