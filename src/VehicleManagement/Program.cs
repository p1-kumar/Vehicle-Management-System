
using VehicleManagement.DataSource;
using VehicleManagement.Services;

namespace VehicleManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<ITripService, TripService>();
            builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
            
            builder.Services.AddSingleton<IDataStore>(provider => new JsonDataStore(builder.Configuration.GetValue<string>("FilePath") ?? string.Empty));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
