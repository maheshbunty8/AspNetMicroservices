using Quartz;

namespace QuartzPoc
{
    public class QuartListener : ISchedulerListener
    {
        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {trigger.JobKey} scheduled at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {triggerKey.Name} unscheduled at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {trigger.JobKey} triggered at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {triggerKey.Name} paused at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Triggers in group {triggerGroup} paused at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {triggerKey.Name} resumed at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Triggers in group {triggerGroup} resumed at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {jobDetail.Key} added at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {jobKey.Name} deleted at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {jobKey.Name} paused at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Job {jobKey.Name} interrupted at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Jobs in group {jobGroup} paused at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Jobs : {jobKey} Resumed at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Jobs in group {jobGroup} paused at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Scheduler is Standby Mode at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Scheduler is Started at {DateTime.Now}");
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
