using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public class NullRepo : IPropertyRepository
    {
        public object LoadProperty(string propname)
        {
            return null;
        }

        public bool SaveProperty(string propname, object value)
        {
            return true;
        }
    }
}
