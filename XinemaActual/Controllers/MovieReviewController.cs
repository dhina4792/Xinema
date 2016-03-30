using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XinemaActual.DAL;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Data;

namespace XinemaActual.Controllers
{
    public class MovieReviewController : Controller
    {
        private MovieReviewGateway movieReviewGateway;

        // GET: Showtimes
        public MovieReviewController()
        {
            movieReviewGateway = new MovieReviewGateway();
        }
        public ActionResult Index()
        {
            var barChart = new Chart(width: 600, height: 400)
                     .AddTitle("Average Genre Chart")
                     .AddSeries(
                         name: "Movie",
                         xValue: new[] { "IMDB", "Rotten Tomatoes", "Overall" },
                         yValues: new[] { 1,2,3 });
            barChart.Save("~/Content/genreChart.jpg", "jpeg");

            return View(movieReviewGateway.SelectAllMoviesReviews());

        }
        public ActionResult Details(int? id)
        {
            string imdb;
            string rotten;
            if (movieReviewGateway.SelectById(id).movieReviewIMDB != null) { 
                if (movieReviewGateway.SelectById(id).movieReviewIMDB != "N/A") {
                    imdb = movieReviewGateway.SelectById(id).movieReviewIMDB;
                }
                else
                {
                    imdb = null;
                }
            }else
            {
                imdb = null;

            }
            if (movieReviewGateway.SelectById(id).movieReviewRottenTomato != null)
            {
                if (movieReviewGateway.SelectById(id).movieReviewRottenTomato != "N/A")
                {
                    rotten = movieReviewGateway.SelectById(id).movieReviewRottenTomato;
                }
                else
                {
                    rotten = null;
                }
            }
            else
            {
                rotten = null;

            }

            if ((imdb != null) && (rotten != null))
            {
                    var barChart = new Chart(width: 600, height: 400)
                     .AddTitle("Movie Review Rating for " + movieReviewGateway.SelectById(id).movieReviewName)
                     .AddSeries(
                         name: "Movie",
                         xValue: new[] { "IMDB", "Rotten Tomatoes", "Overall" },
                         yValues: new[] { movieReviewGateway.SelectById(id).movieReviewIMDB, movieReviewGateway.SelectById(id).movieReviewRottenTomato,
                        ((double.Parse(movieReviewGateway.SelectById(id).movieReviewIMDB) + double.Parse(movieReviewGateway.SelectById(id).movieReviewRottenTomato))/2).ToString() });
                    barChart.Save("~/Content/barChart.jpg", "jpeg");
                    ViewBag.message = "";
            }
            else
            {
                string imageFilePath = Server.MapPath(@"~/Content/barChart.jpg");
                System.IO.File.Delete(imageFilePath);
                ViewBag.message = "Ratings have not been given for this movie yet";
            }            // Return the contents of the Stream to the client

            return View(movieReviewGateway.SelectById(id));

        }
        public ActionResult barChart()
        {
            return base.File("~/Content/barChart.jpg", "jpeg");

        }
        public ActionResult genreChart()
        {
            return base.File("~/Content/genreChart.jpg", "jpeg");

        }

    }
}   