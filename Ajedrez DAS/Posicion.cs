using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Posicion
    {
        public Posicion(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        public readonly int x;
        public readonly int y;
    }
}