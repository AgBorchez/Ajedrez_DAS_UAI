using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Caballo : Pieza
    {
        public Caballo(bool color): base(color) 
        {
        }

        //public override string ImagenFicha()
        //{
        //    return "Piezas\\Imagenes\\Cb.png";
        //}

        public override List<List<Posicion>> MovimientosPosibles(Posicion p)
        {
            List<List<Posicion>> ListaMovsPosibles = new List<List<Posicion>>();
            List<Posicion> MovsPosibles = new List<Posicion>();

            int[][] MovsPosiblesCaballo =
            {
                new int[] {1, 2},
                new int[] {-1, 2},
                new int[] {-2, 1},
                new int[] {-2, -1},
            };

            int colOrigen = p.x;
            int rowOrigen = p.y;


            foreach(var movs in MovsPosiblesCaballo)
            {
                int colDestino = colOrigen + movs[0];
                int colDestinoEspejada = colOrigen + movs[1];
                int rowDestino = rowOrigen + movs[1];
                int rowDestinoEspejada = rowOrigen + movs[0];

                if(rowDestino >= 0 && rowDestino < 7 && colDestino >= 0 && colDestino < 7)
                {
                    MovsPosibles.Add(new Posicion(rowDestino, colDestino));
                }
                if (rowDestinoEspejada >= 0 && rowDestinoEspejada < 7 && colDestinoEspejada >= 0 && colDestinoEspejada < 7)
                {
                    MovsPosibles.Add(new Posicion(rowDestinoEspejada, colDestinoEspejada));
                }

            }

            ListaMovsPosibles.Add(MovsPosibles);

            return ListaMovsPosibles;
        }
    }
}