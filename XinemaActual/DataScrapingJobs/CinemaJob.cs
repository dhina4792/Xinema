using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XinemaActual.DAL;
using XinemaActual.Models;
using XinemaActual.DataScrapingLogic;

namespace XinemaActual.DataScrapingJobs
{
    public class CinemaJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            System.Diagnostics.Debug.WriteLine("Executing cinema job...");
            CinemaGateway cinemaGateway = new CinemaGateway();
            ShowtimeGateway showtimeGateway = new ShowtimeGateway();
            GoogleMoviesScraper gms = new GoogleMoviesScraper();
            gms.TargetURL = "https://www.google.com/movies?near=singapore&rl=1&stok=ABAPP2tdNR_5cLRa-6emW2UtecEL44SX2A%3A1456036737594";
            // Scrap new data
            gms.scrapCinemaInfo();
            List<Cinema> cinemaList = gms.getCinemas();
            List<ShowTime> showTimeList = gms.getShowtimes();
            System.Diagnostics.Debug.WriteLine("cinemaList.Count() " + cinemaList.Count());
            // Check if cinemaList is done

            System.Threading.SpinWait.SpinUntil(() => gms.getIsComplete() == true);
            //Clear existing database first
            cinemaGateway.DeleteRange(cinemaGateway.SelectAllCinemas());
            System.Diagnostics.Debug.WriteLine("Deleted database");
            int cinemaIndex = cinemaList.Count() - 1;
            int showTimeIndex = showTimeList.Count() - 1;
            Cinema cinema = new Cinema();
            ShowTime showTime = new ShowTime();
            // insert new data
            while (cinemaIndex >= 0)
            {
                System.Diagnostics.Debug.WriteLine("Index: " + cinemaIndex);
                cinema.cinemaName = cinemaList[cinemaIndex].cinemaName;
                System.Diagnostics.Debug.WriteLine("Cinena Name: " + cinema.cinemaName);
                cinema.cinemaAddress = cinemaList[cinemaIndex].cinemaAddress;
                System.Diagnostics.Debug.WriteLine("Cinema Address: " + cinema.cinemaAddress);
                cinemaGateway.Insert(cinema);
                cinemaIndex--;
            }

            while (showTimeIndex >= 0)
            {
                showTime.showtimeStartTime = showTimeList[showTimeIndex].showtimeStartTime;
                showTime.showtimeDate = showTimeList[showTimeIndex].showtimeDate;
                showTime.showtimeTitle = showTimeList[showTimeIndex].showtimeTitle;
                showTime.showtimeCinemaName = showTimeList[showTimeIndex].showtimeCinemaName;
                showtimeGateway.Insert(showTime);
                showTimeIndex--;
            }



            System.Diagnostics.Debug.WriteLine("Cinema job ended... ");
        }
    }
}

