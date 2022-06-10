using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AntesQueVenca.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AntesQueVenca.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
            //    .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.AddDbContext<AntesQueVencaContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("AntesQueVencaDatabase")));
            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
            {
                //options.JsonSerializerOptions.Converters.Add(Custo mConverter);
                
            }); 

            //var settings = new JsonSerializerSettings();
            //settings.Converters.Add(new CustomConverter());
            //settings.TypeNameHandling = TypeNameHandling.Auto;
            //settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            
            //configuration.Formatters.JsonFormatter.SerializerSettings = settings;
            //configuration.Formatters.Add(new BsonMediaTypeFormatter());
            //configuration.Formatters.Add(new FormUrlEncodedMediaTypeFormatter());

            //var appXmlType = configuration.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //configuration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
