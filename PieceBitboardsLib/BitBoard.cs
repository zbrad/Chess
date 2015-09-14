using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceBitboardsLib
{
    public struct BitBoard : IEquatable<BitBoard>, IComparable<BitBoard>
    {
        public ulong Board;

        public BitBoard(ulong u)
        {
            this.Board = u;
        }

        public static BitBoard operator |(BitBoard a, BitBoard b)
        {
            return new BitBoard(a.Board | b.Board);
        }

        public static BitBoard operator &(BitBoard a, BitBoard b)
        {
            return new BitBoard(a.Board & b.Board);
        }

        public static BitBoard operator ^(BitBoard a, BitBoard b)
        {
            return new BitBoard(a.Board ^ b.Board);
        }

        public static BitBoard operator ~(BitBoard a)
        {
            return new BitBoard(~a.Board);
        }

        public bool Equals(BitBoard other)
        {
            return Board.Equals(other.Board);
        }

        public int CompareTo(BitBoard other)
        {
            return Board.CompareTo(other.Board);
        }
    }
}
