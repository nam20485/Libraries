using System;
using Microsoft.CSharp;
using System.Collections.Generic;

namespace Libraries.UtilityLib.Json
{
    public class JsonApiRoot : JsonDictionary<string, Uri>
    {
        public JsonApiRoot(string json)
            : base(json)
        {
        }

        public KeyCollection ApiRoots   { get { return Keys; } }
        public ValueCollection Urls     { get { return Values; } }
    }
}