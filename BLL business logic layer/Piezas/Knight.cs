using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class Knight : Piece
    {
        public Knight(BE_GameColor color): base(color) 
        {
        }

        public override List<List<BE_Movement>> GetPossibleMoves(BE_Position p)
        {
            List<List<BE_Movement>> possibleMovs = new List<List<BE_Movement>>(8);

            int[][] knightJumps = new int[][]
            {
                new int[] { -2, -1 },
                new int[] { -2,  1 },
                new int[] { -1, -2 },
                new int[] { -1,  2 },
                new int[] {  1, -2 },
                new int[] {  1,  2 },
                new int[] {  2, -1 },
                new int[] {  2,  1 }
            };

            foreach (int[] jump in knightJumps)
            {
                int newRow = p.Y + jump[0];
                int newCol = p.X + jump[1];

                if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                    BE_Position targetPosition = new BE_Position(newRow, newCol);
                    possibleMovs.Add(new List<BE_Movement> { new BE_Movement(targetPosition, BE_MovType.Both) });
                }
            }

            return possibleMovs;
        }
    }
}