using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignTimeRegions
{
    public abstract class DesignTimeViewProviderBase
    {
        protected readonly IUnityContainer container = new UnityContainer();
        private readonly IDictionary<string, Type> ViewRegistrations = new Dictionary<string, Type>();

        private bool IsInitialized = false;

        public void Initialize()
        {
            if(IsInitialized)
            { return; }

            RegisterViewsWithContainer();
            RegisterViewsWithRegions();
            IsInitialized = true;
        }

        protected abstract void RegisterViewsWithContainer();

        protected abstract void RegisterViewsWithRegions();

        protected void RegisterViewWithRegion<T>(string regionName)
        {
            if(ViewRegistrations.ContainsKey(regionName) == false)
            {
                ViewRegistrations.Add(regionName, typeof(T));
            }
        }

        public object GetViewForRegion(string regionName)
        {
            object view = null;
            Type viewType;

            if(ViewRegistrations.TryGetValue(regionName, out viewType))
            {
                try
                {
                    view = container.Resolve(viewType);
                }catch(Exception ex)
                {
                    view = new TextBlock() { Text = ex.Message };
                }
            }
            else
            {
                view = new TextBlock() { Text = "No view registration for region " + regionName };
            }

            return view;
        }

    }
}
