using Ajedrez_DAS.BLL;
using Ajedrez_DAS.Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ajedrez_DAS
{
    public delegate void ResizeForm(Size s);
    public partial class Tablero : UserControl
    {
        private Game juego = new Game();
        private UCcasilla[,] Casillas = new UCcasilla[8, 8];
        private UCcasilla casillaSeleccionada;
        private List<UCcasilla> PosiblesMovimientos = new List<UCcasilla>();
        private UCcasilla uccReyEnJaque = null;

        public Tablero()
        {
            InitializeComponent();
            juego.InitSquare += Inicializar_Casillas;
            juego.UpdateImage += ActualizarImagenes;
            juego.Board.OnEstadoCambiado += ReaccionarEstadoPartida;

            this.ClientSize = new Size(650, 650);
            juego.InitGame();

            foreach (UCcasilla ucc in this.Controls)
            {
                ucc.ActualizarImagen();
            }
        }

        private void ReaccionarEstadoPartida(GameState estado, GameColor colorAfectado)
        {
            if (uccReyEnJaque != null)
            {
                uccReyEnJaque.ActualizarColorFondo();
                uccReyEnJaque = null;
            }

            if (estado == GameState.Check || estado == GameState.CheckMate)
            {
                foreach (UCcasilla ucc in Casillas)
                {
                    if (ucc.PiezaCasilla != null &&
                        ucc.PiezaCasilla.VerifyCheck() &&
                        ucc.PiezaCasilla.color == colorAfectado)
                    {
                        uccReyEnJaque = ucc;
                        break;
                    }
                }

                if (estado == GameState.Check)
                {
                    uccReyEnJaque?.ActualizarColorFondo(Color.Orange);
                    MessageBox.Show($"¡Jaque al Rey {(colorAfectado == GameColor.White ? "Blanco" : "Negro")}!",
                                    "Aviso de Jaque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (estado == GameState.CheckMate)
                {
                    uccReyEnJaque?.ActualizarColorFondo(Color.Blue);
                    MessageBox.Show($"¡Jaque Mate! Ganan las {(colorAfectado == GameColor.White ? "Negras" : "Blancas")}.",
                                    "Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Inicializar_Casillas(Square c, int y, int x)
        {
            UCcasilla ucc = new UCcasilla((((x + y) % 2 != 0)) ? Color.Beige : Color.BurlyWood, c);
            int sizeCasilla = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 8;

            ucc.Location = new Point(x * sizeCasilla, y * sizeCasilla);
            ucc.Size = new Size(sizeCasilla, sizeCasilla);

            Casillas[y, x] = ucc;
            ucc.SeleccionarCasilla += CasillaClickeada;

            this.Controls.Add(ucc);
        }

        private void CasillaClickeada(UCcasilla c)
        {
            if (c.PiezaCasilla == null && casillaSeleccionada == null) return;
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
            else if (casillaSeleccionada == null && c.PiezaCasilla.color == juego.Color)
            {
                NuevaSeleccion(c);
            }
        }

        private void ResetearColores()
        {
            if (casillaSeleccionada != null) casillaSeleccionada.ActualizarColorFondo();

            foreach (UCcasilla ucc in PosiblesMovimientos)
            {
                ucc.ActualizarColorFondo();
            }

            if (uccReyEnJaque != null)
            {
                if (uccReyEnJaque.CasillaLogica.SquarePiece != null && uccReyEnJaque.CasillaLogica.SquarePiece.VerifyCheck())
                {
                    uccReyEnJaque.ActualizarColorFondo(Color.Orange);
                }
            }
        }

        private void NuevaSeleccion(UCcasilla c)
        {
            List<Position> Movimientos = juego.SelectSquare(c.CasillaLogica);
            casillaSeleccionada = c;
            ResetearColores();

            if (Movimientos != null)
            {
                c.ActualizarColorFondo(Color.Yellow);
                PosiblesMovimientos = new List<UCcasilla>();
                foreach (Position mov in Movimientos)
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

        private void SeleccionMovimiento(Square c)
        {
            juego.MakeMov(c);
            ResetearColores();
        }

        private void ActualizarImagenes()
        {
            foreach (UCcasilla ucc in this.Controls)
            {
                ucc.ActualizarImagen();
            }
        }

        public void Tablero_Resize(Size Size)
        {
            if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0) return;

            // Se calculan las dimensiones de forma independiente para estirar a pantalla completa
            int anchoCasilla = Size.Width / 8;
            int altoCasilla = Size.Height / 8;

            if (anchoCasilla <= 0 || altoCasilla <= 0) return;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    UCcasilla ucc = Casillas[y, x];
                    if (ucc != null)
                    {
                        ucc.Location = new Point(x * anchoCasilla, y * altoCasilla);
                        ucc.Size = new Size(anchoCasilla, altoCasilla);
                    }
                }
            }

            ActualizarImagenes();
        }
    }
}
