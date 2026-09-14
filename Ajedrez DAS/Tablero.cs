using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ajedrez_DAS
{

    public class Tablero
    {
        private Casilla[,] casillas = new Casilla[8,8];

		public Casilla[,] Casillas
		{
			get { return casillas; }
		}
        
        public void InicializarTablero()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    casillas[y, x] = new Casilla(new Posicion(y, x));
                }
            }

            asignarPiezasInicio();

        }

        private void asignarPiezasInicio()
        {
            Type[] plantillaPiezas = {
                typeof(Torre), typeof(Caballo), typeof(Alfil), typeof(Reina),
                typeof(Rey), typeof(Alfil), typeof(Caballo), typeof(Torre)
            };

            for (int col = 0; col < 8; col++)
            {
                casillas[0, col].PiezaEnCasilla = (Pieza)Activator.CreateInstance(plantillaPiezas[col], new object[] { false });
                casillas[1, col].PiezaEnCasilla = new Peon(false);
                casillas[6, col].PiezaEnCasilla = new Peon(true);
                casillas[7, col].PiezaEnCasilla = (Pieza)Activator.CreateInstance(plantillaPiezas[col], new object[] { true });
            }
        }

        public List<Posicion> CalcularMovsValidos(Casilla c)
        {
            List<List<Posicion>> movimientosPosibles = c.CalcularMovimientos();
            List<Posicion> MovimientosValidos = new List<Posicion>();

            foreach(List<Posicion> ListaMovimientos in movimientosPosibles)
            {
                foreach(Posicion Movimiento in ListaMovimientos)
                {
                    Pieza pCasillaDestino = this.casillas[Movimiento.y, Movimiento.x].PiezaEnCasilla;
                    if (pCasillaDestino == null)
                    {
                        MovimientosValidos.Add(Movimiento);
                    }
                    else
                    {
                        if(pCasillaDestino.color != c.PiezaEnCasilla.color) { 
                            MovimientosValidos.Add(Movimiento);
                        }
                        break;
                    }
                }
            }

            return MovimientosValidos;
        }
        public void EjecutarMovimiento(Casilla from, Casilla to)
        {
            
        }

        public void PartidaNueva()
        {

        }

        /*

        RFNs:
        1. jaque, jaque mate.
        2. movimientos validos e invalidos
        3. comer piezas contrarias
        4.      
         
         
         */



    }
}