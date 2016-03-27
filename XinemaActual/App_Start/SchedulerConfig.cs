﻿using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using XinemaActual.DAL;
using XinemaActual.DataScrapingJobs;
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

            scheduler.ScheduleJob(cinemaJob, trigger2);


        }


    }
}