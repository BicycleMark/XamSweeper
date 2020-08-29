using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sweeper.Infrastructure
{
    public class BaseModel : BindableBase, IPropertyRepository
    {
        IPropertyRepository repository;
        public BaseModel(IPropertyRepository repo)
        {
            repository = repo;
            
        }

        public void Clear()
        {
            repository.Clear(); ;
        }

        public void Clear(string sharedName)
        {
            repository.Clear(sharedName);
        }

        public bool ContainsKey(string key)
        {
            return repository.ContainsKey(key);
        }

        public bool ContainsKey(string key, string sharedName)
        {
            return repository.ContainsKey(key, sharedName);
        }

        public string Get(string key, string defaultValue)
        {
            return repository.Get(key, defaultValue);
        }

        public bool Get(string key, bool defaultValue)
        {
            return repository.Get(key, defaultValue); 
        }

        public int Get(string key, int defaultValue)
        {
            return repository.Get(key, defaultValue);
        }

        public double Get(string key, double defaultValue)
        {
            return repository.Get(key, defaultValue);
        }

        public float Get(string key, float defaultValue)
        {
            return repository.Get(key, defaultValue);
        }

        public long Get(string key, long defaultValue)
        {
            return repository.Get(key, defaultValue);
        }

        public string Get(string key, string defaultValue, string sharedName)
        {
            return repository.Get(key, defaultValue);
        }

        public bool Get(string key, bool defaultValue, string sharedName)
        {
            return repository.Get(key, defaultValue);
        }

        public int Get(string key, int defaultValue, string sharedName)
        {
            return repository.Get(key, defaultValue);
        }

        public double Get(string key, double defaultValue, string sharedName)
        {
            return repository.Get(key, defaultValue);
        }

        public float Get(string key, float defaultValue, string sharedName)
        {
            return repository.Get(key, defaultValue);
        }

        public long Get(string key, long defaultValue, string sharedName)
        {
            return repository.Get(key, defaultValue);
        }

        public DateTime Get(string key, DateTime defaultValue)
        {
            return repository.Get(key, defaultValue);
        }

        public DateTime Get(string key, DateTime defaultValue, string sharedName)
        {
            return repository.Get(key, defaultValue);
        }

        public string GetPrivatePreferencesSharedName(string feature)
        {
            return repository.GetPrivatePreferencesSharedName(feature);
        }

        public void Remove(string key)
        {
            repository.Remove(key);
        }

        public void Remove(string key, string sharedName)
        {
            repository.Remove(key,sharedName);
        }

        public void Set(string key, string value)
        {
            repository.Set(key,value);
        }

        public void Set(string key, bool value)
        {
            repository.Set(key, value);
        }

        public void Set(string key, int value)
        {
            repository.Set(key, value);
        }

        public void Set(string key, double value)
        {
            repository.Set(key, value);
        }

        public void Set(string key, float value)
        {
            repository.Set(key, value);
        }

        public void Set(string key, long value)
        {
            repository.Set(key, value);
        }

        public void Set(string key, string value, string sharedName)
        {
            repository.Set(key, value);
        }

        public void Set(string key, bool value, string sharedName)
        {
            repository.Set(key, value);
        }

        public void Set(string key, int value, string sharedName)
        {
            repository.Set(key, value);
        }

        public void Set(string key, double value, string sharedName)
        {
            repository.Set(key, value);
        }

        public void Set(string key, float value, string sharedName)
        {
            repository.Set(key, value);
        }

        public void Set(string key, long value, string sharedName)
        {
            repository.Set(key, value);
        }

        public void Set(string key, DateTime value)
        {
            repository.Set(key, value);
        }

        public void Set(string key, DateTime value, string sharedName)
        {
            repository.Set(key, value);
        }
    }
}
