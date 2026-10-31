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
        private List<UCSquare> PosMovs = new List<UCSquare>();
        private UCSquare uccCheckKing = null;
        public Action<BE_GameColor> OnNextMove;

        public UCBoard(List<BE_User> Players)
        {
            InitializeComponent();

            Game.AssingPlayers(Players);
            SuscribeMetods();

            this.ClientSize = new Size(650, 650);
            Game.InitGame();

            foreach (UCSquare ucc in this.Controls)
            {
                ucc.UpdateImage();
            }
        }

        public UCBoard(List<BE_User> Players, BE_GameRecord record)
        {
            InitializeComponent();
            SuscribeMetods();

            Game.ResumeGame(record, Players);

            this.ClientSize = new Size(650, 650);

            foreach (UCSquare ucc in this.Controls)
            {
                ucc.UpdateImage();
            }
        }

        private void ReactGameState(BE_GameState state, BE_GameColor affectedColor)
        {
            if (uccCheckKing != null)
            {
                uccCheckKing.UpdateBackgroundColor();
                uccCheckKing = null;
            }

            if (state == BE_GameState.Check || state == BE_GameState.CheckMate)
            {
                foreach (UCSquare ucc in Squares)
                {
                    if (ucc.SquarePiece != null &&
                        ucc.SquarePiece.VerifyCheck() &&
                        ucc.SquarePiece.color == affectedColor)
                    {
                        uccCheckKing = ucc;
                        break;
                    }
                }

                if (state == BE_GameState.Check)
                {
                    uccCheckKing?.UpdateBackgroundColor(Color.Orange);
                    MessageBox.Show($"¡Jaque al Rey {(affectedColor == BE_GameColor.White ? "Blanco" : "Negro")}!",
                                    "Aviso de Jaque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (state == BE_GameState.CheckMate)
                {
                    uccCheckKing?.UpdateBackgroundColor(Color.Blue);
                    MessageBox.Show($"¡Jaque Mate! Ganan las {(affectedColor == BE_GameColor.White ? "Negras" : "Blancas")}.",
                                    "Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (state == BE_GameState.Draw)
                {
                    uccCheckKing?.UpdateBackgroundColor(Color.Blue);
                    MessageBox.Show($"¡Rey ahogado! Empate.",
                                    "Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void InitSquares(Square c, int y, int x)
        {
            UCSquare ucc = new UCSquare((((x + y) % 2 != 0)) ? Color.Beige : Color.BurlyWood, c);
            int sizeSquare = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 8;

            ucc.Location = new Point(x * sizeSquare, y * sizeSquare);
            ucc.Size = new Size(sizeSquare, sizeSquare);

            Squares[y, x] = ucc;
            ucc.SelectSquare += ClickedSquare;

            this.Controls.Add(ucc);
        }

        private void ClickedSquare(UCSquare c)
        {
            if (c.SquarePiece == null && SelectedSquare == null) return;
            if (SelectedSquare != null)
            {
                if (PosMovs.Contains(c))
                {
                    SelectMove(c);
                    SelectedSquare = null;
                }
                else
                {
                    ResetColors();
                    SelectedSquare = null;
                }
            }
            else if (SelectedSquare == null && c.SquarePiece.color == Game.actualPlayer)
            {
                NewSelection(c);
            }
        }

        private void ResetColors()
        {
            if (SelectedSquare != null) SelectedSquare.UpdateBackgroundColor();

            foreach (UCSquare ucc in PosMovs)
            {
                ucc.UpdateBackgroundColor();
            }

            if (uccCheckKing != null)
            {
                if (uccCheckKing.SquarePiece != null && uccCheckKing.SquarePiece.VerifyCheck())
                {
                    uccCheckKing.UpdateBackgroundColor(Color.Orange);
                }
            }

            UpdateImage();
        }

        private void NewSelection(UCSquare c)
        {
            List<BE_Position> Movs = Game.SelectSquare(c.pos);
            SelectedSquare = c;
            ResetColors();

            if (Movs != null)
            {
                c.UpdateBackgroundColor(Color.Yellow);
                PosMovs = new List<UCSquare>();
                foreach (BE_Position mov in Movs)
                {
                    if (Squares[mov.Y, mov.X].CasillaOcupada())
                    {
                        Squares[mov.Y, mov.X].UpdateBackgroundColor(Color.Red);
                    }
                    else
                    {
                        Squares[mov.Y, mov.X].UpdateBackgroundColor(Color.Cyan);
                    }

                    PosMovs.Add(Squares[mov.Y, mov.X]);
                }
            }
        }

        private void SelectMove(UCSquare sq)
        {
            if (Game.MakeMov(sq.pos))
            {
                sq.SquarePiece = SelectedSquare.SquarePiece;
                SelectedSquare.SquarePiece = null;
            }
            ResetColors();
        }

        private void UpdateImage()
        {
            foreach (UCSquare ucc in this.Controls)
            {
                ucc.UpdateImage();
            }
        }

        public void ResizeSquares(int boardSize)
        {
            if (boardSize <= 0) return;

            int squareSize = boardSize / 8;
            if (squareSize <= 0) return;

            this.SuspendLayout();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    UCSquare square = Squares[y, x];
                    if (square != null)
                    {
                        square.SetBounds(x * squareSize, y * squareSize, squareSize, squareSize);
                    }
                }
            }

            this.ResumeLayout(true);
            UpdateImage();
        }

        private void SuscribeMetods()
        {
            Game.InitSquare += InitSquares;
            Game.OnNextMove += color => OnNextMove?.Invoke(color); ;
            Game.OnGameStateChanged += ReactGameState;
        }
    }
}
