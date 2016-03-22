﻿using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using XinemaActual.DAL;
using XinemaActual.Models;

namespace XinemaActual.App_Start
{
    public class SchedulerConfig
    {
        public static void Start()
        {
            // Get scheduler instance from factory and start
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail cinemaJob = JobBuilder.Create<CinemaJob>()
    .WithIdentity("cinema_job", "scraping")
    .Build();

            ITrigger trigger1 = TriggerBuilder.Create()
    .WithIdentity("cinema_trigger", "scrapping")
    .WithDailyTimeIntervalSchedule
      (s =>
         s.WithIntervalInHours(24)
        .OnEveryDay()
        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
      )
    .Build();

            ITrigger trigger2 = TriggerBuilder.Create()
.WithIdentity("cinema_trigger", "scrapping")
.StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInHours(24)
                .RepeatForever())
            .Build();

            scheduler.ScheduleJob(cinemaJob, trigger1);


        }

        private class CinemaJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                System.Diagnostics.Debug.WriteLine("Executing cinema job...");
                CinemaGateway cinemaGateway = new CinemaGateway();
              
               
                // Scrap new data
                List<Cinema>cinemaList = cinemaGateway.GetExternalCinemasList("https://www.google.com/movies?near=singapore&rl=1&stok=ABAPP2tdNR_5cLRa-6emW2UtecEL44SX2A%3A1456036737594");
                if (cinemaList.Count()!=0)
                {
                    //Clear existing database first
                    cinemaGateway.DeleteRange(cinemaGateway.SelectAll());
                    int size = cinemaList.Count() - 1;
                    Cinema cinema = new Cinema();
                    // insert new data
                    while (size >= 0)
                    {
                        System.Diagnostics.Debug.WriteLine("size: " + size);
                        //cinema.CinemaName = "name";
                        //cinema.CinemaAddress = "addr";
                        //cinemaGateway.Insert(cinema);
                        cinema.cinemaName = cinemaList[size].cinemaName;
                        System.Diagnostics.Debug.WriteLine("Cinena Name: " + cinema.cinemaName);
                        cinema.cinemaAddress = cinemaList[size].cinemaAddress;
                        System.Diagnostics.Debug.WriteLine("Cinema Address: " + cinema.cinemaAddress);
                        cinemaGateway.Insert(cinema);
                        //cinemaGateway.Update(cinema);
                        size--;
                    }
                }
                //Cinema cinema = new Cinema();
                //cinema.cinemaName = "test cinema name for nuget package test";
                //cinema.cinemaAddress = "test cinema address for nuget package test";
                //cinemaGateway.Insert(cinema);
                System.Diagnostics.Debug.WriteLine("Cinema job ended... ");
            }
        }

    }
}