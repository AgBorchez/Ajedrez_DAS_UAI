using Ajedrez_DAS.Posicionamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ajedrez_DAS
{
    public class Alfil : Pieza
    {
        public Alfil(ColorAjedrez color) : base(color) 
        {
        }

        //public override string ImagenFicha()
        //{
        //    return "Piezas\\Imagenes\\Ab.png";
        //}

        public override List<List<Movimiento>> MovimientosPosibles(Posicion p)
        {
            {
                List<List<Movimiento>> movimientosValidos = new List<List<Movimiento>>();

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
                    List<Movimiento> MovsValidosDireccion = new List<Movimiento>();

                    int nuevaFila = p.y + pasoFila;
                    int nuevaColumna = p.x + pasoColumna;

                    while (nuevaFila >= 0 && nuevaFila < 8 && nuevaColumna >= 0 && nuevaColumna < 8)
                    {
                        Posicion posicionDestino = new Posicion(nuevaFila, nuevaColumna);
                        MovsValidosDireccion.Add(new Movimiento(posicionDestino, TipoMov.Ambos));
                        
                        
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