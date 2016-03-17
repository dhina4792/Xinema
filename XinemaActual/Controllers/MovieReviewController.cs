using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XinemaActual.DAL;
using System.Web.Mvc;
using System.Web.Helpers;

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
            return View(movieReviewGateway.SelectAll());

        }
        public ActionResult Details(int? id)
        {

            var chart = new Chart(width: 600, height: 400)
             .AddTitle("Movie Review Rating for " + movieReviewGateway.SelectById(id).movieReviewName)
             .AddSeries(
                 name: "Movie",
                 xValue: new[] { "IMDB", "Rotten Tomatoes" },
                 yValues: new[] { movieReviewGateway.SelectById(id).movieReviewIMDB, movieReviewGateway.SelectById(id).movieReviewRottenTomato });
            chart.Save("~/Content/chart.jpg", "jpeg");
            // Return the contents of the Stream to the client
            return View(movieReviewGateway.SelectById(id));
        }
        public ActionResult chart()
        {
            
            return base.File("~/Content/chart.jpg","jpeg");

        }

    }
}