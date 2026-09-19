using Ajedrez_DAS.Posicionamiento;
using System;

using System.Collections.Generic;
using System.IO;

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
                casillas[0, col].PiezaEnCasilla = (Pieza)Activator.CreateInstance(plantillaPiezas[col], new object[] { ColorAjedrez.Negro });
                casillas[1, col].PiezaEnCasilla = new Peon(ColorAjedrez.Negro);
                casillas[6, col].PiezaEnCasilla = new Peon(ColorAjedrez.Blanco);
                casillas[7, col].PiezaEnCasilla = (Pieza)Activator.CreateInstance(plantillaPiezas[col], new object[] { ColorAjedrez.Blanco });
            }
        }

        public List<Posicion> CalcularMovsValidos(Casilla c)
        {
            List<List<Movimiento>> movimientosPosibles = c.CalcularMovimientos();
            List<Posicion> MovimientosValidos = new List<Posicion>();

            foreach(List<Movimiento> LineaDeMovimiento in movimientosPosibles)
            {
                foreach(Movimiento MovimientoIndividual in LineaDeMovimiento)
                {
                    Pieza pCasillaDestino = this.casillas[MovimientoIndividual.pos.y, MovimientoIndividual.pos.x].PiezaEnCasilla;
                    //int direccion = (pCasillaDestino.color ? 1 : -1);
                    if (pCasillaDestino == null && (MovimientoIndividual.TipoMov == TipoMov.Avance || MovimientoIndividual.TipoMov == TipoMov.Ambos))
                    {
                        MovimientosValidos.Add(MovimientoIndividual.pos);
                    }
                    else if(pCasillaDestino != null) 
                    {
                        if(pCasillaDestino.color != c.PiezaEnCasilla.color && (MovimientoIndividual.TipoMov == TipoMov.Ambos || MovimientoIndividual.TipoMov == TipoMov.Ataque)) { 
                            MovimientosValidos.Add(MovimientoIndividual.pos);
                        }
                        break;
                    }

                }
            }

            return MovimientosValidos;
        }
        public void EjecutarMovimiento(Casilla from, Casilla to)
        {
            if(to.PiezaEnCasilla == null || (to.PiezaEnCasilla.color != from.PiezaEnCasilla.color)) 
            {
                from.PiezaEnCasilla.Mover();

                from.MoverPieza(to);
            }
        }

        private void ManejarPeon(Peon p)
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