using AutoMapper;
using DemoApp.Core.AutoMapper;
using DemoApp.Core.Infrastructure;
using DemoApp.Core.Repositories;
using DemoApp.Core.Services;
using DemoApp.Repositories.Repositories;
using DemoApp.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace DemoApp_API.Extentions
{
    public static class DIExtensions
    {

        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            AddServiceDI(services);
            AddRepoistoryDI(services);
            //add for data
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //SignalR
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #region automapper
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {

                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion
            return services;
        }

        public static void AddServiceDI(IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IProductService, ProductService>();
        }

        public static void AddRepoistoryDI(IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
