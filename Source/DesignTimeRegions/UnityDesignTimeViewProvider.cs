using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignTimeRegions
{
    public abstract class UnityDesignTimeViewProvider : DesignTimeViewProviderBase
    {
        protected readonly IUnityContainer container = new UnityContainer();

        protected override object ResolveView(Type viewType)
        {
            return container.Resolve(viewType);
        }
    }
}
