using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using ProjectManager.Data;

namespace ProjectManager.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {

        _configuration = configuration;
        _environment = environment;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var conectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(conectionString, ApplicationBuilder =>

            {
                ApplicationBuilder.UseNodaTime();
            });
        }
        );

        services.AddSingleton<IClock>(SystemClock.Instance);

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

    }
    public void Configure(IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline.
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
