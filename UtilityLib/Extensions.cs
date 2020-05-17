using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.Remoting;

namespace Libraries.UtilityLib
{
    public static class Extensions
    {
        public static bool OccursInRange(this TimeSpan time, TimeSpan begin, TimeSpan end)
        {
            return (time >= begin && time <= end);
        }   

        public static byte[] ToByteArray(this String s)
        {
            byte[] bytes = new byte[s.Length * sizeof(char)];
            Buffer.BlockCopy(s.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;        
        }

        public static string ToFileSystemPath(this Uri uri)
        {
            string s = string.Empty;

            //if (uri.IsWellFormedOriginalString())
            // we can't turn a non file:// (e.g. Http://) scheme into a local file system path
            if (uri.IsFile)
            {
                if (uri.IsAbsoluteUri)
                {
                    s = uri.LocalPath;
                } 
                else
                {
                    // relative to current working driectory
                    Uri workingDriectory = new Uri(Directory.GetCurrentDirectory());
                    Uri absolute = new Uri(workingDriectory, uri);
                    s = absolute.LocalPath;
                }
            }

            return s;
        }

        public static Uri FromFileSystemPath(this string path)
        {
            Uri uri = null;

            try
            {                
                if (path == ".")
                {
                    uri = new Uri(Directory.GetCurrentDirectory());
                }
                else
                {
                    uri = new Uri(path);
                }
            }
            catch (FormatException)
            {
                // relative path, prepend current working directory
                Uri workingDirectory = new Uri(Directory.GetCurrentDirectory());
                uri = new Uri(workingDirectory, path);
            }

            return uri;
        }

        public static Uri Append(this Uri baseUri, string append)
        {
            string path = Path.Combine(baseUri.LocalPath, append);
            return new Uri(path);
        }

        public static K GetKey<K, V>(this Dictionary<K, V> dictionary, V value) 
            where K : class
        {
            foreach (var key in dictionary.Keys)
            {
                if (dictionary[key].Equals(value))
                {
                    return key;
                }
            }
            return null;
        }

        public static K[] GetKeys<K, V>(this Dictionary<K, V> dictionary, V value) 
        {
            List<K> keys = new List<K>();
            foreach (var key in dictionary.Keys)
            {
                if (dictionary[key].Equals(value))
                {
                    keys.Add(key);
                }
            }
            return keys.ToArray();
        }

        public static KeyValuePair<K, V>[] GetItemsForValue<K, V>(this Dictionary<K, V> dictionary, V value) 
        {
            List<KeyValuePair<K, V>> items = new List<KeyValuePair<K, V>>();
            foreach (var key in dictionary.Keys)
            {
                V v = dictionary[key];
                if (v.Equals(value))
                {
                    items.Add(new KeyValuePair<K, V>(key, v));
                }
            }
            return items.ToArray();
        }

        public static KeyValuePair<K, V> GetItemForValue<K, V>(this Dictionary<K, V> dictionary, V value)         
        {
            foreach (var key in dictionary.Keys)
            {
                V v = dictionary[key];
                if (v.Equals(value))
                {
                    return new KeyValuePair<K, V>(key, v);
                }
            }
            return default(KeyValuePair<K, V>);
        }     
    }
}
