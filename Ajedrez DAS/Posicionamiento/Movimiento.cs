using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajedrez_DAS.Posicionamiento
{
    public class Movimiento
    {
        public Movimiento(Posicion p, TipoMov tipoMovimiento) 
        {
            this.pos = p;
            this.TipoMov = tipoMovimiento;
        }

        public readonly Posicion pos;

        public readonly TipoMov TipoMov;


    }
}
