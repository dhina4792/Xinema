using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XinemaActual.DAL;
using System.Web.Mvc;

namespace XinemaActual.Controllers
{
    public class ShowtimesController : Controller
    {
        private ShowtimeGateway showTimeGateway;


        // GET: Showtimes
        public ShowtimesController()
        {
            showTimeGateway = new ShowtimeGateway();
            ViewBag.CinemaDropDownListItems = showTimeGateway.GetCinemaNames();
            ViewBag.DateDropDownListItems = showTimeGateway.GetShowtimeDates();
            ViewBag.MovieDropDownListItems = showTimeGateway.GetShowtimeMovies();


        }
        public ActionResult Index(int? cID, int? dID, int? mID)
        {


            if (cID != null)
            {
                foreach (var item in ViewBag.CinemaDropDownListItems)
                {
                    if (item.Value == cID.ToString())
                    {
                        item.Selected = true;
                        return View(showTimeGateway.SelectShowtimeByCinemaNameAndCurrentDate((int)cID));


                    }
                }
                
            }
            else if (mID != null)
            {
                foreach (var item in ViewBag.MovieDropDownListItems)
                {
                    if (item.Value == mID.ToString())
                    {
                        item.Selected = true;
                        return View(showTimeGateway.SelectShowtimeByMovieAndCurrentDate((int)mID));


                    }
                }

            }

            else if(dID != null)
            {
                foreach (var item in ViewBag.DateDropDownListItems)
                {
                    if (item.Value == dID.ToString())
                    {
                        item.Selected = true;


                    }
                    else 
                    {
                        item.Selected = false;


                    }

                }
                return View(showTimeGateway.SelectShowtimeByDate((int)dID));

            }
            else if(TempData["cinemaName"] !=null)
            {
                string cinemaName = TempData["cinemaName"].ToString();
                foreach (var item in ViewBag.CinemaDropDownListItems)
                {
                    if (item.Text == cinemaName)
                    {
                        item.Selected = true;
                        TempData["cinemaName"] = null;
                        return View(showTimeGateway.SelectShowtimeByCinemaNameAndCurrentDate(Convert.ToInt32(item.Value)));

                    }

                }
                
            }
            else if (TempData["movieName"] != null)
            {
                string movieName = TempData["movieName"].ToString();
                foreach (var item in ViewBag.MovieDropDownListItems)
                {
                    if (item.Text == movieName)
                    {
                        item.Selected = true;
                        TempData["movieName"] = null;
                        return View(showTimeGateway.SelectShowtimeByMovieAndCurrentDate(Convert.ToInt32(item.Value)));

                    }

                }

            }
            return View(showTimeGateway.SelectAllShowtimes());

        }
   
    }
}