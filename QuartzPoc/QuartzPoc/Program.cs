using Microsoft.OpenApi.Models;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using Quartz.Impl.Matchers;
using QuartzPoc;
using static Quartz.Logging.OperationName;
using System.Collections.Specialized;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddQuartz(q =>
{
    // configure Quartz
    q.UseMicrosoftDependencyInjectionJobFactory();

    q.UsePersistentStore(s =>
    {
        s.UseSqlServer("Data Source=MAHESH-BUNTY;Database=CommRisk;Integrated Security=True;TrustServerCertificate=True");
        s.UseJsonSerializer();
    });

    // add the listener to the scheduler
    q.AddSchedulerListener<QuartListener>();
    q.AddJobListener<QuartzJobListner>();
});
builder.Services.AddQuartzHostedService(opt => 
{
    opt.WaitForJobsToComplete = true;
    //opt.StartDelay = TimeSpan.FromSeconds(10);
});
builder.Services.Configure<QuartzOptions>(options =>
{
    options.Scheduling.IgnoreDuplicates = true;
    options.Scheduling.OverWriteExistingData = true;
});

builder.Services.AddSingleton(provider =>
{
    var properties = new NameValueCollection
    {
        // Set the job store type to use the persistent job store
        ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",

        // Set the connection string for the database
        ["quartz.jobStore.dataSource"] = "default",
        ["quartz.dataSource.default.connectionString"] = "Data Source=MAHESH-BUNTY;Database=CommRisk;Integrated Security=True;TrustServerCertificate=True",
        ["quartz.dataSource.default.provider"] = "SqlServer",

        // Set additional job store settings
        ["quartz.jobStore.tablePrefix"] = "QRTZ_",
        ["quartz.jobStore.useProperties"] = "true",
        ["quartz.jobStore.clustered"] = "true",
        ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
        ["quartz.jobStore.isClustered"] = "true",
    };
    // create the scheduler factory
    var factory = new StdSchedulerFactory(properties);

    // create the scheduler instance
    var scheduler = factory.GetScheduler().GetAwaiter().GetResult();
    
    // start the scheduler
    scheduler.Start().GetAwaiter().GetResult();

    // return the scheduler instance
    return scheduler;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

await app.RunAsync();
