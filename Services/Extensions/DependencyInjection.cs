using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Context;
using Repositories.IRepositories;
using Repositories.Repositories;
using Services.Services.Mapping;
using Services.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Método para realizar la inyecccion de dependencias de los servicios y repositorios
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterfacesInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();

            services.AddScoped<IUserService, UserService>();


            services.AddScoped(typeof(Repositories.IRepositories.IBaseRepository.IBaseReporitory<,>), typeof(Repositories.Repositories.BaseRepository.BaseRepository<,>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoginEventRepository, LoginEventRepository>();

            return services;
        }

        public static IServiceCollection AddServiceDependency(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(configurationExpression =>
            {
                configurationExpression.AddProfile(new MappingProfile());
            });
            services.AddSingleton(prop => mapperConfiguration.CreateMapper());
            return services;
        }
    }
}
