// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

/* 
 //SQLBULK COPY


string connectionString = "your_connection_string_here";
string destinationTableName = "your_destination_table_name_here";

var data = new List<YourDataClass>
{
    // your data here
};

var bulkCopy = new SqlBulkCopyAsync<YourDataClass>(connectionString, destinationTableName);
aw
await bulkCopy.BulkCopyAsync(data);
*/

/* 
//DB Utility

CREATE TABLE Person (
    Id int NOT NULL PRIMARY KEY,
    FirstName varchar(50) NOT NULL,
    LastName varchar(50) NOT NULL,
    Age int NOT NULL
)

var dbUtility = new DbUtility<SqlConnection>("connection string goes here");

var query = "INSERT INTO Person (Id, FirstName, LastName, Age) VALUES (@Id, @FirstName, @LastName, @Age)";

var parameters = new List<DbParameter>
{
    new SqlParameter("@Id", 1),
    new SqlParameter("@FirstName", "John"),
    new SqlParameter("@LastName", "Doe"),
    new SqlParameter("@Age", 30)
};

var rowsAffected = await dbUtility.ExecuteNonQueryAsync(query, CommandType.Text, parameters);

Console.WriteLine($"Rows affected: {rowsAffected}");

//------------------

var dbUtility = new DbUtility<SqlConnection>("connection string goes here");

var query = "SELECT COUNT(*) FROM Person";

var count = await dbUtility.ExecuteScalarAsync(query, CommandType.Text);

Console.WriteLine($"Count: {count}");

//---------------------------

var dbUtility = new DbUtility<SqlConnection>("connection string goes here");

var query = "SELECT Id, FirstName, LastName, Age FROM Person";

var persons = await dbUtility.ExecuteReaderAsync(query, reader => new Person
{
    Id = reader.GetInt32(0),
    FirstName = reader.GetString(1),
    LastName = reader.GetString(2),
    Age = reader.GetInt32(3)
}, CommandType.Text);

foreach (var person in persons)
{
    Console.WriteLine($"Id: {person.Id}, Name: {person.FirstName} {person.LastName}, Age: {person.Age}");
}

*/

/*
 
//Exception Management Utility
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // other middleware registrations

    app.UseMiddleware<ExceptionManagementMiddleware>();
}

 */

/*
 
static async Task Main(string[] args)
    {
        // Create a new Quartz scheduler factory
        var schedulerFactory = new StdSchedulerFactory();

        // Get a scheduler instance from the factory
        var scheduler = await schedulerFactory.GetScheduler();

        // Load the Quartz job configurations from the XML file
        var xml = File.ReadAllText("quartz_jobs.xml");
        await scheduler.ScheduleJobs(new Quartz.Impl.Xml.JobSchedulingDataParser().Parse(xml), replace: true);

        // Start the scheduler
        await scheduler.Start();

        // Wait for the jobs to run
        await Task.Delay(TimeSpan.FromSeconds(60));

        // Shutdown the scheduler
        await scheduler.Shutdown();
    }
 
 */