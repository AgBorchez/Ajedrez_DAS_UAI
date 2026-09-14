using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ajedrez_DAS
{
    public abstract class Pieza 
    {
		public Pieza(bool color)  // 0 blanco, 1 negro
		{
			this.color = color;
		}
		public readonly bool color;

        //recordatorio: todas las piezas tienen un patron de movimiento, es posible validarlo matematicamente y es responsabilidad
        //de la pieza validar sus movimientos (su patron), la casilla se encargara de ejecutar el movimiento interactuando con otra casilla
        public abstract List<List<Posicion>> MovimientosPosibles(Posicion p);
	}
}