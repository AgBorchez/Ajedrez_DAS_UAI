using System.Collections.Generic;

namespace Ajedrez_DAS.BE
{
    public class Rook : Piece
    {
        public Rook(GameColor color) : base(color) 
        {
        }

        private bool castling;

        public bool Castling
        {
            get { return castling; }
        }

        public override List<List<Movement>> GetPossibleMoves(Position p)
        {
            List<List<Movement>> PosibleMovs = new List<List<Movement>>();

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
                List<Movement> ray = new List<Movement>();

                int newRow = p.y + targetRow;
                int newCol = p.x + targetCol;

                while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                    Position targetPos = new Position(newRow, newCol);
                    ray.Add(new Movement(targetPos, MovType.Both));
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