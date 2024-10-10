using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using WebApp.DB.Entities;

namespace WebApp.Server;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSerilog((services, lc) => lc.ReadFrom.Configuration(builder.Configuration).ReadFrom.Services(services));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                // Set the default tracking behavior to no tracking.
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(builder.Configuration.GetConnectionString(AppConstants.DatabaseConstants.Application));
            });

            builder.Services.AddHttpClient(
                AppConstants.HttpClientConstants.DefaultClient,
                client =>
                {
                    client.Timeout = TimeSpan.FromSeconds(
                        builder.Configuration.GetValue<int>(AppConstants.HttpClientConstants.DefaultClientTimeout)
                    );
                    client.DefaultRequestHeaders.Add(
                        "X-API-KEY",
                        builder.Configuration[AppConstants.HttpClientConstants.DefaultClientApiKey]
                    );
                }
            );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
