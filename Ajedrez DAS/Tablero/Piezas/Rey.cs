using Ajedrez_DAS.Posicionamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Rey : Pieza
    {
        public Rey(ColorAjedrez color): base(color) 
        {
        }

        private bool enroque;

        public bool Enroque
        {
            get { return enroque; }
        }

        public override List<List<Movimiento>> MovimientosPosibles(Posicion p)
        {
            List<List<Movimiento>> Movs = new List<List<Movimiento>>(8);

            int colOrigen = p.x;
            int rowOrigen = p.y;

            //no es posible obtener el rango completo de movimiento espejando algunos, entonces decidi poner todos los movimientos posibles
            //en este caso [0] es "x" y [1] es "y"
            int[][] MovsPosiblesRey =
            {
                new int[] {  0,  1 }, 
                new int[] {  0, -1 }, 
                new int[] { -1,  0 }, 
                new int[] {  1,  0 }, 
                new int[] { -1,  1 }, 
                new int[] {  1,  1 }, 
                new int[] { -1, -1 }, 
                new int[] {  1, -1 }  
            };

            foreach (int[] movs in MovsPosiblesRey)
            {
                List<Movimiento> MovsPosibles = new List<Movimiento>(1);
                int colDestino = colOrigen + movs[0];
                int rowDestino = rowOrigen + movs[1];

                if(rowDestino >= 0 && rowDestino < 8 && colDestino >= 0 && colDestino < 8)
                {
                    Posicion posicionDestino = new Posicion(rowDestino, colDestino);
                    MovsPosibles.Add(new Movimiento(posicionDestino, TipoMov.Ambos));
                }
                Movs.Add(MovsPosibles);
            }

            

            return Movs;
        }

        public override void Mover()
        {
            enroque = false;
        }
    }
}