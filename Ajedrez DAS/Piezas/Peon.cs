using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Peon : Pieza
    {
        public Peon(bool color) : base(color) 
        {
        }

        //public override string ImagenFicha()
        //{
        //    return "Piezas\\Imagenes\\Pb.png";
        //}

        public override List<List<Posicion>> MovimientosPosibles(Posicion p)
        {
            List<List<Posicion>> Movs = new List<List<Posicion>>();
            List<Posicion> MovsPosibles = new List<Posicion>();
            int direccion = color ? -1 : 1;
            //aclaracion: en este caso [0] lo uso para "y", mientras que [1] para "x"
            int[][] movsPosiblesPeon =
            {
                new int[] { direccion, 0, },
                new int[] { direccion, 1, },
                new int[] { direccion, -1,}
            };

            int colInicial = p.x;
            int rowInicial = p.y;

            foreach (int[] movs in movsPosiblesPeon) 
            {
                int colDestino = colInicial + movs[1];
                int rowDestino = rowInicial + movs[0];

                if (rowDestino >= 0 && rowDestino < 8 && colDestino >= 0 && colDestino < 8)
                {
                    MovsPosibles.Add(new Posicion(rowDestino, colDestino));
                }
            }

            Movs.Add(MovsPosibles);

            return Movs;
        }
    }
}