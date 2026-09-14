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
        private Jugador[] jugadores = new Jugador[2];
        public event DelPintarCasilla PintarCasilla;
        public event DelInicializarCasilla asociarcasilla;
        public event DelEventoFront ActualizarImagen;

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
        }

        public List<Posicion> SeleccionarCasilla(Casilla c)
        {
            if(c.PiezaEnCasilla != null)
            {
               return tablero.CalcularMovsValidos(c);
            }
            else
            {
                return null;
            }
        }
       
    }
}