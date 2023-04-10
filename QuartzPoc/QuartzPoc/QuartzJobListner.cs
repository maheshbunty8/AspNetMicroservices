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
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            await Console.Out.WriteLineAsync($"Job Executed :  {context.JobDetail.Key}");
        }
    }
}
