using System.Collections.Generic;
using Ajedrez_DAS.BE;

namespace Ajedrez_DAS.BLL
{
    public class Square
    {
		public Square(Position pos) 
		{
			this.position = pos; 
		}
		public Square(Piece p, Position pos) : this (pos)
		{
			this.pieceInSquare = p;
		}
		private Piece pieceInSquare = null;

		public Piece SquarePiece
		{
			get { return pieceInSquare; }
			set { pieceInSquare = value; }
		}

		private Position position;

		public int x
		{
			get
			{
				return position.x;
			}
		}

        public int y
        {
            get
            {
                return position.y;
            }
        }

        public List<List<Movement>> Movs()
		{
            List<List<Movement>> movs = this.pieceInSquare.GetPossibleMoves(position);

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