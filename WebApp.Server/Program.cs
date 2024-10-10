using Microsoft.EntityFrameworkCore;
using WebApp.DB.Entities;

namespace WebApp.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
                client.Timeout = TimeSpan.FromSeconds(builder.Configuration.GetValue<int>(AppConstants.HttpClientConstants.DefaultClientTimeout));
                client.DefaultRequestHeaders.Add("X-API-KEY", builder.Configuration[AppConstants.HttpClientConstants.DefaultClientApiKey]);
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
}
