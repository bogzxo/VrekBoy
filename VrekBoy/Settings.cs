using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrekBoy
{
    public class Settings
    {
        /// <summary>
        /// Returns the width as requested by the settings of the user
        /// </summary>
        [JsonProperty]
        public uint Width { get; private set; }
        /// <summary>
        /// Returns the height as requested by the settings of the user
        /// </summary>
        [JsonProperty]
        public uint Height { get; private set; }
        /// <summary>
        /// Returns the width as update by the settings of the user
        /// </summary>
        [JsonProperty]
        public uint UpdateRate { get; private set; }
        /// <summary>
        /// Returns the refresh rate as requested by the settings of the user
        /// </summary>
        [JsonProperty]
        public uint RefrehRate { get; private set; }
        /// <summary>
        /// Returns the state of vsycn as requested by the settings of the user
        /// </summary>
        [JsonProperty]
        public bool VSync { get; private set; }
        /// <summary>
        /// Returns the default settings for the current environment
        /// </summary>
        [JsonIgnore]
        public static Settings Default
        {
            get
            {
                var settings = new Settings();

                settings.Width = 1280;
                settings.Height = 720;
                settings.RefrehRate = 60;
                settings.UpdateRate = 60;
                settings.VSync = true;

                return settings;
            }
        }
    }
}
