using System.Net.Mime;
using ApplicationMediator.Services;
using Autofac;


namespace WebApi.AutofacModules
{
    public class ApplicationServicesAutofacModule : Module
    {


        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DigestsService>().AsSelf()
                   .InstancePerLifetimeScope();

        }
    }
}