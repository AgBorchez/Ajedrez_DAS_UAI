using Ajedrez_DAS.Posicionamiento;
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
		public Pieza(ColorAjedrez color)  // 1 blanco, 0 negro
		{
			this.color = color;
		}
		public readonly ColorAjedrez color;

		public virtual void Mover()
		{

		}


		//recordatorio: todas las piezas tienen un patron de movimiento, es posible validarlo matematicamente y es responsabilidad
		//de la pieza validar sus movimientos (su patron), la casilla se encargara de ejecutar el movimiento interactuando con otra casilla

		public abstract List<List<Movimiento>> MovimientosPosibles(Posicion p);
		//este metodo no va, esta mal planteada la resolucion del problema, ignoralo en las piezas hijas
	}
}