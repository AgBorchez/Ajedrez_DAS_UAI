using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class King : Piece
    {
        public King(BE_GameColor color): base(color) 
        {
        }

        public override bool CanCastle => true;

        public override List<List<BE_Movement>> GetPossibleMoves(BE_Position p)
        {
            List<List<BE_Movement>> Movs = new List<List<BE_Movement>>(8);

            int colOrigin = p.X;
            int rowOrigin = p.Y;

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
                List<BE_Movement> possibleMovs = new List<BE_Movement>(1);
                int targerCol = colOrigin + movs[0];
                int targetRow = rowOrigin + movs[1];

                if(targetRow >= 0 && targetRow < 8 && targerCol >= 0 && targerCol < 8)
                {
                    BE_Position targetPosition = new BE_Position(targetRow, targerCol);
                    possibleMovs.Add(new BE_Movement(targetPosition, BE_MovType.Both));
                }
                Movs.Add(possibleMovs);
            }

            

            return Movs;
        }

        public override void Move()
        {
            base.Move();
        }

        public override bool VerifyCheck()
        {
            return true;
        }

        public override Piece Promote(BE_MovType? promotion)
        {
            return this;
        }
    }
}