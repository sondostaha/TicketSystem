using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace TicketSystem.Extentions
{
    public static class CustomJwtExtention 
    {
        public static void AddCustomJwtAuthentication(this IServiceCollection services,ConfigurationManager configuration)
        {
            services.AddAuthentication( o=>
           {
               o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               })
                .AddJwtBearer( options => 
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                         ValidateAudience = false,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                    };
                });
        }
        public static void AddCustoemSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(e =>
            {
                e.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Web_POS",
                    //Description = ""
                    Contact = new OpenApiContact()
                    {
                        Name = "Admin",
                        Email = "SuperAdmin@gmail.com",
                        //Url = 
                    }
                });
                e.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter The Jwt Key",
                });
                e.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
                });
            });
        }
    }
}
