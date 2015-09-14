using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTypesLib
{
    public struct CastleState : IComparable<CastleState>, IEquatable<CastleState>
    {
        [Flags]
        public enum Code : byte
        {
            None = 0,
            BQR = 1,
            BKR = 2,
            WQR = 4,
            WKR = 8,
            W = WKR | WQR,
            B = BKR | BQR,
            All = W | B
        }

        Code state;

        public CastleState(bool hasAll)
        {
            state = (hasAll) ? Code.All : Code.None;
        }

        public CastleState(params Code[] codes)
        {
            state = Code.None;
            foreach (var c in codes)
                state |= c;
        }

        public void Invalidate(params Code[] codes)
        {
            foreach (var c in codes)
                state &= ~c;
        }

        public int CompareTo(CastleState other)
        {
            return state - other.state;
        }

        public bool Equals(CastleState other)
        {
            return state == other.state;
        }

        public Code State { get { return state; } }
    }

}
