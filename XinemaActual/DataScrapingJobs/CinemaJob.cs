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
            GoogleMoviesScraper gms = new GoogleMoviesScraper("https://www.google.com/movies?near=singapore&rl=1&stok=ABAPP2tdNR_5cLRa-6emW2UtecEL44SX2A%3A1456036737594");

            // Scrap new data
            List<Cinema> cinemaList = gms.getCinemas();
            // Check if cinemaList is valid
            if (cinemaList.Count() != 0)
            {
                //Clear existing database first
                cinemaGateway.DeleteRange(cinemaGateway.SelectAll());
                int size = cinemaList.Count() - 1;
                Cinema cinema = new Cinema();
                // insert new data
                while (size >= 0)
                {
                    System.Diagnostics.Debug.WriteLine("size: " + size);
                    cinema.cinemaName = cinemaList[size].cinemaName;
                    System.Diagnostics.Debug.WriteLine("Cinena Name: " + cinema.cinemaName);
                    cinema.cinemaAddress = cinemaList[size].cinemaAddress;
                    System.Diagnostics.Debug.WriteLine("Cinema Address: " + cinema.cinemaAddress);
                    cinemaGateway.Insert(cinema);
                    size--;
                }
            }
            System.Diagnostics.Debug.WriteLine("Cinema job ended... ");
        }
    }
}

