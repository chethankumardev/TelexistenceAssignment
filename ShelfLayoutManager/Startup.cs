using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;
using ShelfLayoutManager.RepositoriesInterface.Repositories;
using Serilog;
using ShelfLayoutManager.Repositories;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ShelfLayoutDbContext>(
            options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShelfLayoutManager", Version = "v1" });
        });

        services.AddControllers();
        services.AddScoped<ICabinetRepository, CabinetRepository>();
        services.AddScoped<IRowRepository, RowRepository>();
        services.AddScoped<ILaneRepository, LaneRepository>();
        services.AddScoped<ISKURepository, SKURepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();

        Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Information() 
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day) 
       .CreateLogger();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShelfLayoutManager");
        });

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
