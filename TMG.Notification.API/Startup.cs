using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MediatR;
using TMG.Notification.CommandHandler;
using TMG.Notification.Data;
using Microsoft.EntityFrameworkCore;
using SendGrid.Extensions.DependencyInjection;
using TMG.Notification.QueryHandler;

namespace TMG.Notification.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers().AddNewtonsoftJson();
      services.AddSwaggerGen()
              .AddMediatR(typeof(Startup))
              .AddMediatR(typeof(UpsertEmailPurposeCommandHandler))
              .AddMediatR(typeof(GetSendGridTemplateIdQueryHandler));

      var connectionString = Configuration.GetConnectionString("EmailDb");

      services.AddDbContextPool<EmailDbContext>(options =>
        options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure())
          .EnableSensitiveDataLogging().EnableDetailedErrors());

      services.AddSendGrid(options =>
      {
        options.ApiKey = Configuration["SENDGRID_API_KEY"];
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TMG Notification API");
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
