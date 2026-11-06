using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class Queen : Piece
    {
        public Queen(BE_GameColor color) : base(color) 
        {
        }

        public override List<List<BE_Movement>> GetPossibleMoves(BE_Position p)
        {
            List<List<BE_Movement>> PosibleMovs = new List<List<BE_Movement>>();

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
                List<BE_Movement> ray = new List<BE_Movement>();

                int NewRow = p.Y + RowMov;
                int NewCol = p.X + ColMov;

                while (NewRow >= 0 && NewRow < 8 && NewCol >= 0 && NewCol < 8)
                {

                    BE_Position posicionDestino = new BE_Position(NewRow, NewCol);
                    ray.Add(new BE_Movement(posicionDestino, BE_MovType.Both));
                    NewRow += RowMov;
                    NewCol += ColMov;
                }

                PosibleMovs.Add(ray);
            }

            return PosibleMovs;
        }

        public override Piece Promote(BE_MovType? promotion)
        {
            return this;
        }
    }
}