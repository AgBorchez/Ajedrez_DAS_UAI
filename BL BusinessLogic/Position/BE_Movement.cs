namespace BE_BusinessEntities
{
    public class BE_Movement
    {
        public BE_Movement(BE_Position p, BE_MovType MovType)
        {
            this.pos = p;
            this.MovType = MovType;
        }

        public BE_Position pos { get; }

        public BE_MovType MovType { get; }

    }
}
