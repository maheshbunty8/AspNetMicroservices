using Quartz;

namespace QuartzPoc
{
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"In Hello Job");

            return Task.CompletedTask;
        }
    }
}
