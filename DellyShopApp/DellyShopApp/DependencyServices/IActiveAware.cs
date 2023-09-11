using System;
using System.Collections.Generic;
using System.Text;

namespace DellyShopApp.DependencyServices
{
    interface IActiveAware
    {
        bool IsActive { get; set; }
        event EventHandler IsActiveChanged;
    }
}
