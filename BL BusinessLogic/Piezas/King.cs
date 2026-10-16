using System.Collections.Generic;

namespace Ajedrez_DAS.BE
{
    public class King : Piece
    {
        public King(GameColor color): base(color) 
        {
        }

        private bool castling;

        public bool Castling
        {
            get { return castling; }
        }

        public override List<List<Movement>> GetPossibleMoves(Position p)
        {
            List<List<Movement>> Movs = new List<List<Movement>>(8);

            int colOrigin = p.x;
            int rowOrigin = p.y;

            //no es posible obtener el rango completo de movimiento espejando algunos, entonces decidi poner todos los movimientos posibles
            //en este caso [0] es "x" y [1] es "y"
            int[][] KingMovs =
            {
                new int[] {  0,  1 }, 
                new int[] {  0, -1 }, 
                new int[] { -1,  0 }, 
                new int[] {  1,  0 }, 
                new int[] { -1,  1 }, 
                new int[] {  1,  1 }, 
                new int[] { -1, -1 }, 
                new int[] {  1, -1 }  
            };

            foreach (int[] movs in KingMovs)
            {
                List<Movement> possibleMovs = new List<Movement>(1);
                int targerCol = colOrigin + movs[0];
                int targetRow = rowOrigin + movs[1];

                if(targetRow >= 0 && targetRow < 8 && targerCol >= 0 && targerCol < 8)
                {
                    Position targetPosition = new Position(targetRow, targerCol);
                    possibleMovs.Add(new Movement(targetPosition, MovType.Both));
                }
                Movs.Add(possibleMovs);
            }

            

            return Movs;
        }

        public override void Move()
        {
            castling = false;
        }

        public override bool VerifyCheck()
        {
            return true;
        }
    }
}