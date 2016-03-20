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

            var IMDBChart = new Chart(width: 600, height: 400)
                .AddTitle("Top Grossing IMDB Box Office")
                .AddSeries("Default", chartType: "Pie",
                    xValue: new[] { "Zootopia", "10 Cloverfield Land", "Deadpool"},
                    yValues: new[] { "144","24.7","328.2" });
            IMDBChart.Save("~/Content/IMDBChart.jpg", "jpeg");

            var RTChart = new Chart(width: 600, height: 400)
                .AddTitle("Top Grossing Rotten Tomatoes Box Office")
                .AddSeries("Default", chartType: "Pie",
                    xValue: new[] { "Zootopia", "10 Cloverfield Land", "Deadpool" },
                    yValues: new[] { "51.3", "24.7", "10.9" });
            RTChart.Save("~/Content/RTChart.jpg", "jpeg");

            return View(movieReviewGateway.SelectAll());

        }
        public ActionResult Details(int? id)
        {

            var barChart = new Chart(width: 600, height: 400)
             .AddTitle("Movie Review Rating for " + movieReviewGateway.SelectById(id).movieReviewName)
             .AddSeries(
                 name: "Movie",
                 xValue: new[] { "IMDB", "Rotten Tomatoes","Overall" },
                 yValues: new[] { movieReviewGateway.SelectById(id).movieReviewIMDB, movieReviewGateway.SelectById(id).movieReviewRottenTomato,
               ((double.Parse(movieReviewGateway.SelectById(id).movieReviewIMDB) + double.Parse(movieReviewGateway.SelectById(id).movieReviewRottenTomato))/2).ToString() });
            barChart.Save("~/Content/barChart.jpg", "jpeg");
            // Return the contents of the Stream to the client
            return View(movieReviewGateway.SelectById(id));
        }
        public ActionResult barChart()
        {
            return base.File("~/Content/barChart.jpg","jpeg");

        }
        public ActionResult IMDBChart()
        {
            return base.File("~/Content/IMDBChart.jpg", "jpeg");

        }
        public ActionResult RTChart()
        {
            return base.File("~/Content/RTChart.jpg", "jpeg");

        }

    }
}