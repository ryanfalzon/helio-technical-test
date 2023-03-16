using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using PhoneBook.Lib.Infra;
using Swashbuckle.AspNetCore.SwaggerGen;

#pragma warning disable CS1591

namespace PhoneBook.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var appSettings = new AppSettings();
        _configuration.Bind(appSettings);

        if (appSettings.ConnectionStrings == null) throw new ArgumentNullException(nameof(appSettings.ConnectionStrings));

        services.AddSingleton(_ => appSettings.ConnectionStrings);

        services.AddControllers(options => { options.Filters.Add(typeof(CustomExceptionFilter)); }).AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.WriteIndented        = true;
            x.JsonSerializerOptions.PropertyNamingPolicy = null;
        }).AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.WriteIndented        = true;
            x.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        
        services.AddApiVersioning(config =>
        {
            config.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat           = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        services.AddPhoneBookConfig(appSettings.ConnectionStrings);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/version", async context =>
            {
                var version = Assembly.GetAssembly(typeof(Startup))?.GetName().Version;
                await context.Response.WriteAsync($"{version}");
            });

            endpoints.MapControllers();
        });
    }
}