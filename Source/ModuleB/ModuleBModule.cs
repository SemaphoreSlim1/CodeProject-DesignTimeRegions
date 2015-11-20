using Infrastructure;
using Microsoft.Practices.Unity;
using ModuleB.Demonstration;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleB
{
    public class ModuleBModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ModuleBModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            container.RegisterType<View>();
            container.RegisterType<IViewModel, ViewModel>();

            regionManager.RegisterViewWithRegion(NamedRegions.ModuleB, typeof(View));
        }
    }
}
