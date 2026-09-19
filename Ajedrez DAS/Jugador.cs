using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Jugador
    {
		public Jugador(ColorAjedrez color) 
		{
			this.Color = color;
		}
		public readonly ColorAjedrez Color;

	}
}