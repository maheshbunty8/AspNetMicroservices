using Quartz;

namespace QuartzScheduler
{
    public class MyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // check if data is available
            bool isDataAvailable = true; //CheckDataAvailability();

            if (isDataAvailable)
            {
                var jobKey = context.JobDetail.Key;

                // pause the job
                await context.Scheduler.PauseJob(jobKey);

                // set the job's end time to today's date
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(jobKey.Name + "_Trigger", jobKey.Group)
                    .EndAt(DateBuilder.TodayAt(23, 59, 59))
                    .Build();

                await context.Scheduler.ScheduleJob(trigger);
            }
            else
            {
                // data is not available, continue with the job execution
                // ...
            }
        }
    }
}
