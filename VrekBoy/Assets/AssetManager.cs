using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrekBoy.Assets
{
    internal sealed class AssetManager<T>
    {
        /// <summary>
        /// The actual asset and name keypairs as a read only array
        /// </summary>
        public Dictionary<string, T> Assets { get; private set; }

        public AssetManager()
        {
            Assets = new Dictionary<string, T>();
        }

        /// <summary>
        /// Method to add a loaded object into this generic asset manager
        /// </summary>
        /// <param name="obj">The object</param>
        /// <param name="name">Its id as a string</param>
        public void Add(T obj, string name)
        {
            if (Assets.ContainsKey(name)) throw new Exception($"Asset with name {name} already exists!");
            Assets.Add(name, obj);
        }
    }
}
