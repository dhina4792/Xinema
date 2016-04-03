using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using XinemaActual.DAL;
using XinemaActual.Models;
using GoogleMaps.LocationServices;
namespace XinemaActual.Controllers
{
    public class CinemasController : Controller
    {
        private CinemaGateway cinemaGateway;



        public CinemasController()
        {
            cinemaGateway = new CinemaGateway();
            ViewBag.CinemaOrderByDropDownListItems = cinemaGateway.GetCinemaOrderByNames();
        }
        public ActionResult Index(int? id)
        {
           if (id != null)
            {
                foreach (var item in ViewBag.CinemaOrderByDropDownListItems)
                {
                    if (item.Value == id.ToString())
                    {
                        item.Selected = true;


                    }
                }
                return View(cinemaGateway.SortBy((int)id));
            }
            else
            {
                foreach (var item in ViewBag.CinemaOrderByDropDownListItems)
                {
                    if (item.Text == "All")
                    {
                        item.Selected = true;


                    }
                }

            }
            return View(cinemaGateway.SelectAllCinemas());
        }
        public ActionResult Details(int? id)
        {
            Cinema model = cinemaGateway.SelectById(id);
            var address = model.cinemaAddress;
            int indexOfHyphen = address.IndexOf('-');
            String addressWithoutPhone = address.Substring(0, indexOfHyphen - 1);
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(addressWithoutPhone);
            ViewBag.latCinema = point.Latitude;
            ViewBag.longCinema = point.Longitude;
            TempData["cinemaName"] = cinemaGateway.SelectById(id).cinemaName;
            return View(model);
        }
    }
}