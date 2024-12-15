using MedManage.Application.Interfaces;
using MedManage.Application.Mappings;
using MedManage.Application.Services;
using MedManage.Domain.Entities;
using MedManage.Domain.Interfaces;
using MedManage.Persistence.Data;
using MedManage.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MedManage.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Регистрация всех AutoMapper профилей
        public static void AddCoreApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(UserMappingProfile));
            services.AddAutoMapper(typeof(ProductMappingProfile));
            services.AddAutoMapper(typeof(InventoryMappingProfile));
            services.AddAutoMapper(typeof(OrganizationMappingProfile));
            services.AddAutoMapper(typeof(AnnouncementMappingProfile));
        }




    }
}