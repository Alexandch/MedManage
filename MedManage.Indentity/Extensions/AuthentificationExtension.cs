using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MedManage.Indentity.Extensions
{

    public static class AuthentificationExtension
    {

        public static void AddInfrastractureIndentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.UseSecurityTokenValidators = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false, //тут пизда какая-то надо фиксить с true токен не валиден
                        ValidateLifetime =
                            false, //покак false, каждый новый токен истакает за 2 дня, потом добавим расширение, которое будет у каждого пользователя обновлять токены
                        ValidateIssuerSigningKey = false,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidIssuer = configuration["Jwt:Issuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ??
                                                       "")) // JWT secret keys should not be disclosed. (Major(securirty) замечание от sonar)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Token invalid: " + context.Exception.ToString());
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";
                                return context.Response.WriteAsync("Invalid token.");
                            }
                            else
                            {
                                Console.WriteLine("Response already started, cannot set status code.");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

        }

    }
}