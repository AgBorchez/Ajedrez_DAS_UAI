using System.Collections.Generic;

namespace Ajedrez_DAS.BE
{
    public abstract class Piece 
    {
		public Piece(GameColor color)  // 1 blanco, 0 negro
		{
			this.color = color;
		}
		public readonly GameColor color;

		public virtual void Move()
		{

		}


		//recordatorio: todas las piezas tienen un patron de movimiento, es posible validarlo matematicamente y es responsabilidad
		//de la pieza validar sus movimientos (su patron), la casilla se encargara de ejecutar el movimiento interactuando con otra casilla

		public abstract List<List<Movement>> GetPossibleMoves(Position p);
		//este metodo no va, esta mal planteada la resolucion del problema, ignoralo en las piezas hijas

		public virtual bool VerifyCheck()
		{
			return false;
		}
	}
}