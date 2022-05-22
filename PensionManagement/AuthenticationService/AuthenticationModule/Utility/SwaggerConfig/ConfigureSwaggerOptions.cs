using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AuthenticationModule.Utility.SwaggerConfig
{
    /// <summary>
    /// Class for Configuration of Swagger
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
     
        /// <summary>
        /// Method for Configuring swagger options
        /// </summary>
        /// <param name="options">Swagger options parameter</param>
        public void Configure(SwaggerGenOptions options)
        {

            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "AuthenticationModule API",
                Version = "v1",
                Description = "Authentication Microservice will be used to generate the JWT token for Pension Management System"
            });

            //Adding comment file
            var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            options.IncludeXmlComments(cmlCommentsFullPath);
        }
    }
}
