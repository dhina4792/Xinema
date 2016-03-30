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

            //var IMDBChart = new Chart(width: 600, height: 400)
            //    .AddTitle("Top Grossing IMDB Box Office")
            //    .AddSeries("Default", chartType: "Pie",
            //        xValue: new[] { "Zootopia", "10 Cloverfield Land", "Deadpool" },
            //        yValues: new[] { "144", "24.7", "328.2" });
            //IMDBChart.Save("~/Content/IMDBChart.jpg", "jpeg");

            //var RTChart = new Chart(width: 600, height: 400)
            //    .AddTitle("Top Grossing Rotten Tomatoes Box Office")
            //    .AddSeries("Default", chartType: "Pie",
            //        xValue: new[] { "Zootopia", "10 Cloverfield Land", "Deadpool" },
            //        yValues: new[] { "51.3", "24.7", "10.9" });
            //RTChart.Save("~/Content/RTChart.jpg", "jpeg");
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
            string imdb = movieReviewGateway.SelectById(id).movieReviewIMDB;
            string rotten = movieReviewGateway.SelectById(id).movieReviewRottenTomato;

            if ((imdb != null) || (rotten != null))
            {
                if (!(imdb.Contains("N/A")) || (!rotten.Contains("N/A")))
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