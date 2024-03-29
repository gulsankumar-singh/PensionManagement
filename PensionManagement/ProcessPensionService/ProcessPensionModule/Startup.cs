using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProcessPensionModule.AppDbContext;
using ProcessPensionModule.Filters;
using ProcessPensionModule.Repository;
using ProcessPensionModule.Repository.IRepository;
using ProcessPensionModule.Services.GetPensionerDetailQueries;
using ProcessPensionModule.Utility;
using ProcessPensionModule.Utility.AutoMapperConfig;
using ProcessPensionModule.Utility.SwaggerConfig;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace ProcessPensionModule
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

            services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProcessPensionModule", Version = "v1" });
            //});

            services.AddScoped<IGetPensionerDetail, GetPensionerDetail>();
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPensionDetailRepository, PensionDetailRepository>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.AddAuthentication(auth => {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection(StaticData.JwtDetail).GetSection(StaticData.Key).Value)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration.GetSection(StaticData.JwtDetail).GetSection(StaticData.Issuer).Value,
                    ValidAudience = Configuration.GetSection(StaticData.JwtDetail).GetSection(StaticData.Audience).Value,
                    ClockSkew = System.TimeSpan.Zero,
                    ValidateLifetime = true
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader()
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcessPensionModule v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
