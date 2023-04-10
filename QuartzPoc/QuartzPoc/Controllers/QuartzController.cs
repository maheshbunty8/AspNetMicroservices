using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Reflection.Metadata;

namespace QuartzPoc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuartzController : ControllerBase
    {
        private readonly IScheduler _scheduler;

        public QuartzController(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        [HttpPost("jobs")]
        public async Task<IActionResult> CreateJob([FromBody] JobDto jobDto)
        {
            try
            {
                var jobDetail = JobBuilder.Create(typeof(HelloJob))
                    .WithIdentity(jobDto.JobName, jobDto.JobGroup)
                    .PersistJobDataAfterExecution(true)
                    .Build();

                //var trigger = TriggerBuilder.Create()
                //    .WithIdentity(jobDto.TriggerName, jobDto.TriggerGroup)
                //    .WithCronSchedule(jobDto.CronExpression)
                //    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("my-trigger", "my-group")
                    .WithSimpleSchedule(o =>
                    {
                        o.RepeatForever();
                        o.WithIntervalInSeconds(10);
                    })
                    .Build();

                await _scheduler.ScheduleJob(jobDetail, trigger);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("jobs/{jobName}/{jobGroup}/pause")]
        public async Task<IActionResult> PauseJob(string jobName, string jobGroup)
        {
            try
            {
                var jobKey = new JobKey(jobName, jobGroup);
                await _scheduler.PauseJob(jobKey);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("jobs/{jobName}/{jobGroup}/resume")]
        public async Task<IActionResult> ResumeJob(string jobName, string jobGroup)
        {
            try
            {
                var jobKey = new JobKey(jobName, jobGroup);
                await _scheduler.ResumeJob(jobKey);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("jobs/{jobName}/{jobGroup}")]
        public async Task<IActionResult> DeleteJob(string jobName, string jobGroup)
        {
            try
            {
                var jobKey = new JobKey(jobName, jobGroup);
                await _scheduler.DeleteJob(jobKey);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("jobs/{jobName}/{jobGroup}/triggers")]
        public async Task<IActionResult> GetJobTriggers(string jobName, string jobGroup)
        {
            try
            {
                var jobKey = new JobKey(jobName, jobGroup);
                var triggers = await _scheduler.GetTriggersOfJob(jobKey);

                var triggerDtos = new List<TriggerDto>();
                foreach (var trigger in triggers)
                {
                    triggerDtos.Add(new TriggerDto
                    {
                        TriggerName = trigger.Key.Name,
                        TriggerGroup = trigger.Key.Group,
                        StartTime = trigger.StartTimeUtc.ToString(),
                        EndTime = trigger.EndTimeUtc?.ToString(),
                        MisfireInstruction = trigger.MisfireInstruction.ToString()
                    });
                }

                return Ok(triggerDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class JobDto
        {
            public string JobName { get; set; }
            public string JobGroup { get; set; }
            public string JobType { get; set; }
            public string JobData { get; set; }
            public string TriggerName { get; set; }
            public string TriggerGroup { get; set; }
            public string CronExpression { get; set; }

        }

        public class TriggerDto
        {
            public string TriggerName { get; set; }
            public string TriggerGroup { get; set; }
            public string CronExpression { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public int? Priority { get; set; }
            public string MisfireInstruction { get; set; }
        }
    }
}
