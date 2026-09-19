using Ajedrez_DAS.Posicionamiento;
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
        public Peon(ColorAjedrez color) : base(color) 
        {
        }

        private bool primerMov = true;
        public bool PrimerMov { get { return primerMov; } }

        public void ConsumirPrimerMov() { primerMov  = false; }
        

        public override List<List<Movimiento>> MovimientosPosibles(Posicion p)
        {
            // despues de probar un poco el juego, me di cuenta que el peon es de la unicas (si no es que la unica) pieza que 
            // tiene un movimiento variable dependiendo x factores, por eso pense en la siguiente distribucion de movimientos
            List<List<Movimiento>> Movs = new List<List<Movimiento>>(3);
            List<Movimiento> MovsAvance = new List<Movimiento>(1);
            int direccion = ((color == ColorAjedrez.Blanco) ? -1 : 1);
            Posicion posicionDestino = new Posicion(p.y + direccion, p.x);

            MovsAvance.Add(new Movimiento(posicionDestino, TipoMov.Avance));

            if (primerMov)
            {
                posicionDestino = new Posicion(p.y + (direccion * 2), p.x);
                // Al estar segundo en la misma lista, el tablero sabe que 
                // depende de que el paso anterior no esté bloqueado
                MovsAvance.Add(new Movimiento(posicionDestino, TipoMov.Avance));
            }
            Movs.Add(MovsAvance);

            // 2. diagonales de ataque (tablero las filtra)
            // izquierda

            if(p.x - 1 != -1) { 

                posicionDestino = new Posicion(p.y + direccion, p.x - 1);
            
                Movs.Add(new List<Movimiento> { new Movimiento(posicionDestino, TipoMov.Ataque) });
            
            }
            // derecha

            if (p.x + 1 != 8)
            {
                posicionDestino = new Posicion(p.y + direccion, p.x + 1);

                Movs.Add(new List<Movimiento> { new Movimiento(posicionDestino, TipoMov.Ataque) });

                // orden de movimientos: avance normal, avance doble (primer mov), ataque izquierdo, ataque derecho
                
            }
            return Movs;
        }

        public override void Mover()
        {
            primerMov = false;
        }

    
    }
}