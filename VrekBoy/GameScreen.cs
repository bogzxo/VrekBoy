using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrekBoy
{
    public interface IGameScreen : Drawable
    {
        void Update(float delta);
        void Onload();
    }
}
