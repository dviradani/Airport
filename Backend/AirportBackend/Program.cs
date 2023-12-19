using AirportBackend.Data;
using AirportBackend.Services.Interfaces;
using AirportBackend.Services.Repositories;
using AirportBackend.Services.signalR;
using AirportBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace AirportBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AirportContext>(o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
            builder.Services.AddSingleton<IFlightsListManager, FlightsListManager>();
            builder.Services.AddSingleton<ISimulator, Simulator>();
            builder.Services.AddSingleton<IRouteManager, RouteManager>();
            builder.Services.AddSingleton<IMainRepository, MainRepository>();
            builder.Services.AddSingleton<HubContextService>();
            builder.Services.AddSignalR();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            using (var scope = app.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AirportContext>();
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.MapHub<AirportHub>("/hub");

            app.Run();
        }
    }
}