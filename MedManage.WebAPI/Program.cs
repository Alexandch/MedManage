using System.Text;
using MedManage.Application.Extensions;
using MedManage.Indentity.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MedManage.Indentity.Extensions;
using UserManagement.Application.Filters.ExceptionHadling;
using UserManagement.Persistence.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAppDbContext(builder.Configuration);
        builder.Services.AddCoreApplicationServices();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddDirectoryBrowser();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();  //в builder все службы регистрируем
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerAuthentication();
        builder.Services.AddInfrastructureRepositoriesServices(builder.Configuration);
        builder.Services.AddCoreApplicationServices();
        
        //builder.Services.AddCoreApplicationServices();
        //builder.Services.AddInfrastructureRepositoriesServices();
        
        var app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "MedManage WebAPI");
            options.RoutePrefix = string.Empty;  
        });
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}