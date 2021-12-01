using Dionysus.BusinessLogic;
using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess;
using Dionysus.DBAccess.Interfaces;
using Dionysus.JWT;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dionysus.Services
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddSingleton<IEnvironmentalReadingDBAccess, EnvironmentalReadingDBAccess>();
            services.AddScoped<IUIBusinessLogic, UIBusinessLogic>();

            services.AddSingleton<IBatchDBAccess, BatchDBAccess>();
            services.AddScoped<IBatchBusnessLogic, BatchBusniessLogic>();

            services.AddSingleton<IJWTGeneration, JWTGeneration>();
            
            services.AddSingleton<IUserDBAccess, UserDBAccess>();
            services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();

            services.AddSingleton<IRatingDBAccess, RatingDBAccess>();
            services.AddScoped<IRatingBusinessLogic, RatingBusinessLogic>();

            services.AddSingleton<IEnvironmentalControllerDBAccess, EnvironmentalControllerDBAccess>();
            services.AddScoped<IEnvironmentalControllerBusinessLogic, EnvironmentalControllerBusinessLogic>();
            
            //services.AddSingleton<IElevationCodeDBAccess, ElevationCodeDBAccess>();
            services.AddScoped<IRaspberryBusinessLogic, RaspberryBusinessLogic>();
            

            services.AddSingleton<ISensorDBAccess, SensorDBAccess>();
            services.AddScoped<ISensorBusinessLogic, SensorBusinessLogic>();

            return services;
        }

    }
}



        
