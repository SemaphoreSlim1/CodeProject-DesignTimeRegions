using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleC.Demonstration.DesignTime
{
    public class ViewModel : IViewModel
    {
        /// <summary>
        /// Gets the text to display at design time
        /// </summary>
        public string DisplayText
        {
            get
            { 
                return "Hello Module C from design time view model";
            }
        }
    }
}
