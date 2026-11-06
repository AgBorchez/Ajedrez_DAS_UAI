namespace BE_BusinessEntities
{
    public class BE_MovementRecord
    {
        public BE_MovementRecord(int turn, string from, string to, BE_MovType MovType) 
        {
            this.Turn = turn;
            this.FromSquare = from;
            this.ToSquare = to;
            this.Special = MovType;
        }
        public int Turn { get; set; }
        public BE_MovType Special { get; set; }
        public string FromSquare { get; set; }
        public string ToSquare { get; set; }
    }
}