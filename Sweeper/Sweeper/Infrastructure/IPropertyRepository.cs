using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public interface IPropertyRepository
    {
        bool SaveProperty(string propname, object value);
        object LoadProperty(string propname);
    }
}
