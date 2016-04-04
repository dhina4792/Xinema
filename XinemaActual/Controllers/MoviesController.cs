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
            ViewBag.MovieGenreDropDownListItems = movieGateway.GetMovieGenres();
        }
        public ActionResult Index(int? aID, int? gID)
        {
            ////return View(vm);
            if (aID != null)
            {
                foreach (var item in ViewBag.MovieOrderByDropDownItems)
                {
                    if (item.Value == aID.ToString())
                    {
                        item.Selected = true;


                    }
                }
                return View(movieGateway.SortBy((int)aID));
            }
            else if (gID != null)
            {
                foreach (var item in ViewBag.MovieGenreDropDownListItems)
                {
                    if (item.Value == gID.ToString())
                    {
                        item.Selected = true;
                    }
                }
                return View(movieGateway.SelectMovieByMovieByGenres((int)gID));

            }
            else
            {
                return View(movieGateway.SelectAllMovies());
            }


        }
        public ActionResult Details(int? id)
        {
            TempData["movieName"] = movieGateway.SelectById(id).movieTitle;
            return View(movieGateway.SelectById(id));
        }
    }
}
