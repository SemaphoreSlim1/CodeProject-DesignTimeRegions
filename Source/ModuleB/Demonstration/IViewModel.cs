using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB.Demonstration
{
    public interface IViewModel
    {
        /// <summary>
        /// Gets the text to display to the user
        /// </summary>
        string DisplayText { get; }
    }
}
