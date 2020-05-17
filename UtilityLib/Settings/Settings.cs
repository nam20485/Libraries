using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Libraries.UtilityLib.Xml;
using Libraries.UtilityLib.Windows;
using Libraries.UtilityLib.Notification;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Diagnostics.Contracts;
using Libraries.UtilityLib.Storage;

namespace Libraries.UtilityLib.Settings
{
    public abstract class Settings<T, S, R> : FormattedFile<T, S, R>
        where T : Settings<T, S, R>, new()
        where S : Serializer<T>, new()
        where R : FileStorage<T, S>, new()
    {
        protected const string settingsExtension = ".settings";

        public Settings()
            : base(WinFormUtils.GetEntryAssemblyFilename(false) + settingsExtension)
        {
        }

        public new static T Load(Uri path)
        {
            string f = WinFormUtils.GetEntryAssemblyFilename(false) + settingsExtension;
            //string f = Filename; // static- not set yet
            T t = FormattedFile<T, S, R>.Load(path, f);
            if (t == FormattedFile<T, S, R>.Empty)
            {
                t = new T();
                t.InitDefaults();
            }
            return t;
        }

        public static T Load()
        {
            return Load(Constants.CurrentDirectory);
        }

        public override void Save()
        {
            Save(Constants.CurrentDirectory.Append(Filename));
        }      

        protected abstract void InitDefaults();
    }
}
