using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Reina : Pieza
    {
        public Reina(bool color) : base(color) 
        {
        }

        public override List<List<Posicion>> MovimientosPosibles(Posicion p)
        {
            List<List<Posicion>> movimientosPosibles = new List<List<Posicion>>();

            int filaOrigen = p.y;
            int colOrigen = p.x;
            int[] movsPosiblesRec = { 1, -1 };
            int[][] movsPosiblesDiag = new int[][]
            {
                    new int[] { 1, 1 },
                    new int[] { 1, -1 },
                    new int[] { -1, 1 },
                    new int[] { -1, -1 }
            };

            for (int i = 0; i < movsPosiblesRec.Length; i++)
            {
                int FilaDestino = filaOrigen + movsPosiblesRec[i];
                int ColDestino = colOrigen + movsPosiblesRec[i];
                List<Posicion> MovsValidosDireccion = new List<Posicion>();

                while ((FilaDestino >= 0 && FilaDestino <= 7) && (ColDestino >= 0 && ColDestino <= 7))
                {
                    MovsValidosDireccion.Add(new Posicion(FilaDestino, ColDestino));

                    ColDestino += movsPosiblesRec[i];
                    FilaDestino += movsPosiblesRec[i];
                }

                movimientosPosibles.Add(MovsValidosDireccion);
            }

            

            foreach (var dir in movsPosiblesDiag)
            {
                List<Posicion> MovsValidosDireccion = new List<Posicion>();
                int pasoFila = dir[0];
                int pasoColumna = dir[1];

                int nuevaFila = p.y + pasoFila;
                int nuevaColumna = p.x + pasoColumna;

                while (nuevaFila >= 0 && nuevaFila < 7 && nuevaColumna >= 0 && nuevaColumna < 7)
                {
                    MovsValidosDireccion.Add(new Posicion(nuevaFila, nuevaColumna));


                    nuevaFila += pasoFila;
                    nuevaColumna += pasoColumna;
                }

                movimientosPosibles.Add(MovsValidosDireccion);
            }

            return movimientosPosibles;
        }
    }
}