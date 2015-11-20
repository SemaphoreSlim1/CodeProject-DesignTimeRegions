using Infrastructure;
using Microsoft.Practices.Unity;
using ModuleA.Demonstration;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;

        public ModuleAModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            container.RegisterType<View>();
            container.RegisterType<IViewModel, ViewModel>();

            regionManager.RegisterViewWithRegion(NamedRegions.ModuleA, typeof(View));
        }
    }
}
