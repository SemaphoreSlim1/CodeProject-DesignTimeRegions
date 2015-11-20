using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB.Demonstration
{
    public class ViewModel : IViewModel
    {
        /// <summary>
        /// Gets the text to display at run time
        /// </summary>
        public string DisplayText
        {
            get
            {
                return "Hello Module B from actual implementation view model";
            }
        }
    }
}
