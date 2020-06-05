using SFML.Graphics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace VrekBoy.Assets
{
    public class Loader
    {
        private static AssetManager<SFML.Graphics.Texture> Textures;
        private static AssetManager<SFML.Graphics.Font> Fonts;


        public static void Initialize()
        {
            Textures = new AssetManager<Texture>();
            Fonts = new AssetManager<Font>();
        }

        /// <summary>
        /// This method loads an asset defined by a string name via a file into memory
        /// </summary>
        /// <param name="assetType">The type of asset to load</param>
        /// <param name="name">The name used to retrieve the loaded asset</param>
        /// <param name="filePath">The path of the file</param>
        public static bool Load(Assets.AssetType assetType, string name, string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException($"File {filePath} was not found!");

            switch (assetType)
            {
                case AssetType.Texture:
                    var sfTexture = new Texture(filePath);
                    Textures.Add(sfTexture, name);
                    return true;
                case AssetType.Font:
                    var sfFont = new Font(filePath);
                    Fonts.Add(sfFont, name);
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Method to return a previously loaded asset from memory
        /// </summary>
        /// <param name="type">The type of asset</param>
        /// <param name="name">The name of the corrosponding asset</param>
        private static object Get(AssetType type, string name)
        {
            switch (type)
            {
                case AssetType.Texture:
                    return Textures.Assets[name];
                case AssetType.Font:
                    return Fonts.Assets[name];
            }
            return null;
        }

        public static Texture GetTexture(string name) => (Texture)Get(AssetType.Texture, name);
        public static Font GetFont(string name) => (Font)Get(AssetType.Font, name);

        public static void Dispose()
        {
            foreach (var asset in Textures.Assets.Values)
                asset.Dispose();
            foreach (var asset in Fonts.Assets.Values)
                asset.Dispose();
            Console.WriteLine("Assets disposed");
        }
    }
}
