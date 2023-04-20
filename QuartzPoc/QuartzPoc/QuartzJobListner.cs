using Quartz;

namespace QuartzPoc
{
    public class QuartzJobListner : IJobListener
    {
        public string Name => "QuartzJobListner";

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"Job to be Vetoed :  {context.JobDetail.Key}");
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"Job to be Executed :  {context.JobDetail.Key}");

            bool isDataAvailable = CheckIfDataIsAvailable();

            // If data is available, pause the job
            if (isDataAvailable)
            {
                var scheduler = context.Scheduler;
                var jobDetail = context.JobDetail;
                var jobKey = jobDetail.Key;

                scheduler.PauseJob(jobKey);

                Console.WriteLine($"Job {jobKey} has been paused as data is available.");
            }

        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"Job Executed :  {context.JobDetail.Key}");
        }

        private bool CheckIfDataIsAvailable()
        {
            // Check if data is available and return true or false
            // Replace this with your own data availability check
            return true;
        }
    }
}
