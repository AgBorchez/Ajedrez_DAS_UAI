using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Partida
    {
        private Tablero tablero = new Tablero();
        //private Jugador[] jugadores = new Jugador[2];
        private ColorAjedrez JugadorActual;
        private List<Posicion> MovimientosValidosAcutales;
        private Casilla CasillaSelec;
        public event DelInicializarCasilla asociarcasilla;
        public event DelActualizarImagenes ActualizarImagen;

        public ColorAjedrez Color { get { return JugadorActual; } }
        public Tablero Tablero { get { return tablero; } }
        public void IniciarJuego()
        {
            tablero.InicializarTablero();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    asociarcasilla?.Invoke(tablero.Casillas[y, x], y, x);
                }
            }

            JugadorActual = ColorAjedrez.Blanco;
        }

        public List<Posicion> SeleccionarCasilla(Casilla c)
        {
            if(c.PiezaEnCasilla != null && c.PiezaEnCasilla.color == JugadorActual) 
            { 
                CasillaSelec = c;
                if(c.PiezaEnCasilla != null)
                {
                    MovimientosValidosAcutales = tablero.CalcularMovsValidos(c);
                    return MovimientosValidosAcutales;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public void EjecutarMovimiento(Casilla to)
        {
            if(MovimientosValidosAcutales.Contains(new Posicion(to.y, to.x)))
            {       
                tablero.EjecutarMovimiento(CasillaSelec, to);
                ActualizarImagen?.Invoke();
                ActualizarTurno();
            }
        }

        private void ActualizarTurno()
        {
            if (JugadorActual == ColorAjedrez.Blanco) 
            {
                JugadorActual = ColorAjedrez.Negro;
                return;
            }
            if (JugadorActual == ColorAjedrez.Negro)
            {
                JugadorActual = ColorAjedrez.Blanco;
                return;
            }
        }



    }
}