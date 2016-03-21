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


        // Scrapper scrapper = new Scrapper();
        // GET: Cinemas

        public CinemasController()
        {
            cinemaGateway = new CinemaGateway();
            ViewBag.CinemaOrderByDropDownListItems = cinemaGateway.GetCinemaOrderByNames();
        }
        public ActionResult Index(int? id)
        {
            //System.Diagnostics.Debug.WriteLine("Executing cinema job...");
            //CinemaGateway cinemaGateway = new CinemaGateway();
            //// Clear existing database first
            //cinemaGateway.DeleteRange(cinemaGateway.SelectAll());
            //// Scrap new data
            //List<Cinema> cinemaList = cinemaGateway.GetExternalCinemasList("https://www.google.com/movies?near=singapore&rl=1&stok=ABAPP2tdNR_5cLRa-6emW2UtecEL44SX2A%3A1456036737594");

            //int size = cinemaList.Count() - 1;
            //Cinema cinema = new Cinema();
            //// insert new data
            //while (size >= 0)
            //{
            //    System.Diagnostics.Debug.WriteLine("size: " + size);
            //    //cinema.CinemaName = "name";
            //    //cinema.CinemaAddress = "addr";
            //    //cinemaGateway.Insert(cinema);
            //    cinema.cinemaName = cinemaList[size].cinemaName;
            //    System.Diagnostics.Debug.WriteLine("Cinena Name: " + cinema.cinemaName);
            //    cinema.cinemaAddress = cinemaList[size].cinemaAddress;
            //    System.Diagnostics.Debug.WriteLine("Cinema Address: " + cinema.cinemaAddress);
            //    cinemaGateway.Insert(cinema);
            //    //cinemaGateway.Update(cinema);
            //    size--;
            //}
            //System.Diagnostics.Debug.WriteLine("Cinema job ended... ");

            //scrapper.scrapCinemaName("https://www.google.com/movies?near=singapore&rl=1&stok=ABAPP2tdNR_5cLRa-6emW2UtecEL44SX2A%3A1456036737594");
            //CinemasVM vm = new CinemasVM();
            //vm.cinemasName = scrapper.getCinemaNames();
            //return View(vm);
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