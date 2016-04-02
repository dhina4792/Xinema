using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinemaActual.DAL;
using XinemaActual.Models;
using XinemaActual.DataScrapingLogic;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace XinemaActual.DataScrapingJobs
{
    public class CinemaJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            System.Diagnostics.Debug.WriteLine("Executing cinema job...");
            CinemaGateway cinemaGateway = new CinemaGateway();
            ShowtimeGateway showtimeGateway = new ShowtimeGateway();
            MovieGateway movieGateway = new MovieGateway();
            MovieReviewGateway movieReviewGateway = new MovieReviewGateway();
            GoogleMoviesScraper gms = new GoogleMoviesScraper();
            gms.TargetURL = "https://www.google.com/movies?near=singapore&rl=1&stok=ABAPP2tdNR_5cLRa-6emW2UtecEL44SX2A%3A1456036737594";
            // Scrap new data
            gms.scrapCinemaInfo();
            List<Cinema> cinemaList = gms.getCinemas();
            List<ShowTime> showTimeList = gms.getShowtimes();
            List<Movie> moviesList = gms.getMovies();
            System.Diagnostics.Debug.WriteLine("cinemaList.Count() " + cinemaList.Count());
            // Check if cinemaList is done

            System.Threading.SpinWait.SpinUntil(() => gms.getIsComplete() == true);
            //Clear existing database first
            cinemaGateway.DeleteRange(cinemaGateway.SelectAllCinemas());
            movieGateway.DeleteRange(movieGateway.SelectAll());
            movieReviewGateway.DeleteRange(movieReviewGateway.SelectAll());
            System.Diagnostics.Debug.WriteLine("Deleted database");
            int cinemaIndex = cinemaList.Count() - 1;
            int showTimeIndex = showTimeList.Count() - 1;
            int movieIndex = moviesList.Count() - 1;
            Cinema cinema = new Cinema();
            ShowTime showTime = new ShowTime();
            Movie movie = new Movie();
            MovieReview movieReview = new MovieReview();
            // insert new data
            while (cinemaIndex >= 0)
            {
                System.Diagnostics.Debug.WriteLine("Index: " + cinemaIndex);
                cinema.cinemaName = cinemaList[cinemaIndex].cinemaName;
                System.Diagnostics.Debug.WriteLine("Cinena Name: " + cinema.cinemaName);
                cinema.cinemaAddress = cinemaList[cinemaIndex].cinemaAddress;
                System.Diagnostics.Debug.WriteLine("Cinema Address: " + cinema.cinemaAddress);
                cinemaGateway.Insert(cinema);
                cinemaIndex--;
            }

            while (showTimeIndex >= 0)
            {
                showTime.showtimeStartTime = showTimeList[showTimeIndex].showtimeStartTime;
                System.Diagnostics.Debug.WriteLine("ShowtimeStartTime: " + showTimeList[showTimeIndex].showtimeStartTime);
                DateTime myDate = DateTime.Parse(showTimeList[showTimeIndex].showtimeDate);
                showTime.showtimeDate = myDate.ToString("MM/dd/yyyy");
                showTime.showtimeTitle = showTimeList[showTimeIndex].showtimeTitle;
                showTime.showtimeCinemaName = showTimeList[showTimeIndex].showtimeCinemaName;
                showtimeGateway.Insert(showTime);
                showTimeIndex--;
            }

            while (movieIndex >= 0)
            {
                string omdbQuery = moviesList[movieIndex].movieTitle;
                omdbQuery = "http://www.omdbapi.com/?t=" + omdbQuery + "&plot=short&r=json&tomatoes=true";
                var request = WebRequest.Create(omdbQuery);
                request.ContentType = "application/json; charset=utf-8";
                string jsonString;
                System.Threading.Thread.Sleep(2000);
                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonString = sr.ReadToEnd();
                }
                System.Diagnostics.Debug.WriteLine(jsonString);

                dynamic movieReviewJson = JObject.Parse(jsonString);
                movieReview.movieReviewName = moviesList[movieIndex].movieTitle;
                movieReview.movieReviewIMDB = movieReviewJson.imdbRating;
                movieReview.movieReviewRottenTomato = movieReviewJson.tomatoRating;
                
                movieReviewGateway.Insert(movieReview);

                movie.moviePoster = movieReviewJson.Poster;
                movie.moviePlot = movieReviewJson.Plot;
                movie.movieActors = movieReviewJson.Actors;
                movie.movieDirector = movieReviewJson.Director;
                movie.movieWebsiteURL = movieReviewJson.Website;
                movie.movieIMDBRating = movieReviewJson.imdbRating;
                movie.movieTomatoesRating = movieReviewJson.tomatoRating;
                movie.movieTitle = moviesList[movieIndex].movieTitle;
                movie.movieGenre = moviesList[movieIndex].movieGenre;
                movie.movieRating = moviesList[movieIndex].movieRating;
                movie.movieTrailerURL  = moviesList[movieIndex].movieTrailerURL;
                System.Diagnostics.Debug.WriteLine("movie url"+moviesList[movieIndex].movieTrailerURL);
                movie.movieRunningTime = moviesList[movieIndex].movieRunningTime;
                movie.movieLanguage = moviesList[movieIndex].movieLanguage;

                movieGateway.Insert(movie);
                movieIndex--;
            }



            System.Diagnostics.Debug.WriteLine("Cinema job ended... ");
        }
    }
}

