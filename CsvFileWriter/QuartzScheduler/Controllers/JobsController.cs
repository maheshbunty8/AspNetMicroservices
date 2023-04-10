using System;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly ISchedulerFactory _schedulerFactory;

    public JobsController(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] JobDto jobDto)
    {
        try
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var jobDetail = JobBuilder.Create(jobDto.JobType)
                .WithIdentity(jobDto.Name, jobDto.Group)
                .PersistJobDataAfterExecution(true)
                .Build();
            var trigger = new CronTriggerImpl
            {
                CronExpressionString = jobDto.CronExpression
            };
            await scheduler.ScheduleJob(jobDetail, trigger);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{name}/{group}")]
    public async Task<IActionResult> Delete(string name, string group)
    {
        try
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var jobKey = new JobKey(name, group);
            await scheduler.DeleteJob(jobKey);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("jobs/{jobKey}")]
    public async Task<IActionResult> GetJob([FromRoute] string jobKey)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobDetail = await scheduler.GetJobDetail(new JobKey(jobKey));
        if (jobDetail == null)
        {
            return NotFound();
        }

        var triggers = await scheduler.GetTriggersOfJob(jobDetail.Key);
        var response = new JobResponse
        {
            JobName = jobDetail.Key.Name,
            JobGroup = jobDetail.Key.Group,
            Parameter1 = jobDetail.JobDataMap.GetString("parameter1"),
            Triggers = triggers.Select(t => t.Key.Name).ToList()
        };

        return Ok(response);
    }
}

public class JobDto
{
    public string Name { get; set; }
    public string Group { get; set; }
    public Type JobType { get; set; }
    public string CronExpression { get; set; }
}

public class JobResponse
{
    public string JobName { get; set; }
    public string JobGroup { get; set; }
    public string Parameter1 { get; set; }

    public IEnumerable<string> Triggers { get; set; }
}
