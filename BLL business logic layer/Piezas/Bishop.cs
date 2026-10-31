using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class Bishop : Piece
    {
        public Bishop(BE_GameColor color) : base(color) 
        {
        }

        public override List<List<BE_Movement>> GetPossibleMoves(BE_Position p)
        {
            {
                List<List<BE_Movement>> ValidMoves = new List<List<BE_Movement>>();

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
                    List<BE_Movement> MovesInDirection = new List<BE_Movement>();

                    int newRow = p.Y + destRow;
                    int newCol = p.X + destCol;

                    while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                    {
                        BE_Position targetPosition = new BE_Position(newRow, newCol);
                        MovesInDirection.Add(new BE_Movement(targetPosition, BE_MovType.Both));
                        
                        
                        newRow += destRow;
                        newCol += destCol;
                    }

                    ValidMoves.Add(MovesInDirection);
                }

                return ValidMoves;
            }

        }

        public override Piece Promote(BE_MovType? promotion)
        {
            return this;
        }
    }
}