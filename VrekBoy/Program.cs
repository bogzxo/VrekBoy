using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrekBoy
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Engine.Instance.RunInstance();
        }
    }
}
