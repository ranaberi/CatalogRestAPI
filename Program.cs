using System.Net.Mime;
using System.Text.Json;
using Catalog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}



app.MapControllers();

//the predicate is a way to filter out which health checks to include in this endpoint
app.MapHealthChecks("/health/ready", new HealthCheckOptions 
{
    Predicate =(check)=> check.Tags.Contains("ready"),
    ResponseWriter = async(context, report)=>
    {
        var result = JsonSerializer.Serialize
        (
            new
            {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new 
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                        duration = entry.Value.Duration.ToString()

                    })

            }
        );
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});
app.MapHealthChecks("/health/live", new HealthCheckOptions {Predicate =(_)=> false});

app.Run();