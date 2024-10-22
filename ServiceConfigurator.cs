﻿using DeliveryApp.Contracts;

namespace DeliveryApp
{
    public static class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDeliveryApp, DeliveryApp>();
            services.AddScoped<IFileService, FileService>();
        }
    }
}
