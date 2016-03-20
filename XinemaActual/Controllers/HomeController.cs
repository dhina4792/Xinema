using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Data;
using System.Web.Helpers;
using Quartz;
using Quartz.Impl;

namespace XinemaActual.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var IMDBSet = new DataSet();
            IMDBSet.ReadXmlSchema(Server.MapPath("~/App_Data/IMDB.xsd"));
            IMDBSet.ReadXml(Server.MapPath("~/App_Data/IMDB.xml"));
            var IMDBView = new DataView(IMDBSet.Tables[0]);
            var IMDBChart = new Chart(width: 600, height: 400)
                .AddTitle("Top Grossing IMDB Box Office")
                .AddSeries("Default", chartType: "Pie",
                    xValue: IMDBView, xField: "MovieName",
                    yValues: IMDBView, yFields: "MovieReview");
            IMDBChart.Save("~/Content/IMDBChart.jpg", "jpeg");

            var RTSet = new DataSet();
            RTSet.ReadXmlSchema(Server.MapPath("~/App_Data/RT.xsd"));
            RTSet.ReadXml(Server.MapPath("~/App_Data/RT.xml"));
            var RTView = new DataView(RTSet.Tables[0]);
            var RTChart = new Chart(width: 600, height: 400)
                .AddTitle("Top Grossing Rotten Tomatoes Box Office")
                .AddSeries("Default", chartType: "Pie",
                    xValue: RTView, xField: "MovieName",
                    yValues: RTView, yFields: "MovieReview");
            RTChart.Save("~/Content/RTChart.jpg", "jpeg");
            return View();
        }

    }
}