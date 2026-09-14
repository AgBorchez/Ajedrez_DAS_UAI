using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Jugador
    {
		public Jugador(bool color) 
		{
			this.color = color;
		}
		private bool color; // 1 negro, 0 blanco 

		public bool Color
		{
			get { return color; }
		}

	}
}