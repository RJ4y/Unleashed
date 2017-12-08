using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.ViewModels;

namespace UnleashedApp
{
    public class Bootstrap
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            //services
            containerBuilder.RegisterType<NavigationService>()
                .As<INavigationService>()
                .SingleInstance();

            //ViewModels
            containerBuilder.RegisterType<MenuViewModel>()
                .As<IMenuViewModel>()
                .SingleInstance();

            containerBuilder.RegisterType<WhoIsWhoViewModel>()
                .As<IWhoIsWhoViewModel>()
                .SingleInstance();
        }
    }
}
