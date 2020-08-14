using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;


namespace Sweeper.Infrastructure
{
    public class XamarinEssentialsPropertyRepo : IPropertyRepository
    {
        public bool LoadOnCreate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool LoadOnGet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool SaveOnSet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XamarinEssentialsPropertyRepo(bool loadOnCreate, bool loadOnGet, bool saveOnSet)
        {
            LoadOnCreate = loadOnCreate;
            LoadOnGet = loadOnGet;
            SaveOnSet = SaveOnSet;

        }
       
        public object LoadProperty(string propname)
        {
            throw new NotImplementedException();
        }

        public bool SaveProperty(string propname, object value)
        {
            throw new NotImplementedException();
        }
    }
}
