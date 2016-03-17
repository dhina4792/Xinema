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


        }
        public ActionResult Index()
        {
            return View(showTimeGateway.SelectAll());

        }
    }
}