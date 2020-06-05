using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrekBoy
{
    public class DeltaTimeCalculator
    {
        private DateTime dtPrevFrame;
        public float GetDelta()
        {
            var now = DateTime.Now;
            var delta = (float)now.Subtract(dtPrevFrame).Milliseconds / 60.0f;
            dtPrevFrame = now;
            return delta;
        }
    }
}
