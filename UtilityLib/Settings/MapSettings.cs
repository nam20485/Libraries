using System;
using Libraries.UtilityLib.Xml;
using System.Collections.Generic;
using Libraries.UtilityLib.Settings;
using Libraries.UtilityLib.Storage;
using Libraries.UtilityLib.Notification;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Diagnostics.Contracts;

namespace Libraries.UtilityLib
{
   public abstract class MapSettings<T, TSerializer, TStorage> : Settings<T, TSerializer, TStorage>
        where T : MapSettings<T, TSerializer, TStorage>, new()
        where TSerializer : Serializer<T>, new()
        where TStorage : FileStorage<T, TSerializer>, new()
    {
        public Dictionary<string, object> Map { get; protected set; }

        public MapSettings()
        {
            Map = new Dictionary<string, object>();
        }

        protected void Set(string name, object val)
        {
            Map[name] = val;
            NotifyPropertyChanged(name);
        }

        protected object Get(string name)
        {
            return Map[name];
        }

        public TValue Get<TValue>(string name)
        {
            return (TValue) Get(name);
        }

        public void Set<TValue>(string name, TValue val)
        {
            //Set(name, val);
            Map[name] = val;
            NotifyPropertyChanged(name);
        }

        public object this [string name]
        {
            get { return Get(name); }
            set { Set(name, value); }
        }
    }
}

