using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Infra.Identity.Models;
using TimeProject.Infra.IoC;

namespace TimeProject.Services.Api
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(opt => opt.AddPolicy("*", plc => plc.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().Build()));

            services.AddIdentityMongoDbProvider<User, MongoRole<string>, string>(identity =>
            {
                identity.Password.RequiredLength = 6;
                identity.Password.RequireDigit = false;
                identity.Password.RequireLowercase = false;
                identity.Password.RequireNonAlphanumeric = false;
                identity.Password.RequireUppercase = false;

                identity.User.RequireUniqueEmail = false;
            }, mongo =>
            {
                mongo.UsersCollection = "user";
                mongo.ConnectionString = Configuration.GetConnectionString("MongoDbIdentity");
            });


            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                var parameters = jwt.TokenValidationParameters;
                parameters.ValidateIssuer = false;
                parameters.ValidateLifetime = true;
                parameters.ValidateAudience = false;
                parameters.ValidateIssuerSigningKey = true;
                parameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]));

            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeProject", Version = "v1" });
            });

            NativeInject.InjectDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("*");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeProject"));

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
