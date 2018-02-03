using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;

namespace Henchman
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
            services.AddTransient<IMongoClient>(provider => GetMongoClient());
            services.AddTransient<IHenchmanDAO, HenchmanDAO>();
            services.AddTransient<IHenchmanBLC, HenchmanBLC>();
            
            services.AddMvc();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Title = "Henchman",
                    Version = "v1",
                    Description = "Service used to retrieve Henchmen"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("v1/swagger.json", "Henchman");
            });
        }

        public MongoClient GetMongoClient()
        {
            var user = "admin";
            var password = "password";
            var name = "CloudFoundry_oumhg86d_nq48b1sc";
            var connection = $"mongodb://{user}:{password}@ds121896.mlab.com:21896/{name}";
            
            return new MongoClient(connection);
        }
    }
}
