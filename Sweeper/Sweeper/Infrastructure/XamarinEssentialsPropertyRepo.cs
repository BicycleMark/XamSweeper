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

        public string GetPrivatePreferencesSharedName(string feature)
        {
            return $"{AppInfo.PackageName}.xamarinessentials.{feature}";
        }

        public bool ContainsKey(string key)
        {
            return Preferences.ContainsKey(key);
        }

        public void Remove(string key)
        {
            Preferences.Remove(key);
        }

        public void Clear()
        {
            Preferences.Clear();
        }

        public string Get(string key, string defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public bool Get(string key, bool defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public int Get(string key, int defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public double Get(string key, double defaultValue)
        {
            return Preferences.Get(key, defaultValue); 
        }

        public float Get(string key, float defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public long Get(string key, long defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public void Set(string key, string value)
        {
            Preferences.Get(key, value);
        }

        public void Set(string key, bool value)
        {
            Preferences.Get(key, value);
        }

        public void Set(string key, int value)
        {
            Preferences.Get(key, value);
        }

        public void Set(string key, double value)
        {
            Preferences.Get(key, value);
        }

        public void Set(string key, float value)
        {
            Preferences.Get(key, value);
        }

        public void Set(string key, long value)
        {
            Preferences.Get(key, value);
        }

        public bool ContainsKey(string key, string sharedName)
        {
            return Preferences.ContainsKey(key, sharedName);
        }

        public void Remove(string key, string sharedName)
        {
            Preferences.Remove(key, sharedName);
        }

        public void Clear(string sharedName)
        {
            Preferences.Clear(sharedName);
        }

        public string Get(string key, string defaultValue, string sharedName)
        {
            return Preferences.Get(key, defaultValue, sharedName);
        }

        public bool Get(string key, bool defaultValue, string sharedName)
        {
            return Preferences.Get(key, defaultValue, sharedName);
        }

        public int Get(string key, int defaultValue, string sharedName)
        {
            return Preferences.Get(key, defaultValue, sharedName);
        }

        public double Get(string key, double defaultValue, string sharedName)
        {
            return Preferences.Get(key, defaultValue, sharedName);
        }

        public float Get(string key, float defaultValue, string sharedName)
        {
            return Preferences.Get(key, defaultValue, sharedName);
        }

        public long Get(string key, long defaultValue, string sharedName)
        {
            return Preferences.Get(key, defaultValue, sharedName);
        }

        public void Set(string key, string value, string sharedName)
        {
            Preferences.Set(key, value, sharedName);
        }

        public void Set(string key, bool value, string sharedName)
        {
            Preferences.Set(key, value, sharedName);
        }

        public void Set(string key, int value, string sharedName)
        {
            Preferences.Set(key, value, sharedName);
        }

        public void Set(string key, double value, string sharedName)
        {
            Preferences.Set(key, value, sharedName);
        }

        public void Set(string key, float value, string sharedName)
        {
            Preferences.Set(key, value, sharedName);
        }

        public void Set(string key, long value, string sharedName)
        {
            Preferences.Set(key, value, sharedName);
        }

        public DateTime Get(string key, DateTime defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public void Set(string key, DateTime value)
        {
            Preferences.Set(key, value);
        }

        public DateTime Get(string key, DateTime defaultValue, string sharedName)
        {
            return Preferences.Get(key, defaultValue, sharedName);
        }

        public void Set(string key, DateTime value, string sharedName)
        {
            throw new NotImplementedException();
        }
    }
}
