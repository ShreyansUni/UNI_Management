using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using UNI_Management.Common;
using UNI_Management.Common.DependencyInjection;
using UNI_Management.Data.Repository;
using UNI_Management.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Service
{
    public static class ServiceRegistry
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<ITempDataDictionary, TempDataDictionary>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           // services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddScoped<IDapperService, DapperService>();
            services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

            // scan all dependency of solution
            services.AddDependencyScanning().ScanAssembly();
        }
    }
}
