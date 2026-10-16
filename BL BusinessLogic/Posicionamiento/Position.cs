namespace Ajedrez_DAS.BE
{
    public struct Position
    {
        public Position(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        public readonly int x;
        public readonly int y;
    }
}