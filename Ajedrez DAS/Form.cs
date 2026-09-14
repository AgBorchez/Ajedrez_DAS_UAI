using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ajedrez_DAS
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Partida juego = new Partida();
        private UCcasilla[,] Casillas = new UCcasilla[8,8];
        private Casilla casillaSeleccionada;
        List<UCcasilla> PosiblesMovimientos = new List<UCcasilla>();
        public Form()
        {
            InitializeComponent();
            juego.asociarcasilla += Inicializar_Casillas;
        }

        private void Inicializar_Casillas(Casilla c, int y, int x)
        {
            UCcasilla ucc = new UCcasilla((((x + y) % 2 != 0)) ? Color.Beige : Color.BurlyWood, c);
            int sizeCasilla = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 8;

            ucc.Location = new Point(x * sizeCasilla, y * sizeCasilla);
            ucc.Size = new Size(sizeCasilla, sizeCasilla);

            Casillas[y, x] = ucc;
            ucc.SeleccionarCasilla += CasillaSeleccionada;

            this.Controls.Add( ucc );
        }

        private void CasillaSeleccionada(Casilla c)
        {
            ResetearColores();

            List<Posicion> Movimientos = juego.SeleccionarCasilla(c);
            casillaSeleccionada = c;

            if(Movimientos != null)
            {
                Casillas[c.y, c.x].ActualizarColorFondo(Color.Yellow);
                PosiblesMovimientos = new List<UCcasilla>();
                foreach(Posicion mov in Movimientos)
                {
                    Casillas[mov.y, mov.x].ActualizarColorFondo(Color.Cyan);
                    PosiblesMovimientos.Add(Casillas[mov.y, mov.x]);
                }
            }
        }

        private void ResetearColores()
        {
            if (casillaSeleccionada != null)
            {
                Casillas[casillaSeleccionada.y, casillaSeleccionada.x].ActualizarColorFondo();
            }
            if (casillaSeleccionada != null)
            {
                foreach (UCcasilla ucc in PosiblesMovimientos)
                {
                    ucc.ActualizarColorFondo();
                }
            }
        }

        private void FormTablero_Load(object sender, EventArgs e)
        {
            this.Size = new Size(800, 800);
            juego.IniciarJuego();

            foreach(UCcasilla ucc in this.Controls)
            {

                ucc.ActualizarImagen();
            }
        }

    }
}
