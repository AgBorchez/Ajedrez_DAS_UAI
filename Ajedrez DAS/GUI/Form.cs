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
        private UCcasilla casillaSeleccionada;
        List<UCcasilla> PosiblesMovimientos = new List<UCcasilla>();
        public Form()
        {
            InitializeComponent();
            juego.asociarcasilla += Inicializar_Casillas;
            juego.ActualizarImagen += ActualizarImagenes;
        }

        private void Inicializar_Casillas(Casilla c, int y, int x)
        {
            UCcasilla ucc = new UCcasilla((((x + y) % 2 != 0)) ? Color.Beige : Color.BurlyWood, c);
            int sizeCasilla = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 8;

            ucc.Location = new Point(x * sizeCasilla, y * sizeCasilla);
            ucc.Size = new Size(sizeCasilla, sizeCasilla);

            Casillas[y, x] = ucc;
            ucc.SeleccionarCasilla += CasillaClickeada;

            this.Controls.Add( ucc );
        }

        private void CasillaClickeada(UCcasilla c)
        {
            if( c.PiezaCasilla == null && casillaSeleccionada == null) return;
            if (casillaSeleccionada != null)
            {
                if (PosiblesMovimientos.Contains(c)) 
                { 
                    SeleccionMovimiento(c.CasillaLogica);
                    casillaSeleccionada = null;
                }
                else
                {
                    ResetearColores();
                    casillaSeleccionada = null;
                }
            }
            else if (casillaSeleccionada == null && c.PiezaCasilla.color == juego.Color) {
                NuevaSeleccion(c);
            }
        }

        private void ResetearColores()
        {
            if(casillaSeleccionada!=null) casillaSeleccionada.ActualizarColorFondo();
            
            foreach (UCcasilla ucc in PosiblesMovimientos)
            {
                ucc.ActualizarColorFondo();
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

        private void NuevaSeleccion(UCcasilla c)
        {            
            List<Posicion> Movimientos = juego.SeleccionarCasilla(c.CasillaLogica);
            casillaSeleccionada = c;
            ResetearColores();

            if (Movimientos != null)
            {
                c.ActualizarColorFondo(Color.Yellow);
                PosiblesMovimientos = new List<UCcasilla>();
                foreach (Posicion mov in Movimientos)
                {
                    if (Casillas[mov.y, mov.x].CasillaOcupada())
                    {
                        Casillas[mov.y, mov.x].ActualizarColorFondo(Color.Red);
                    }
                    else
                    {
                        Casillas[mov.y, mov.x].ActualizarColorFondo(Color.Cyan);
                    }
                    
                    PosiblesMovimientos.Add(Casillas[mov.y, mov.x]);
                }
            }
        }

        // Ocurre cuando ya hay una casilla seleccionada previamente, ejecuta un movimiento valido de los pintados en pantalla
        private void SeleccionMovimiento(Casilla c) 
        {
            juego.EjecutarMovimiento(c);
            ResetearColores();
        }

        private void ActualizarImagenes()
        {
            foreach(UCcasilla ucc in this.Controls)
            {
                ucc.ActualizarImagen();
            }
        }

    }
}
