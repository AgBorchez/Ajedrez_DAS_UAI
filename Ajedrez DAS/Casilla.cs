using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Ajedrez_DAS
{
    public class Casilla
    {
		public Casilla(Posicion pos) 
		{
			this.posicion = pos; 
		}
		public Casilla(Pieza p, Posicion pos) : this (pos)
		{
			this.piezaEnCasilla = p;
		}
		private Pieza piezaEnCasilla = null;

		public Pieza PiezaEnCasilla
		{
			get { return piezaEnCasilla; }
			set { piezaEnCasilla = value; }
		}

		private Posicion posicion;

		public int x
		{
			get
			{
				return posicion.x;
			}
		}

        public int y
        {
            get
            {
                return posicion.y;
            }
        }

        public List<List<Posicion>> CalcularMovimientos()
		{
            List<List<Posicion>> movs = this.piezaEnCasilla.MovimientosPosibles(this.posicion);

			return movs;
		}


    }
}