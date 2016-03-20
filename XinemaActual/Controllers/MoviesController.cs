using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using XinemaActual.DAL;
using XinemaActual.Models;

namespace XinemaActual.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private MovieGateway movieGateway;
        // Scrapper scrapper = new Scrapper();
        // GET: Cinemas

        public MoviesController()
        {

            movieGateway = new MovieGateway();
            ViewBag.MovieOrderByDropDownItems = movieGateway.GetMovieOrderByNames();
        }
        public ActionResult Index(int? id)
        {
            ////return View(vm);
            if (id != null)
            {
                foreach (var item in ViewBag.MovieOrderByDropDownItems)
                {
                    if (item.Value == id.ToString())
                    {
                        item.Selected = true;


                    }
                }
                return View(movieGateway.SortBy((int)id));
            }
            return View(movieGateway.SelectAllMovies());


        }
        public ActionResult Details(int? id)
        {
            TempData["movieName"] = movieGateway.SelectById(id).movieTitle;
            return View(movieGateway.SelectById(id));
        }
    }
}
