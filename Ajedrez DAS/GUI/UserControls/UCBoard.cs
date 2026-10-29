using BL_business_logic_layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_BusinessEntities;

namespace Presentacion
{
    public delegate void ResizeForm(Size s);
    public partial class UCBoard : UserControl
    {
        private GameEngine Game = new GameEngine();
        private UCSquare[,] Squares = new UCSquare[8, 8];
        private UCSquare SelectedSquare;
        private List<UCSquare> PosiblesMovimientos = new List<UCSquare>();
        private UCSquare uccReyEnJaque = null;
        public Action<BE_GameColor> OnNextMove;

        public UCBoard(List<BE_User> Players)
        {
            InitializeComponent();

            Game.AssingPlayers(Players);
            Game.InitSquare += Inicializar_Casillas;
            Game.UpdateImage += ActualizarImagenes;
            Game.Board.OnStateChanged += ReaccionarEstadoPartida;
            Game.OnNextMove += OnNextMove;

            this.ClientSize = new Size(650, 650);
            Game.InitGame();

            foreach (UCSquare ucc in this.Controls)
            {
                ucc.ActualizarImagen();
            }
        }

        private void ReaccionarEstadoPartida(BE_GameState estado, BE_GameColor colorAfectado)
        {
            if (uccReyEnJaque != null)
            {
                uccReyEnJaque.ActualizarColorFondo();
                uccReyEnJaque = null;
            }

            if (estado == BE_GameState.Check || estado == BE_GameState.CheckMate)
            {
                foreach (UCSquare ucc in Squares)
                {
                    if (ucc.SquarePiece != null &&
                        ucc.SquarePiece.VerifyCheck() &&
                        ucc.SquarePiece.color == colorAfectado)
                    {
                        uccReyEnJaque = ucc;
                        break;
                    }
                }

                if (estado == BE_GameState.Check)
                {
                    uccReyEnJaque?.ActualizarColorFondo(Color.Orange);
                    MessageBox.Show($"¡Jaque al Rey {(colorAfectado == BE_GameColor.White ? "Blanco" : "Negro")}!",
                                    "Aviso de Jaque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (estado == BE_GameState.CheckMate)
                {
                    uccReyEnJaque?.ActualizarColorFondo(Color.Blue);
                    MessageBox.Show($"¡Jaque Mate! Ganan las {(colorAfectado == BE_GameColor.White ? "Negras" : "Blancas")}.",
                                    "Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Inicializar_Casillas(Square c, int y, int x)
        {
            UCSquare ucc = new UCSquare((((x + y) % 2 != 0)) ? Color.Beige : Color.BurlyWood, c);
            int sizeCasilla = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 8;

            ucc.Location = new Point(x * sizeCasilla, y * sizeCasilla);
            ucc.Size = new Size(sizeCasilla, sizeCasilla);

            Squares[y, x] = ucc;
            ucc.SeleccionarCasilla += CasillaClickeada;

            this.Controls.Add(ucc);
        }

        private void CasillaClickeada(UCSquare c)
        {
            if (c.SquarePiece == null && SelectedSquare == null) return;
            if (SelectedSquare != null)
            {
                if (PosiblesMovimientos.Contains(c))
                {
                    SeleccionMovimiento(c);
                    SelectedSquare = null;
                }
                else
                {
                    ResetearColores();
                    SelectedSquare = null;
                }
            }
            else if (SelectedSquare == null && c.SquarePiece.color == Game.actualPlayer)
            {
                NuevaSeleccion(c);
            }
        }

        private void ResetearColores()
        {
            if (SelectedSquare != null) SelectedSquare.ActualizarColorFondo();

            foreach (UCSquare ucc in PosiblesMovimientos)
            {
                ucc.ActualizarColorFondo();
            }

            if (uccReyEnJaque != null)
            {
                if (uccReyEnJaque.SquarePiece != null && uccReyEnJaque.SquarePiece.VerifyCheck())
                {
                    uccReyEnJaque.ActualizarColorFondo(Color.Orange);
                }
            }

            ActualizarImagenes();
        }

        private void NuevaSeleccion(UCSquare c)
        {
            List<BE_Position> Movimientos = Game.SelectSquare(c.pos);
            SelectedSquare = c;
            ResetearColores();

            if (Movimientos != null)
            {
                c.ActualizarColorFondo(Color.Yellow);
                PosiblesMovimientos = new List<UCSquare>();
                foreach (BE_Position mov in Movimientos)
                {
                    if (Squares[mov.Y, mov.X].CasillaOcupada())
                    {
                        Squares[mov.Y, mov.X].ActualizarColorFondo(Color.Red);
                    }
                    else
                    {
                        Squares[mov.Y, mov.X].ActualizarColorFondo(Color.Cyan);
                    }

                    PosiblesMovimientos.Add(Squares[mov.Y, mov.X]);
                }
            }
        }

        private void SeleccionMovimiento(UCSquare sq)
        {
            if (Game.MakeMov(sq.pos))
            {
                sq.SquarePiece = SelectedSquare.SquarePiece;
                SelectedSquare.SquarePiece = null;
            }
            ResetearColores();
        }

        private void ActualizarImagenes()
        {
            foreach (UCSquare ucc in this.Controls)
            {
                ucc.ActualizarImagen();
            }
        }

        public void RedimensionarCuadrado(int ladoTablero)
        {
            if (ladoTablero <= 0) return;

            // Cada casilla mide exactamente un octavo del lado total
            int ladoCasilla = ladoTablero / 8;
            if (ladoCasilla <= 0) return;

            this.SuspendLayout();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    UCSquare ucc = Squares[y, x]; 
            if (ucc != null) 
            {
                        // SetBounds asigna X, Y, Ancho y Alto de forma atómica y exacta
                        ucc.SetBounds(x * ladoCasilla, y * ladoCasilla, ladoCasilla, ladoCasilla);
                    }
                }
            }

            this.ResumeLayout(true);
            ActualizarImagenes(); 
        }
    }
}
