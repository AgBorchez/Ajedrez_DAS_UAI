using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Alfil : Pieza
    {
        public Alfil(bool color) : base(color) 
        {
        }

        //public override string ImagenFicha()
        //{
        //    return "Piezas\\Imagenes\\Ab.png";
        //}

        public override List<List<Posicion>> MovimientosPosibles(Posicion p)
        {
            {
                List<List<Posicion>> movimientosValidos = new List<List<Posicion>>();

                int[][] direcciones = new int[][]
                {
                    new int[] { 1, 1 },   
                    new int[] { 1, -1 },  
                    new int[] { -1, 1 },  
                    new int[] { -1, -1 }  
                };

                foreach (var dir in direcciones)
                {
                    int pasoFila = dir[0];
                    int pasoColumna = dir[1];
                    List<Posicion> MovsValidosDireccion = new List<Posicion>();

                    int nuevaFila = p.y + pasoFila;
                    int nuevaColumna = p.x + pasoColumna;

                    while (nuevaFila >= 0 && nuevaFila < 7 && nuevaColumna >= 0 && nuevaColumna < 7)
                    {
                        MovsValidosDireccion.Add(new Posicion(nuevaFila, nuevaColumna));
                        
                        
                        nuevaFila += pasoFila;
                        nuevaColumna += pasoColumna;
                    }

                    movimientosValidos.Add(MovsValidosDireccion);
                }

                return movimientosValidos;
            }
        }
    }
}