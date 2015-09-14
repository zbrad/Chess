using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTypesLib
{
    public struct Square : ISquare
    {
        byte position;
        public int Row { get { return position >> 3; } }
        public int Column { get { return position & 0x07; } }
        public int Position { get { return position; } }

        public Square(int position)
        {
            if (position < 0 || position >= 64)
                throw new ArgumentOutOfRangeException("only supports 64 squares");

            this.position = (byte)position;
        }

        public int CompareTo(ISquare other)
        {
            if (other == null)
                return -1;
            return position - other.Position;
        }

        public bool Equals(ISquare other)
        {
            if (other == null)
                return false;
            return position == other.Position;
        }

        public static readonly Square[] Squares;
        static Square()
        {
            Squares = new Square[64];
            for (var i = 0; i < 64; i++)
                Squares[i] = new Square(i);
        }
    }
}
