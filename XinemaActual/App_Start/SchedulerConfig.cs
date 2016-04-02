using Quartz;
using Quartz.Impl;
using XinemaActual.DataScrapingJobs;


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
                .WithIntervalInHours(2)
                .RepeatForever())
            .Build();

            scheduler.ScheduleJob(cinemaJob, trigger2);


        }


    }
}