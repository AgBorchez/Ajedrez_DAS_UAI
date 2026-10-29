using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
    public class Square
    {
		public Square(BE_Position pos) 
		{
			this.position = pos; 
		}
		public Square(Piece p, BE_Position pos) : this (pos)
		{
			this.pieceInSquare = p;
		}
		private Piece pieceInSquare = null;

		public Piece SquarePiece
		{
			get { return pieceInSquare; }
			set { pieceInSquare = value; }
		}

		private BE_Position position;
		public BE_Position Position { get { return position; } }

		public int x
		{
			get
			{
				return position.X;
			}
		}

        public int y
        {
            get
            {
                return position.Y;
            }
        }

		public readonly string Notation;

        public List<List<BE_Movement>> Movs()
		{
            List<List<BE_Movement>> movs = this.pieceInSquare.GetPossibleMoves(position);

			return movs;
		}

		public void MovePiece(Square to)
		{
            SquarePiece.Move();
            to.pieceInSquare = this.pieceInSquare;
			this.pieceInSquare = null;
		}


    }
}