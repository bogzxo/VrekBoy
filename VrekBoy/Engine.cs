using Newtonsoft.Json;
using SFML.Graphics;
using SFML.Utils;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VrekBoy.Assets;
using VrekBoy.Player;

namespace VrekBoy
{
    public class Engine
    {
        private static Engine _engine;
        public static Engine Instance
        {
            get
            {
                if (_engine == null) _engine = new Engine();
                return _engine;
            }
        }

        // PRIVATE
        private RenderWindow _renderWindow;
        private bool _running;
        private Settings _settings;
        private DeltaTimeCalculator _updateDelta;
        private RenderTexture _displayRenderTexture;
        // END PRIVATE

        // PUBLIC
        /// <summary>
        /// Returns the current render window instance (instantiated or not)
        /// </summary>
        public RenderWindow RenderWindow { get { return _renderWindow; } }
        /// <summary>
        /// Returns wether or not the game loop is active
        /// </summary>
        public bool Running { get { return _running; } }
        /// <summary>
        /// The currently used game screen
        /// </summary>
        public IGameScreen GameScreen { get; set; }
        // END PUBLIC

        /// <summary>
        /// Calling this method initializes all core engine components and loads all content into memory
        /// </summary>
        public void RunInstance(IGameScreen screen)
        {
            if (_running) throw new Exception("Game instance already running!");
            _running = true;

            // set game screen
            GameScreen = screen;

            // initialize and load settings
            // also creating them if they are not in place
            if (File.Exists("config/settings.json")) _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("config/settings.json"));
            else
            {
                if (!Directory.Exists("config")) Directory.CreateDirectory("config");

                _settings = Settings.Default;
                File.WriteAllText("config/settings.json", JsonConvert.SerializeObject(_settings));
            }

            // create main render window
            _renderWindow = new RenderWindow(new VideoMode(_settings.Width, _settings.Height), "VrekLife: VrekBoys Journy", Styles.Default);
            _renderWindow.SetFramerateLimit(_settings.RefrehRate);
            _renderWindow.SetVerticalSyncEnabled(_settings.VSync);

            _renderWindow.SetActive();

            // assign some basic events such as closing and resizing
            _renderWindow.Closed += (o, e) =>
            {
                _running = false;
            };

            // get the delta time calculators ready
            _updateDelta = new DeltaTimeCalculator();

            // initialize the assetmanager
            Loader.Initialize();

            // get the render texture ready
            _displayRenderTexture = new RenderTexture(_settings.Width, _settings.Height);

            // run the main game loop
            while (_running)
            {
                _renderWindow.DispatchEvents();
                if (!_renderWindow.HasFocus()) continue;

                LoopRender();
                LoopUpdate();
            }
        }

        private void LoopUpdate()
        {
            // get delta
            var delta = _updateDelta.GetDelta();

            // update screen
            GameScreen.Update(delta);
        }

        private void LoopRender()
        {
            // clear the frame buffer
            _displayRenderTexture.Clear(Color.Magenta);

            // draw the player
            _displayRenderTexture.Draw(GameScreen);

            // draw everything on screen
            _displayRenderTexture.Display();
            _renderWindow.Draw(new Sprite(_displayRenderTexture.Texture));

            // send to GPU
            _renderWindow.Display();
        }

        ~Engine()
        {
            // just to be thread safe incase i change the loop structure
            _running = false;

            if (RenderWindow != null)
                RenderWindow.Dispose();
            Assets.Loader.Dispose();
        }
    }
}
