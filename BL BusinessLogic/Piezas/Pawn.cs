using System.Collections.Generic;

namespace Ajedrez_DAS.BE
{
    public class Pawn : Piece
    {
        public Pawn(GameColor color) : base(color) 
        {
        }

        private bool firstMove = true;
        public bool FirstMove { get { return firstMove; } }

        public void consumeFirstMove() { firstMove  = false; }


        public override List<List<Movement>> GetPossibleMoves(Position p)
        {
            List<List<Movement>> Movs = new List<List<Movement>>(3);
            int direction = ((color == GameColor.White) ? -1 : 1);
            int newRow = p.y + direction;

            // Si el peón ya está en el borde extremo, no tiene movimientos hacia adelante ni en diagonal
            if (newRow < 0 || newRow >= 8)
            {
                return Movs;
            }

            // 1. Avance frontal
            List<Movement> FowardMovs = new List<Movement>(2);
            FowardMovs.Add(new Movement(new Position(newRow, p.x), MovType.Foward));

            if (firstMove)
            {
                int doubleRow = p.y + (direction * 2);
                if (doubleRow >= 0 && doubleRow < 8)
                {
                    FowardMovs.Add(new Movement(new Position(doubleRow, p.x), MovType.Foward));
                }
            }
            Movs.Add(FowardMovs);

            // 2. Diagonales de ataque
            // Izquierda
            if (p.x - 1 >= 0)
            {
                Movs.Add(new List<Movement> {
            new Movement(new Position(newRow, p.x - 1), MovType.Attack)
        });
            }

            // Derecha
            if (p.x + 1 < 8)
            {
                Movs.Add(new List<Movement> {
            new Movement(new Position(newRow, p.x + 1), MovType.Attack)
        });
            }

            return Movs;
        }

        public override void Move()
        {
            firstMove = false;
        }

    
    }
}