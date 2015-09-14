using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTypesLib
{
    public struct EnPassant : IComparable<EnPassant>, IEquatable<EnPassant>
    {
        byte column;

        public EnPassant(Square square)
        {
            this.column = (byte)square.Column;
        }

        public int Column { get { return this.column; } }

        public int CompareTo(EnPassant other)
        {
            return column - other.Column;
        }

        public bool Equals(EnPassant other)
        {
            return column == other.Column;
        }
    }

}
