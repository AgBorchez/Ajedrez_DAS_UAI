using Ajedrez_DAS.Posicionamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Caballo : Pieza
    {
        public Caballo(ColorAjedrez color): base(color) 
        {
        }

        //public override string ImagenFicha()
        //{
        //    return "Piezas\\Imagenes\\Cb.png";
        //}

        public override List<List<Movimiento>> MovimientosPosibles(Posicion p)
        {
            List<List<Movimiento>> listaMovsPosibles = new List<List<Movimiento>>(8);

            int[][] saltosCaballo = new int[][]
            {
                new int[] { -2, -1 },
                new int[] { -2,  1 },
                new int[] { -1, -2 },
                new int[] { -1,  2 },
                new int[] {  1, -2 },
                new int[] {  1,  2 },
                new int[] {  2, -1 },
                new int[] {  2,  1 }
            };

            foreach (var salto in saltosCaballo)
            {
                int nuevaFila = p.y + salto[0];
                int nuevaColumna = p.x + salto[1];

                if (nuevaFila >= 0 && nuevaFila < 8 && nuevaColumna >= 0 && nuevaColumna < 8)
                {
                    Posicion posicionDestino = new Posicion(nuevaFila, nuevaColumna);
                    listaMovsPosibles.Add(new List<Movimiento> { new Movimiento(posicionDestino, TipoMov.Ambos) });
                }
            }

            return listaMovsPosibles;
        }
    }
}