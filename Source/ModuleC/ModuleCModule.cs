using Infrastructure;
using Microsoft.Practices.Unity;
using ModuleC.Demonstration;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleC
{
    public class ModuleCModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ModuleCModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            container.RegisterType<View>();
            container.RegisterType<IViewModel, ViewModel>();

            regionManager.RegisterViewWithRegion(NamedRegions.ModuleC, typeof(View));
        }
    }
}
