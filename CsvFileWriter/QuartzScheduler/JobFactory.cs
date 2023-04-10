using Quartz.Spi;
using Quartz;

namespace QuartzScheduler
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            var jobType = jobDetail.JobType;

            return (IJob)_serviceProvider.GetService(jobType);
        }

        public void ReturnJob(IJob job)
        {
            // No need to do anything here
        }
    }
}
