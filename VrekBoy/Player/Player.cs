using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VrekBoy.Assets;

namespace VrekBoy.Player
{
    public class Player : Drawable
    {
        private class PlayerVertexArray
        {
            private int _width, _height;
            public VertexArray VertexArray { get; private set; }

            public PlayerVertexArray(int width, int height)
            {
                this._width = width;
                this._height = height;

                VertexArray = new VertexArray(PrimitiveType.Quads, 4);
            }
            public void Update(Vector2f pos)
            {
                VertexArray[0] = new Vertex(new Vector2f(pos.X, pos.Y), new Vector2f(0, 0));
                VertexArray[1] = new Vertex(new Vector2f(pos.X + _width, pos.Y), new Vector2f(_width, 0));
                VertexArray[2] = new Vertex(new Vector2f(pos.X + _width, pos.Y + _height), new Vector2f(_width, _height));
                VertexArray[3] = new Vertex(new Vector2f(pos.X, pos.Y + _height), new Vector2f(0, _height));
            }
        }


        private static Player _player;
        public static Player Instance
        {
            get
            {
                if (_player == null) _player = new Player();
                return _player;
            }
        }

        /// <summary>
        /// The absolute position of the player
        /// </summary>
        public Vector2f Position { get; private set; }
        public Vector2i Size { get; private set; }
        /// <summary>
        /// Helper class for an object orientated aproach to updating a vertex array on the fly
        /// </summary>
        private PlayerVertexArray vxPlayer;
        public Player()
        {
            this.Position = new Vector2f(0, 0);
            this.Size = new Vector2i(32, 32);

            // load player texture
            Loader.Load(AssetType.Texture, "player", "test.png");

            vxPlayer = new PlayerVertexArray(Size.X, Size.Y);
        }

        public void Update(float delta)
        {
            // y movements
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                this.Position += new Vector2f(0, -10 * delta);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                this.Position += new Vector2f(0, 10 * delta);
            }

            // x movements
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                this.Position += new Vector2f(-10 * delta, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                this.Position += new Vector2f(10 * delta, 0);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            // draw the player
            vxPlayer.Update(this.Position);
            states.Texture = Loader.GetTexture("player");
            target.Draw(vxPlayer.VertexArray, states);
            states.Texture = null;
        }
    }
}
