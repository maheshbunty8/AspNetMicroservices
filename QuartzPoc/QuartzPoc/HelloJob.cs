using Quartz;

namespace QuartzPoc
{
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobKey = context.JobDetail.Key;
            var scheduler = context.Scheduler;

            // Get the current date and time
            var currentTime = DateTime.Now;

            // Set the pause time to the end of the current day
            var pauseTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 23, 59, 59);

            // Pause the job
            await scheduler.PauseJob(jobKey);

            // Schedule the job to resume after the pause time
            var trigger = TriggerBuilder.Create()
                .WithIdentity("ResumeTrigger", "YourJobGroup")
                .StartAt(pauseTime.AddSeconds(1))
                .Build();

            try
            {
                // Do the job processing
                // ...

                // Resume the job
                await scheduler.ResumeJob(jobKey);
            }
            catch (JobExecutionException)
            {
                // If the job execution fails, reschedule the job to resume after the pause time
                await scheduler.ScheduleJob(trigger);
            }
        }
    }
}
