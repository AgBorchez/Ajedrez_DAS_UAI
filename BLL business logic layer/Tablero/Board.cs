using System;
using System.Collections.Generic;
using Ajedrez_DAS.BE;

namespace Ajedrez_DAS.BLL
{
    public delegate void DelStateChanged(GameState State, GameColor Color);
    public delegate void DelMovement(Square from, Square to);

    public class Board
    {
        private Square[,] squares = new Square[8, 8];
        private Dictionary<GameColor, Piece> kings = new Dictionary<GameColor, Piece>();
        private Dictionary<GameColor, List<Position>> squaresUnderAtack = new Dictionary<GameColor, List<Position>>();
        public event DelStateChanged OnStateChanged;
        public event DelMovement OnMovemnt;

        public Square[,] Squares
        {
            get { return squares; }
        }

        public void InitializeBoard()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    squares[y, x] = new Square(new Position(y, x));
                }
            }

            SetupStartingPieces();
            SquaresUnderAttack();
        }

        private void SetupStartingPieces()
        {
            Type[] SpecialPieces = {
                typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen),
                typeof(King), typeof(Bishop), typeof(Knight), typeof(Rook)
            };

            for (int col = 0; col < 8; col++)
            {
                squares[0, col].SquarePiece = (Piece)Activator.CreateInstance(SpecialPieces[col], new object[] { GameColor.Black });
                squares[1, col].SquarePiece = new Pawn(GameColor.Black);
                squares[6, col].SquarePiece = new Pawn(GameColor.White);
                squares[7, col].SquarePiece = (Piece)Activator.CreateInstance(SpecialPieces[col], new object[] { GameColor.White });

                if (col == 4)
                {
                    kings.Add(GameColor.Black, squares[0, col].SquarePiece);
                    kings.Add(GameColor.White, squares[7, col].SquarePiece);
                }
            }
        }

        public List<Position> GetLegalMoves(Square c)
        {
            List<List<Movement>> Movs = c.Movs();
            List<Position> pseudoLegal = new List<Position>();

            // 1. Obtener movimientos pseudo-legales geométricos
            foreach (List<Movement> MovLine in Movs)
            {
                foreach (Movement IndividualMov in MovLine)
                {
                    Piece TargetSquarePiece = SelectSquare(IndividualMov).SquarePiece;

                    if (TargetSquarePiece == null && (IndividualMov.MovType == MovType.Foward || IndividualMov.MovType == MovType.Both))
                    {
                        pseudoLegal.Add(IndividualMov.pos);
                    }
                    else if (TargetSquarePiece != null)
                    {
                        if (TargetSquarePiece.VerifyCheck()) break;

                        if (TargetSquarePiece.color != c.SquarePiece.color &&
                           (IndividualMov.MovType == MovType.Both || IndividualMov.MovType == MovType.Attack))
                        {
                            pseudoLegal.Add(IndividualMov.pos);
                        }
                        break;
                    }
                }
            }

            List<Position> LegalMovs = new List<Position>();
            GameColor Color = c.SquarePiece.color;

            foreach (Position targetPos in pseudoLegal)
            {
                Square targetSquare = squares[targetPos.y, targetPos.x];

                // Simular jugada
                Piece TargetPieceAux = targetSquare.SquarePiece;
                targetSquare.SquarePiece = c.SquarePiece;
                c.SquarePiece = null;

                SquaresUnderAttack();

                if (!KingUnderAttack(Color))
                {
                    LegalMovs.Add(targetPos);
                }

                c.SquarePiece = targetSquare.SquarePiece;
                targetSquare.SquarePiece = TargetPieceAux;
            }

            SquaresUnderAttack();

            return LegalMovs;
        }

        private bool KingUnderAttack(GameColor colorRey)
        {
            Piece KingPiece = kings[colorRey];
            Position KingPos = new Position(-1, -1);

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (squares[y, x].SquarePiece == KingPiece)
                    {
                        KingPos = new Position(y, x);
                        break;
                    }
                }
                if (KingPos.x != -1) break;
            }

            GameColor RivColor = (colorRey == GameColor.White) ? GameColor.Black : GameColor.White;
            List<Position> EnemyAttacks = squaresUnderAtack[RivColor];

            return EnemyAttacks.Exists(a => a.x == KingPos.x && a.y == KingPos.y);
        }

        public GameState MakeMove(Square from, Square to)
        {
            if (to.SquarePiece == null || (to.SquarePiece.color != from.SquarePiece.color))
            {
                GameColor AttackerColor = from.SquarePiece.color;
                GameColor AttackedColor = (AttackerColor == GameColor.White) ? GameColor.Black : GameColor.White;

                from.MovePiece(to);
                SquaresUnderAttack();

                OnMovemnt?.Invoke(from, to);

                bool AtacckedCheck = KingUnderAttack(AttackedColor);

                GameState estado = GetSideGameState(AttackedColor, AtacckedCheck);

                OnStateChanged?.Invoke(estado, AttackedColor);

                return estado;
            }

            return GameState.InGame;
        }

        private Square SelectSquare(Movement m)
        {
            if (m.pos.y < 0 || m.pos.y >= 8 || m.pos.x < 0 || m.pos.x >= 8)
                return null;

            return squares[m.pos.y, m.pos.x];
        }

        private void SquaresUnderAttack()
        {
            List<Position> WhiteThreat = new List<Position>();
            List<Position> BlackThreat = new List<Position>();

            foreach (Square c in squares)
            {
                if (c.SquarePiece != null)
                {
                    List<Position> Threated = GetAttackedSquares(c);

                    if (c.SquarePiece.color == GameColor.White)
                        WhiteThreat.AddRange(Threated);
                    else
                        BlackThreat.AddRange(Threated);
                }
            }

            squaresUnderAtack[GameColor.White] = WhiteThreat;
            squaresUnderAtack[GameColor.Black] = BlackThreat;
        }

        private List<Position> GetAttackedSquares(Square c)
        {
            List<Position> Attacked = new List<Position>();
            List<List<Movement>> Lines = c.Movs();

            foreach (List<Movement> line in Lines)
            {
                foreach (Movement mov in line)
                {
                    if (mov.pos.y < 0 || mov.pos.y >= 8 || mov.pos.x < 0 || mov.pos.x >= 8) continue;
                    
                    if (mov.MovType == MovType.Foward) continue;

                    Attacked.Add(mov.pos);

                    if (SelectSquare(mov).SquarePiece != null)
                    {
                        break;
                    }
                }
            }

            return Attacked;
        }

        public bool HasLegalMovs(GameColor colorBando)
        {
            foreach (Square c in squares)
            {
                if (c.SquarePiece != null && c.SquarePiece.color == colorBando)
                {
                    List<Position> movs = GetLegalMoves(c);
                    if (movs != null && movs.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public GameState GetSideGameState(GameColor colorRival, bool hayJaqueDirecto)
        {
            bool tieneMovs = HasLegalMovs(colorRival);

            if (hayJaqueDirecto)
            {
                return tieneMovs ? GameState.Check : GameState.CheckMate;
            }
            else
            {
                return tieneMovs ? GameState.InGame : GameState.Tablas;
            }
        }
    }
}