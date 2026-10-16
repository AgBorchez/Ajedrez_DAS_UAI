using System.Collections.Generic;

namespace Ajedrez_DAS.BE
{
    public class Bishop : Piece
    {
        public Bishop(GameColor color) : base(color) 
        {
        }

        public override List<List<Movement>> GetPossibleMoves(Position p)
        {
            {
                List<List<Movement>> ValidMoves = new List<List<Movement>>();

                int[][] directions = new int[][]
                {
                    new int[] { 1, 1 },   
                    new int[] { 1, -1 },  
                    new int[] { -1, 1 },  
                    new int[] { -1, -1 }  
                };

                foreach (var dir in directions)
                {
                    int destRow = dir[0];
                    int destCol = dir[1];
                    List<Movement> MovesInDirection = new List<Movement>();

                    int newRow = p.y + destRow;
                    int newCol = p.x + destCol;

                    while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                    {
                        Position targetPosition = new Position(newRow, newCol);
                        MovesInDirection.Add(new Movement(targetPosition, MovType.Both));
                        
                        
                        newRow += destRow;
                        newCol += destCol;
                    }

                    ValidMoves.Add(MovesInDirection);
                }

                return ValidMoves;
            }
        }
    }
}