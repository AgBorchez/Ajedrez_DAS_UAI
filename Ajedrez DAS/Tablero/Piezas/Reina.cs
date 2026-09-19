using Ajedrez_DAS.Posicionamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Reina : Pieza
    {
        public Reina(ColorAjedrez color) : base(color) 
        {
        }

        public override List<List<Movimiento>> MovimientosPosibles(Posicion p)
        {
            List<List<Movimiento>> movimientosPosibles = new List<List<Movimiento>>();

            int[][] direcciones = new int[][]
            {
                new int[] { 1, 0 },   
                new int[] { -1, 0 },  
                new int[] { 0, 1 },   
                new int[] { 0, -1 },  
                new int[] { 1, 1 },
                new int[] { 1, -1 },
                new int[] { -1, 1 },
                new int[] { -1, -1 }
            };

            foreach (var dir in direcciones)
            {
                int pasoFila = dir[0];
                int pasoColumna = dir[1];
                List<Movimiento> rayo = new List<Movimiento>();

                int nuevaFila = p.y + pasoFila;
                int nuevaColumna = p.x + pasoColumna;

                while (nuevaFila >= 0 && nuevaFila < 8 && nuevaColumna >= 0 && nuevaColumna < 8)
                {

                    Posicion posicionDestino = new Posicion(nuevaFila, nuevaColumna);
                    rayo.Add(new Movimiento(posicionDestino, TipoMov.Ambos));
                    nuevaFila += pasoFila;
                    nuevaColumna += pasoColumna;
                }

                movimientosPosibles.Add(rayo);
            }

            return movimientosPosibles;
        }
    }
}