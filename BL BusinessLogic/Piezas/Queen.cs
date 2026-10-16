using System.Collections.Generic;

namespace Ajedrez_DAS.BE
{
    public class Queen : Piece
    {
        public Queen(GameColor color) : base(color) 
        {
        }

        public override List<List<Movement>> GetPossibleMoves(Position p)
        {
            List<List<Movement>> PosibleMovs = new List<List<Movement>>();

            int[][] Directions = new int[][]
            {
                new int[] { 1, 0 },   
                new int[] { -1, 0 },  
                new int[] { 0, 1 },   
                new int[] { 0, -1 },  
                new int[] { 1, 1 },
                new int[] { 1, -1 },
                new int[] { -1, 1 },
                new int[] { -1, -1 }
            };

            foreach (int[] dir in Directions)
            {
                int RowMov = dir[0];
                int ColMov = dir[1];
                List<Movement> rayo = new List<Movement>();

                int NewRow = p.y + RowMov;
                int NewCol = p.x + ColMov;

                while (NewRow >= 0 && NewRow < 8 && NewCol >= 0 && NewCol < 8)
                {

                    Position posicionDestino = new Position(NewRow, NewCol);
                    rayo.Add(new Movement(posicionDestino, MovType.Both));
                    NewRow += RowMov;
                    NewCol += ColMov;
                }

                PosibleMovs.Add(rayo);
            }

            return PosibleMovs;
        }
    }
}