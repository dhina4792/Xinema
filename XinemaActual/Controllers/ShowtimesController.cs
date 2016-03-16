using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XinemaActual.DAL;
using System.Web.Mvc;
using XinemaActual.ViewModels;

namespace XinemaActual.Controllers
{
    public class ShowtimesController : Controller
    {
        private ShowtimeGateway showTimeGateway;
        private MovieGateway movies;
        private CinemaGateway cinemas;
        // GET: Showtimes
        public ShowtimesController()
        {
            showTimeGateway = new ShowtimeGateway();
            movies = new MovieGateway();
            cinemas = new CinemaGateway();

        }
        public ActionResult Index()
        {
            return View(showTimeGateway.SelectAll());
            //return View(showTimeGateway.GetallShowtimes(movies,cinemas));
        }
    }
}