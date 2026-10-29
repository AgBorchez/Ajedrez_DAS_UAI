using System.Collections.Generic;
using BE_BusinessEntities;

namespace BL_business_logic_layer
{
	//Lo dejo aca porque no veo necesidad de que las otras capas necesiten saber sobre las piezas,
	//dado que las piezas lo unico que alteran es el patron de movimiento, con saber que hay una pieza en una casilla es mas que suficiente
    public abstract class Piece 
    {
		public Piece(BE_GameColor color)  // 1 blanco, 0 negro
		{
			this.color = color;
		}
		public readonly BE_GameColor color;

		public virtual void Move()
		{

		}


		//recordatorio: todas las piezas tienen un patron de movimiento, es posible validarlo matematicamente y es responsabilidad
		//de la pieza validar sus movimientos (su patron), la casilla se encargara de ejecutar el movimiento interactuando con otra casilla

		public abstract List<List<BE_Movement>> GetPossibleMoves(BE_Position p);
		//este metodo no va, esta mal planteada la resolucion del problema, ignoralo en las piezas hijas

		public virtual bool VerifyCheck()
		{
			return false;
		}
	}
}