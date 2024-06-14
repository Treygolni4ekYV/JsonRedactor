using JsonReaderLib.Enums;
using JsonReaderLib.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace JsonReaderLib
{
    public class JsonFile
    {
        private readonly string _fullPath;
        public string FullPath { get { return _fullPath; } }

        public JsonFile(string path)
        {
            _fullPath = path;
        }

        public bool ContainItem(string itemName)
        {
            JObject jsonObject = JObject.Parse(File.ReadAllText(_fullPath));
            return jsonObject[itemName] == null ? false : true;
        }
    }
}
