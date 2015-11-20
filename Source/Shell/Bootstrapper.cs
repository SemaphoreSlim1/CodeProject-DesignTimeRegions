using Microsoft.Practices.Unity;
using ModuleA;
using ModuleB;
using ModuleC;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Shell
{
    public class Bootstrapper : UnityBootstrapper
    {
        private IEnumerable<Type> moduleTypes = new Type[] 
        {
            typeof(ModuleAModule),
            typeof(ModuleBModule),
            typeof(ModuleCModule)
        };

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();            

            Container.RegisterType<MainWindow>();
        }

        protected override void ConfigureModuleCatalog()
        {
            var mc = ModuleCatalog as ModuleCatalog;

            if (mc == null)
            { return; }

            foreach (var moduleType in moduleTypes)
            {
                mc.AddModule(moduleType);
            }
        }
    }
}
