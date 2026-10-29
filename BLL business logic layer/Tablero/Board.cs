using System;
using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public delegate void DelStateChanged(BE_GameState State, BE_GameColor AttackedColor);
    public delegate void DelMovement(Square from, Square to);

    public class Board
    {
        private Square[,] squares = new Square[8, 8];
        private Dictionary<BE_GameColor, Piece> kings = new Dictionary<BE_GameColor, Piece>();
        private Dictionary<BE_GameColor, List<BE_Position>> squaresUnderAtack = new Dictionary<BE_GameColor, List<BE_Position>>();
        private int Turn;
        public event DelStateChanged OnStateChanged;
        public event DelMovement OnMovemnt;

        public Square[,] Squares
        {
            get { return squares; }
        }

        public void InitializeBoard()
        {
            Turn = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    squares[y, x] = new Square(new BE_Position(y, x));
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
                squares[0, col].SquarePiece = (Piece)Activator.CreateInstance(SpecialPieces[col], new object[] { BE_GameColor.Black });
                squares[1, col].SquarePiece = new Pawn(BE_GameColor.Black);
                squares[6, col].SquarePiece = new Pawn(BE_GameColor.White);
                squares[7, col].SquarePiece = (Piece)Activator.CreateInstance(SpecialPieces[col], new object[] { BE_GameColor.White });

                if (col == 4)
                {
                    kings.Add(BE_GameColor.Black, squares[0, col].SquarePiece);
                    kings.Add(BE_GameColor.White, squares[7, col].SquarePiece);
                }
            }
        }

        public List<BE_Position> GetLegalMoves(BE_Position pos, BE_GameColor gc)
        {
            Square s = SelectSquare(pos);
            if (s.SquarePiece == null) return null;
            if (s.SquarePiece.color != gc) return null;
            
            List<List<BE_Movement>> Movs = s.Movs();
            List<BE_Position> pseudoLegal = new List<BE_Position>();

            // 1. Obtener movimientos pseudo-legales geométricos
            foreach (List<BE_Movement> MovLine in Movs)
            {
                foreach (BE_Movement IndividualMov in MovLine)
                {
                    Piece TargetSquarePiece = SelectSquare(IndividualMov.pos).SquarePiece;

                    if (TargetSquarePiece == null && (IndividualMov.MovType == BE_MovType.Forward || IndividualMov.MovType == BE_MovType.Both))
                    {
                        pseudoLegal.Add(IndividualMov.pos);
                    }
                    else if (TargetSquarePiece != null)
                    {
                        if (TargetSquarePiece.VerifyCheck()) break;

                        if (TargetSquarePiece.color != s.SquarePiece.color &&
                           (IndividualMov.MovType == BE_MovType.Both || IndividualMov.MovType == BE_MovType.Attack))
                        {
                            pseudoLegal.Add(IndividualMov.pos);
                        }
                        break;
                    }
                }
            }

            List<BE_Position> LegalMovs = new List<BE_Position>();
            BE_GameColor Color = s.SquarePiece.color;

            foreach (BE_Position targetPos in pseudoLegal)
            {
                Square targetSquare = squares[targetPos.Y, targetPos.X];

                // Simular jugada
                Piece TargetPieceAux = targetSquare.SquarePiece;
                targetSquare.SquarePiece = s.SquarePiece;
                s.SquarePiece = null;

                SquaresUnderAttack();

                if (!KingUnderAttack(Color))
                {
                    LegalMovs.Add(targetPos);
                }

                s.SquarePiece = targetSquare.SquarePiece;
                targetSquare.SquarePiece = TargetPieceAux;
            }

            SquaresUnderAttack();

            return LegalMovs;
        }

        private bool KingUnderAttack(BE_GameColor colorRey)
        {
            Piece KingPiece = kings[colorRey];
            BE_Position KingPos = new BE_Position(-1, -1);

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (squares[y, x].SquarePiece == KingPiece)
                    {
                        KingPos = new BE_Position(y, x);
                        break;
                    }
                }
                if (KingPos.X != -1) break;
            }

            BE_GameColor RivColor = (colorRey == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White;
            List<BE_Position> EnemyAttacks = squaresUnderAtack[RivColor];

            return EnemyAttacks.Exists(a => a.X == KingPos.X && a.Y == KingPos.Y);
        }

        public BE_GameState MakeMove(BE_Position fromPos, BE_Position toPos)
        {
            Square from = SelectSquare(fromPos);
            Square to = SelectSquare(toPos);

            if (to.SquarePiece == null || (to.SquarePiece.color != from.SquarePiece.color))
            {
                BE_GameColor AttackerColor = from.SquarePiece.color;
                BE_GameColor AttackedColor = (AttackerColor == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White;

                from.MovePiece(to);
                
                SquaresUnderAttack();

                OnMovemnt?.Invoke(from, to);

                bool AtacckedCheck = KingUnderAttack(AttackedColor);

                BE_GameState estado = GetSideGameState(AttackedColor, AtacckedCheck);

                OnStateChanged?.Invoke(estado, AttackedColor);

                return estado;
            }

            return BE_GameState.InGame;
        }

        private Square SelectSquare(BE_Position pos)
        {
            if (pos.Y < 0 || pos.Y >= 8 || pos.X < 0 || pos.X >= 8)
                return null;

            return squares[pos.Y, pos.X];
        }

        private void SquaresUnderAttack()
        {
            List<BE_Position> WhiteThreat = new List<BE_Position>();
            List<BE_Position> BlackThreat = new List<BE_Position>();

            foreach (Square c in squares)
            {
                if (c.SquarePiece != null)
                {
                    List<BE_Position> Threated = GetAttackedSquares(c);

                    if (c.SquarePiece.color == BE_GameColor.White)
                        WhiteThreat.AddRange(Threated);
                    else
                        BlackThreat.AddRange(Threated);
                }
            }

            squaresUnderAtack[BE_GameColor.White] = WhiteThreat;
            squaresUnderAtack[BE_GameColor.Black] = BlackThreat;
        }

        private List<BE_Position> GetAttackedSquares(Square c)
        {
            List<BE_Position> Attacked = new List<BE_Position>();
            List<List<BE_Movement>> Lines = c.Movs();

            foreach (List<BE_Movement> line in Lines)
            {
                foreach (BE_Movement mov in line)
                {
                    if (mov.pos.Y < 0 || mov.pos.Y >= 8 || mov.pos.X < 0 || mov.pos.X >= 8) continue;
                    
                    if (mov.MovType == BE_MovType.Forward) continue;

                    Attacked.Add(mov.pos);

                    if (SelectSquare(mov.pos).SquarePiece != null)
                    {
                        break;
                    }
                }
            }

            return Attacked;
        }

        public bool HasLegalMovs(BE_GameColor colorBando)
        {
            foreach (Square c in squares)
            {
                List<BE_Position> movs = GetLegalMoves(c.Position, colorBando);
                if (movs != null && movs.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public BE_GameState GetSideGameState(BE_GameColor colorRival, bool hayJaqueDirecto)
        {
            bool tieneMovs = HasLegalMovs(colorRival);

            if (hayJaqueDirecto)
            {
                return tieneMovs ? BE_GameState.Check : BE_GameState.CheckMate;
            }
            else
            {
                return tieneMovs ? BE_GameState.InGame : BE_GameState.Tablas;
            }
        }
    }
}