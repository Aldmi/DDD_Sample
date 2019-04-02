using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Digests.Data.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Kernel.Enums;
using WebApi.AutofacModules;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }



        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = Configuration.GetConnectionString("DigestsSubDomainDbConnectionUseNpgsql");
            builder.RegisterModule(new UnitOfWorkAutofacModule(connectionString));

            builder.RegisterModule(new ApplicationServicesAutofacModule());
        }



        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILifetimeScope scope)
        {
            InitializeAsync(scope).Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }


        /// <summary>
        /// Инициализация системы.
        /// </summary>
        private async Task InitializeAsync(IComponentContext scope)
        {
            //var logger = scope.Resolve<ILogger>();
            //СОЗДАНИЕ БД (если не созданно)--------------------------------------------------
            try
            {
                var uow = scope.Resolve<IUnitOfWorkDigests>();
                await uow.CreateDb(HowCreateDb.EnsureCreated); // EnsureCreated 4 Debug
            }
            catch (Exception ex)
            {
                //logger.Fatal($"Ошибка создания БД на основе миграций {ex}");
            }
        }
    }
}
