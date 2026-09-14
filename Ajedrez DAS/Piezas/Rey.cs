using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Rey : Pieza
    {
        public Rey(bool color): base(color) 
        {
        }

        //public override string ImagenFicha()
        //{
        //    return "Piezas\\Imagenes\\Rb.png";
        //}

        public override List<List<Posicion>> MovimientosPosibles(Posicion p)
        {
            List<Posicion> MovsPosibles = new List<Posicion>();

            int colOrigen = p.x;
            int rowOrigen = p.y;

            //no es posible obtener el rango completo de movimiento espejando algunos, entonces decidi poner todos los movimientos posibles
            //en este caso [0] es "x" y [1] es "y"
            int[][] MovsPosiblesRey =
            {
                new int[] {0, 1},
                new int[] {0, -1},
                new int[] {1, 0},
                new int[] {1, 0},
                new int[] {1, 1},
                new int[] {1, -1},
                new int[] {1, -1},
                new int[] {1, 1}
            };

            foreach (int[] movs in MovsPosiblesRey)
            {
                int colDestino = colOrigen + movs[0];
                int rowDestino = rowOrigen + movs[1];

                if(rowDestino >= 0 && rowDestino < 7 && colDestino >= 0 && colDestino < 7)
                {
                    MovsPosibles.Add(new Posicion(rowDestino, colDestino));
                }
            }

            List<List<Posicion>> Movs = new List<List<Posicion>>();
            Movs.Add(MovsPosibles);

            return Movs;
        }
    }
}