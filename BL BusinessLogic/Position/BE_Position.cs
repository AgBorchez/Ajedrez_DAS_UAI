using System;

namespace BE_BusinessEntities
{
    public struct BE_Position
    {
        public BE_Position(int y, int x)
        {
            this.Y = y;
            this.X = x;
        }
        public int X { get; }
        public int Y { get; }

        public string ToNotation()
        {
            if (X < 0 || X > 7 || Y < 0 || Y > 7)
                throw new ArgumentOutOfRangeException($"Coordenadas fuera de rango: X={X}, Y={Y}");

            char col = (char)('a' + X);
            int row = Y + 1;

            return $"{col}{row}";
        }

        public static BE_Position FromNotation(string notation)
        {
            if (string.IsNullOrWhiteSpace(notation) || notation.Length != 2)
                throw new ArgumentException("La notación debe contener exactamente 2 caracteres (ej: 'e4').", nameof(notation));

            char colChar = char.ToLower(notation[0]); 
            char rowChar = notation[1];              

            if (colChar < 'a' || colChar > 'h')
                throw new ArgumentOutOfRangeException(nameof(notation), $"Columna inválida: '{colChar}'. Debe estar entre 'a' y 'h'.");

            if (rowChar < '1' || rowChar > '8')
                throw new ArgumentOutOfRangeException(nameof(notation), $"Fila inválida: '{rowChar}'. Debe estar entre '1' y '8'.");

            int x = colChar - 'a';       
            int y = (rowChar - '0') - 1; 

            return new BE_Position(y, x);
        }

    }
    
}