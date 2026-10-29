using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class Rook : Piece
    {
        public Rook(BE_GameColor color) : base(color) 
        {
        }

        private bool castling;

        public bool Castling
        {
            get { return castling; }
        }

        public override List<List<BE_Movement>> GetPossibleMoves(BE_Position p)
        {
            List<List<BE_Movement>> PosibleMovs = new List<List<BE_Movement>>();

            int[][] directions = new int[][]
            {
                new int[] { 1, 0 },   
                new int[] { -1, 0 },  
                new int[] { 0, 1 },   
                new int[] { 0, -1 }   
            };

            foreach (int[] dir in directions)
            {
                int targetRow = dir[0];
                int targetCol = dir[1];
                List<BE_Movement> ray = new List<BE_Movement>();

                int newRow = p.Y + targetRow;
                int newCol = p.X + targetCol;

                while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                    BE_Position targetPos = new BE_Position(newRow, newCol);
                    ray.Add(new BE_Movement(targetPos, BE_MovType.Both));
                    newRow += targetRow;
                    newCol += targetCol;
                }

                PosibleMovs.Add(ray);
            }

            return PosibleMovs;
        }

        public override void Move()
        {
            castling = false;
        }
    }
}