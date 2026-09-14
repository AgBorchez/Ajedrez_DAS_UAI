using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ajedrez_DAS
{
    public class Torre : Pieza
    {
        public Torre(bool color) : base(color) 
        {
        }

        //public override string ImagenFicha()
        //{
        //    return "Piezas\\Imagenes\\Tb.png";
        //}

        public override List<List<Posicion>> MovimientosPosibles(Posicion p)
        {
            List<List<Posicion>> movimientosPosibles = new List<List<Posicion>>();

            int filaOrigen = p.y;
            int colOrigen = p.x;
            int[] movsPosibles = { 1, -1 };

            for (int i = 0; i < movsPosibles.Length; i++) 
            {
                int FilaDestino = filaOrigen + movsPosibles[i];
                int ColDestino = colOrigen + movsPosibles[i];
                List<Posicion> movsPosiblesDireccion = new List<Posicion>();

                while ((FilaDestino >= 0 && FilaDestino <= 7) && (ColDestino >= 0 && ColDestino <= 7)) 
                {
                    movsPosiblesDireccion.Add(new Posicion(FilaDestino, ColDestino));

                    ColDestino += movsPosibles[i];
                    FilaDestino += movsPosibles[i];
                }

                movimientosPosibles.Add(movsPosiblesDireccion);
            }

            return movimientosPosibles;
        }
    }
}