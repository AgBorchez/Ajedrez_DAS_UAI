namespace Ajedrez_DAS.BE
{
    public class Movement
    {
        public Movement(Position p, MovType MovType) 
        {
            this.pos = p;
            this.MovType = MovType;
        }

        public readonly Position pos;

        public readonly MovType MovType;

    }
}
