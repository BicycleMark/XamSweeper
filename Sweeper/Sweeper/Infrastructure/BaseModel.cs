using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sweeper.Infrastructure
{
    public class BaseModel : BindableBase
    {
        IPropertyRepository repository;
        public BaseModel(IPropertyRepository repo)
        {
            repository = repo;
            
        }

        public object Load(string propName )
        {
            return repository.LoadProperty(propName);
        }

        public bool Save(object value, string propName)
        {
            return repository.SaveProperty(propName, value);
        }
    }
}
