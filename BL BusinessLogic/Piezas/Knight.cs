using System.Collections.Generic;

namespace Ajedrez_DAS.BE
{
    public class Knight : Piece
    {
        public Knight(GameColor color): base(color) 
        {
        }

        public override List<List<Movement>> GetPossibleMoves(Position p)
        {
            List<List<Movement>> possibleMovs = new List<List<Movement>>(8);

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
                int newRow = p.y + jump[0];
                int newCol = p.x + jump[1];

                if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                    Position targetPosition = new Position(newRow, newCol);
                    possibleMovs.Add(new List<Movement> { new Movement(targetPosition, MovType.Both) });
                }
            }

            return possibleMovs;
        }
    }
}