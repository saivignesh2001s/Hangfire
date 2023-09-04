using Autofac;
using Unconnectedwebapi.CustomMiddleware;
using Unconnectedwebapi.Repository;

namespace Unconnectedwebapi
{
    public sealed class autofacmodule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //AddTransient
            builder.RegisterType<Usermethods>().As<IUsermethods>()
                .InstancePerDependency();
            builder.RegisterType<ExceptionMiddleware>()
                .InstancePerDependency();
            //AddScoped
            //builder.RegisterType<Usermethods>().As<IUsermethods>()
            //  .InstancePerLifetimeScope();
            //AddSingleton
            //builder.RegisterType<Usermethods>().As<IUsermethods>()
            //.SingleInstance();
        }

       
    }
}
