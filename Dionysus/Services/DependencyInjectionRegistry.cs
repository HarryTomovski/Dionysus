﻿using Dionysus.BusinessLogic;
using Dionysus.BusinessLogic.Interfaces;
using Dionysus.DBAccess;
using Dionysus.DBAccess.Interfaces;
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
            services.AddSingleton<IBatchDBAccess, BatchDBAccess>();
            services.AddScoped<IRaspberryBusinessLogic, RaspberryBusinessLogic>();
            services.AddScoped<IUIBusinessLogic, UIBusinessLogic>();
            return services;
        }

    }
}



        
