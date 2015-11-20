using System;
using DesignTimeRegions;
using Microsoft.Practices.Unity;
using Infrastructure;

namespace Shell.DesignTime
{
    public class ViewProvider : DesignTimeViewProviderBase
    {
        protected override void RegisterViewsWithContainer()
        {
            //Region A's view and its dependencies
            container.RegisterType<ModuleA.Demonstration.View>();
            container.RegisterType<ModuleA.Demonstration.IViewModel, ModuleA.Demonstration.DesignTime.ViewModel>();

            //Region B's view and its dependencies
            container.RegisterType<ModuleB.Demonstration.View>();
            container.RegisterType<ModuleB.Demonstration.IViewModel, ModuleB.Demonstration.DesignTime.ViewModel>();

            //Region C's view and its dependencies
            container.RegisterType<ModuleC.Demonstration.View>();
            container.RegisterType<ModuleC.Demonstration.IViewModel, ModuleC.Demonstration.DesignTime.ViewModel>();
        }

        protected override void RegisterViewsWithRegions()
        {
            RegisterViewWithRegion<ModuleA.Demonstration.View>(NamedRegions.ModuleA);
            RegisterViewWithRegion<ModuleB.Demonstration.View>(NamedRegions.ModuleB);
            RegisterViewWithRegion<ModuleC.Demonstration.View>(NamedRegions.ModuleC);
        }
    }
}
