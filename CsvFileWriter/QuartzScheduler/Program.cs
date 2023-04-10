using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using QuartzScheduler;
using System.Collections.Specialized;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

builder.Services.AddHostedService<QuartzHostedService>();

var app = builder.Build();

var properties = new NameValueCollection
{
    // Set the job store type to use the persistent job store
    ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",

    // Set the connection string for the database
    ["quartz.jobStore.dataSource"] = "default",
    ["quartz.dataSource.default.connectionString"] = "your-connection-string-here",
    ["quartz.dataSource.default.provider"] = "SqlServer",

    // Set additional job store settings
    ["quartz.jobStore.tablePrefix"] = "QRTZ_",
    ["quartz.jobStore.useProperties"] = "true",
    ["quartz.jobStore.clustered"] = "true",
    ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
    ["quartz.jobStore.isClustered"] = "true",
};

var jobListener = new MyJobListener();

var schedulerFactory = new StdSchedulerFactory(properties);
var scheduler = await schedulerFactory.GetScheduler();
scheduler.ListenerManager.AddJobListener(jobListener);
await scheduler.Start();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
