using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DesignTimeRegions
{
    /// <summary>
    /// Base implementation for design time view providers
    /// </summary>
    public abstract class DesignTimeViewProviderBase
    {        
        private readonly IDictionary<string, Type> ViewRegistrations = new Dictionary<string, Type>();

        private bool IsInitialized = false;

        public void Initialize()
        {
            if (IsInitialized)
            { return; }

            RegisterViewsWithContainer();
            RegisterViewsWithRegions();
            IsInitialized = true;
        }

        /// <summary>
        /// Registers the views and any dependent design time view models with their appropriate containers
        /// </summary>
        protected abstract void RegisterViewsWithContainer();

        /// <summary>
        /// Registers the type of view with the appropriate named region. Use the <see cref="RegisterViewWithRegion{T}(string)"/> helper method in implementing this function.
        /// </summary>
        protected abstract void RegisterViewsWithRegions();

        /// <summary>
        /// Resolves the desired view from the container and provides an instance of the desired view.
        /// </summary>
        /// <param name="viewType">The desired type of view</param>        
        protected abstract object ResolveView(Type viewType);

        protected void RegisterViewWithRegion<T>(string regionName)
        {
            if (ViewRegistrations.ContainsKey(regionName) == false)
            {
                ViewRegistrations.Add(regionName, typeof(T));
            }
        }

        public object GetViewForRegion(string regionName)
        {
            object view = null;
            Type viewType;

            if (ViewRegistrations.TryGetValue(regionName, out viewType))
            {
                try
                {
                    view = ResolveView(viewType);
                }
                catch (Exception ex)
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
