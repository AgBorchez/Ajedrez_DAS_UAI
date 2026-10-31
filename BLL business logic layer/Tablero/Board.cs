using System;
using System.Collections.Generic;
using BE_BusinessEntities;
using BL_BusinessLogic;

namespace BL_business_logic_layer
{
    public delegate void DelStateChanged(BE_GameState State, BE_GameColor AttackedColor);
    public delegate void DelMovement(Square from, Square to);

    public class Board
    {
        private Square[,] squares = new Square[8, 8];
        private Dictionary<BE_GameColor, Piece> kings = new Dictionary<BE_GameColor, Piece>();
        private Dictionary<BE_GameColor, List<BE_Position>> squaresUnderAtack = new Dictionary<BE_GameColor, List<BE_Position>>();

        // Estado transitorio para comer al paso (casilla vacía detrás del peón que avanzó doble)
        private BE_Position? enPassantVulnerableSquare = null;

        public Square[,] Squares => squares;

        public void InitializeBoard()
        {
            enPassantVulnerableSquare = null;

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
                    kings[BE_GameColor.Black] = squares[0, col].SquarePiece;
                    kings[BE_GameColor.White] = squares[7, col].SquarePiece;
                }
            }
        }

        public List<BE_MoveResult> GetLegalMoves(BE_Position pos, BE_GameColor gc)
        {
            Square s = SelectSquare(pos);
            if (s?.SquarePiece == null || s.SquarePiece.color != gc) return null;

            List<List<BE_Movement>> Movs = s.Movs();
            List<BE_MoveResult> pseudoLegal = new List<BE_MoveResult>();

            // 1. Movimientos estándar y ataques regulares
            foreach (List<BE_Movement> MovLine in Movs)
            {
                foreach (BE_Movement IndividualMov in MovLine)
                {
                    Square targetSquare = SelectSquare(IndividualMov.pos);
                    if (targetSquare == null) continue;

                    Piece TargetSquarePiece = targetSquare.SquarePiece;

                    if (TargetSquarePiece == null && (IndividualMov.MovType == BE_MovType.Forward || IndividualMov.MovType == BE_MovType.Both))
                    {
                        pseudoLegal.Add(new BE_MoveResult(IndividualMov.pos, IndividualMov.MovType));
                    }
                    else if (TargetSquarePiece != null)
                    {
                        if (TargetSquarePiece.VerifyCheck()) break;

                        if (TargetSquarePiece.color != s.SquarePiece.color &&
                            (IndividualMov.MovType == BE_MovType.Both || IndividualMov.MovType == BE_MovType.Attack))
                        {
                            pseudoLegal.Add(new BE_MoveResult(IndividualMov.pos, IndividualMov.MovType));
                        }
                        break;
                    }
                }
            }

            // 2. Agregar Comer al Paso
            if (s.SquarePiece.EnPassant)
            {
                AgregarMovimientosAlPaso(s, pseudoLegal);
            }

            // 3. Agregar Enroques 
            if (s.SquarePiece.CanCastle && !s.SquarePiece.HasMoved)
            {
                AddCaslingMoves(s, s.SquarePiece, pseudoLegal);
            }

            // 4. Filtrado de Jaque mediante simulación
            List<BE_MoveResult> LegalMovs = new List<BE_MoveResult>();
            BE_GameColor Color = s.SquarePiece.color;

            foreach (BE_MoveResult targetPos in pseudoLegal)
            {
                Square targetSquare = squares[targetPos.Position.Y, targetPos.Position.X];

                // Simular jugada (considerando captura lateral en caso de comer al paso)
                bool esAlPaso = (s.SquarePiece is Pawn) && enPassantVulnerableSquare.HasValue &&
                                targetPos.Position.X == enPassantVulnerableSquare.Value.X &&
                                targetPos.Position.Y == enPassantVulnerableSquare.Value.Y;

                Square peonAlPasoCasilla = null;
                Piece peonAlPasoCapturado = null;

                if (esAlPaso)
                {
                    int enemyY = (Color == BE_GameColor.White) ? targetPos.Position.Y + 1 : targetPos.Position.Y - 1;
                    peonAlPasoCasilla = squares[enemyY, targetPos.Position.X];
                    peonAlPasoCapturado = peonAlPasoCasilla.SquarePiece;
                    peonAlPasoCasilla.SquarePiece = null;
                }

                Piece TargetPieceAux = targetSquare.SquarePiece;
                targetSquare.SquarePiece = s.SquarePiece;
                s.SquarePiece = null;

                SquaresUnderAttack();

                if (!KingUnderAttack(Color))
                {
                    LegalMovs.Add(targetPos);
                }

                // Revertir simulación
                s.SquarePiece = targetSquare.SquarePiece;
                targetSquare.SquarePiece = TargetPieceAux;

                if (esAlPaso && peonAlPasoCasilla != null)
                {
                    peonAlPasoCasilla.SquarePiece = peonAlPasoCapturado;
                }
            }

            SquaresUnderAttack();

            return LegalMovs;
        }

        private void AgregarMovimientosAlPaso(Square s, List<BE_MoveResult> pseudoLegal)
        {
            if (!enPassantVulnerableSquare.HasValue) return;

            int dy = (s.SquarePiece.color == BE_GameColor.White) ? -1 : 1;

            // El peón debe estar en la fila adyacente a la casilla vulnerable
            if (s.Position.Y + dy == enPassantVulnerableSquare.Value.Y &&
                Math.Abs(s.Position.X - enPassantVulnerableSquare.Value.X) == 1)
            {
                pseudoLegal.Add(new BE_MoveResult(enPassantVulnerableSquare.Value, BE_MovType.Attack));
            }
        }

        private void AddCaslingMoves(Square kingSquare, Piece king, List<BE_MoveResult> pseudoLegal)
        {
            BE_GameColor color = king.color;
            if (KingUnderAttack(color)) return; // No se puede enrocar en jaque

            int row = (color == BE_GameColor.White) ? 7 : 0;
            if (kingSquare.Position.Y != row || kingSquare.Position.X != 4) return;

            BE_GameColor rivalColor = (color == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White;
            List<BE_Position> enemyAttacks = squaresUnderAtack[rivalColor];

            // Enroque Corto (Columna 7)
            Piece ShortCastlingPiece = squares[row, 7].SquarePiece;
            if (ShortCastlingPiece != null && ShortCastlingPiece.color == color && !ShortCastlingPiece.HasMoved)
            {
                if (squares[row, 5].SquarePiece == null && squares[row, 6].SquarePiece == null)
                {
                    if (!enemyAttacks.Exists(a => a.Y == row && (a.X == 5 || a.X == 6)))
                    {
                        pseudoLegal.Add(new BE_MoveResult(new BE_Position(row, 6), BE_MovType.CastleKingside));
                    }
                }
            }

            // Enroque Largo (Columna 0)
            Piece LongCastlingPiece = squares[row, 0].SquarePiece;
            if (LongCastlingPiece != null && LongCastlingPiece.color == color && !LongCastlingPiece.HasMoved)
            {
                if (squares[row, 1].SquarePiece == null && squares[row, 2].SquarePiece == null && squares[row, 3].SquarePiece == null)
                {
                    if (!enemyAttacks.Exists(a => a.Y == row && (a.X == 2 || a.X == 3)))
                    {
                        pseudoLegal.Add(new BE_MoveResult(new BE_Position(row, 2), BE_MovType.CastleQueenside));
                    }
                }
            }
        }

        public BE_GameState MakeMove(BE_Position fromPos, BE_Position toPos, BE_MovType? promotion = null)
        {
            Square from = SelectSquare(fromPos);
            Square to = SelectSquare(toPos);

            if (to.SquarePiece == null || (to.SquarePiece.color != from.SquarePiece.color))
            {
                Piece movingPiece = from.SquarePiece;
                BE_GameColor AttackerColor = movingPiece.color;
                BE_GameColor AttackedColor = (AttackerColor == BE_GameColor.White) ? BE_GameColor.Black : BE_GameColor.White;

                // Caso enroque
                if (movingPiece.CanCastle && Math.Abs(toPos.X - fromPos.X) == 2)
                {
                    // Enroque Corto 
                    if (toPos.X - fromPos.X == 2)
                    {
                        Square torreFrom = squares[fromPos.Y, 7];
                        Square torreTo = squares[fromPos.Y, 5];
                        torreFrom.SquarePiece?.Move();
                        torreFrom.MovePiece(torreTo);
                    }
                    // Enroque Largo 
                    else if (fromPos.X - toPos.X == 2)
                    {
                        Square torreFrom = squares[fromPos.Y, 0];
                        Square torreTo = squares[fromPos.Y, 3];
                        torreFrom.SquarePiece?.Move();
                        torreFrom.MovePiece(torreTo);
                    }
                }

                // Comer al paso
                // Se ejecuta si el destino coincide exactamente con la casilla vulnerable registrada
                bool IsEnPassant = enPassantVulnerableSquare.HasValue &&
                                       toPos.X == enPassantVulnerableSquare.Value.X &&
                                       toPos.Y == enPassantVulnerableSquare.Value.Y &&
                                       to.SquarePiece == null && // La casilla de destino tiene que estar vacía
                                       fromPos.X != toPos.X;    

                if (IsEnPassant)
                {
                    int enemyY = (AttackerColor == BE_GameColor.White) ? toPos.Y + 1 : toPos.Y - 1;
                    squares[enemyY, toPos.X].SquarePiece = null; 
                }

                // Registrar casilla vulnerable si la pieza lo habilita (peón avanzando 2 filas)
                if (movingPiece.LeavesEnPassantVulnerability(fromPos.Y, toPos.Y))
                {
                    int step = (toPos.Y - fromPos.Y) / 2;
                    enPassantVulnerableSquare = new BE_Position(fromPos.Y + step, fromPos.X); 
                }
                else
                {
                    enPassantVulnerableSquare = null;
                }

                movingPiece.Move();

                // Mover físicamente la pieza a la casilla destino
                from.MovePiece(to);

                // Coronacion
                if (to.SquarePiece.CanPromote(toPos.Y))
                {
                    if(promotion != null)
                    {
                        to.SquarePiece = to.SquarePiece.Promote(promotion.Value);
                    }
                    else
                    {
                        to.SquarePiece = to.SquarePiece.Promote(null);
                    }
                    
                }

                SquaresUnderAttack();

                bool AtacckedCheck = KingUnderAttack(AttackedColor);

                return GetSideGameState(AttackedColor, AtacckedCheck);
            }

            return BE_GameState.InGame;
        }

        public void ApplyMoveDirectly(BE_Position fromPos, BE_Position toPos)
        {
            Square from = SelectSquare(fromPos);
            Square to = SelectSquare(toPos);

            // Replicar enroque si corresponde durante reanudación de historial
            if (from.SquarePiece.CanCastle)
            {
                if (toPos.X - fromPos.X == 2) // Corto
                {
                    squares[fromPos.Y, 7].MovePiece(squares[fromPos.Y, 5]);
                }
                else if (fromPos.X - toPos.X == 2) // Largo
                {
                    squares[fromPos.Y, 0].MovePiece(squares[fromPos.Y, 3]);
                }
            }

            // Replicar comer al paso si corresponde
            if (from.SquarePiece.EnPassant && fromPos.X != toPos.X && to.SquarePiece == null)
            {
                int enemyY = (from.SquarePiece.color == BE_GameColor.White) ? toPos.Y + 1 : toPos.Y - 1;
                squares[enemyY, toPos.X].SquarePiece = null;
            }

            from.MovePiece(to);

            // Replicar coronación
            if (to.SquarePiece.CanPromote(toPos.Y))
            {
                to.SquarePiece = new Queen(to.SquarePiece.color);
            }
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
                List<BE_MoveResult> movs = GetLegalMoves(c.Position, colorBando);
                if (movs != null && movs.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public BE_GameState GetSideGameState(BE_GameColor RivalColor, bool isCheck)
        {
            bool hasMovs = HasLegalMovs(RivalColor);

            if (isCheck)
            {
                return hasMovs ? BE_GameState.Check : BE_GameState.CheckMate;
            }
            else
            {
                return hasMovs ? BE_GameState.InGame : BE_GameState.Draw;
            }
        }
    }
}