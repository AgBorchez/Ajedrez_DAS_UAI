using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class Pawn : Piece
    {
        public Pawn(BE_GameColor color) : base(color) 
        {
        }

        private bool firstMove = true;
        public bool FirstMove { get { return firstMove; } }

        public void consumeFirstMove() { firstMove  = false; }


        public override List<List<BE_Movement>> GetPossibleMoves(BE_Position p)
        {
            List<List<BE_Movement>> Movs = new List<List<BE_Movement>>(3);
            int direction = ((color == BE_GameColor.White) ? -1 : 1);
            int newRow = p.Y + direction;

            // Si el peón ya está en el borde extremo, no tiene movimientos hacia adelante ni en diagonal
            if (newRow < 0 || newRow >= 8)
            {
                return Movs;
            }

            // 1. Avance frontal
            List<BE_Movement> FowardMovs = new List<BE_Movement>(2);
            FowardMovs.Add(new BE_Movement(new BE_Position(newRow, p.X), BE_MovType.Forward));

            if (firstMove)
            {
                int doubleRow = p.Y + (direction * 2);
                if (doubleRow >= 0 && doubleRow < 8)
                {
                    FowardMovs.Add(new BE_Movement(new BE_Position(doubleRow, p.X), BE_MovType.Forward));
                }
            }
            Movs.Add(FowardMovs);

            // 2. Diagonales de ataque
            // Izquierda
            if (p.X - 1 >= 0)
            {
                Movs.Add(new List<BE_Movement> {
            new BE_Movement(new BE_Position(newRow, p.X - 1), BE_MovType.Attack)
        });
            }

            // Derecha
            if (p.X + 1 < 8)
            {
                Movs.Add(new List<BE_Movement> {
            new BE_Movement(new BE_Position(newRow, p.X + 1), BE_MovType.Attack)
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