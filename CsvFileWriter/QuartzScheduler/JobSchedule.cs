using Quartz;

namespace QuartzScheduler
{
    public class JobSchedule
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public Type JobType { get; set; }
        public string CronExpression { get; set; }

        public JobSchedule()
        {
        }

        public JobSchedule(string name, string group, Type jobType, string cronExpression)
        {
            Name = name;
            Group = group;
            JobType = jobType;
            CronExpression = cronExpression;
        }

        public IJobDetail CreateJob()
        {
            return JobBuilder.Create(JobType)
                .WithIdentity(Name, Group)
                .Build();
        }

        public ITrigger CreateTrigger()
        {
            return TriggerBuilder.Create()
                .WithIdentity($"{Name}.trigger", Group)
                .WithCronSchedule(CronExpression)
                .Build();
        }
    }
}
