using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public class NullRepo : IPropertyRepository
    {
        public void Clear()
        {
            
        }

        public void Clear(string sharedName)
        {
            
        }

        public bool ContainsKey(string key)
        {
            return false;
        }

        public bool ContainsKey(string key, string sharedName)
        {
            return false;
        }

        public string Get(string key, string defaultValue)
        {
            return defaultValue;
        }

        public bool Get(string key, bool defaultValue)
        {
            return defaultValue;
        }

        public int Get(string key, int defaultValue)
        {
            return defaultValue;
        }

        public double Get(string key, double defaultValue)
        {
            return defaultValue;
        }

        public float Get(string key, float defaultValue)
        {
            return defaultValue;
        }

        public long Get(string key, long defaultValue)
        {
            return defaultValue;
        }

        public string Get(string key, string defaultValue, string sharedName)
        {
            return defaultValue;
        }

        public bool Get(string key, bool defaultValue, string sharedName)
        {
            return defaultValue;
        }

        public int Get(string key, int defaultValue, string sharedName)
        {
            return defaultValue;
        }

        public double Get(string key, double defaultValue, string sharedName)
        {
            return defaultValue;
        }

        public float Get(string key, float defaultValue, string sharedName)
        {
            return defaultValue;
        }

        public long Get(string key, long defaultValue, string sharedName)
        {
            return defaultValue;
        }

        public DateTime Get(string key, DateTime defaultValue)
        {
            return defaultValue;
        }

        public DateTime Get(string key, DateTime defaultValue, string sharedName)
        {
            return defaultValue;
        }

        public string GetPrivatePreferencesSharedName(string feature)
        {
            return null;
        }

        public void Remove(string key)
        {
            
        }

        public void Remove(string key, string sharedName)
        {
            
        }

        public void Set(string key, string value)
        {
            
        }

        public void Set(string key, bool value)
        {
            
        }

        public void Set(string key, int value)
        {
            
        }

        public void Set(string key, double value)
        {
            
        }

        public void Set(string key, float value)
        {
            
        }

        public void Set(string key, long value)
        {
            
        }

        public void Set(string key, string value, string sharedName)
        {
            
        }

        public void Set(string key, bool value, string sharedName)
        {
            
        }

        public void Set(string key, int value, string sharedName)
        {
            
        }

        public void Set(string key, double value, string sharedName)
        {
            
        }

        public void Set(string key, float value, string sharedName)
        {
            
        }

        public void Set(string key, long value, string sharedName)
        {
            
        }

        public void Set(string key, DateTime value)
        {
            
        }

        public void Set(string key, DateTime value, string sharedName)
        {
            
        }
    }
}
