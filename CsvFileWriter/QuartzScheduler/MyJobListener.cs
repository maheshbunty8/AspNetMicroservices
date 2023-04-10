using Quartz;

namespace QuartzScheduler
{
    public class MyJobListener : IJobListener
    {
        public string Name => "MyJobListener";

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {context.JobDetail.Key} is about to be executed");
            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {context.JobDetail.Key} was vetoed");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {context.JobDetail.Key} was executed");
            return Task.CompletedTask;
        }
    }
}
