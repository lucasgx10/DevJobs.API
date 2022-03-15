using DevJobs.API.persistence;
using DevJobs.API.persistence.repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DevJobsCs2");

Console.WriteLine($"ConnectionString e {connectionString}");
builder.Services.AddDbContext<DevJobsContext>(options => 
options.UseSqlServer(connectionString));

builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{
        Title = "DevJobs.APi",
        Version = "v1",
        Contact = new OpenApiContact {
            Name = "Lucasgx10" 
        }
    });

    var xmlFile = "DevJobs.API.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

builder.Host.ConfigureAppConfiguration((hostingContext, config) => {
    Serilog.Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.MSSqlServer(connectionString,
            sinkOptions: new MSSqlServerSinkOptions() {
                AutoCreateSqlTable = true,
            TableName = "logs"
        })
        .WriteTo.Console()
        .CreateLogger();
}).UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(); 
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
