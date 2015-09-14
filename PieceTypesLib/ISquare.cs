using System;

namespace PieceTypesLib
{
    public interface ISquare : IComparable<ISquare>, IEquatable<ISquare>
    {
        int Position { get; }
        int Column { get; }
        int Row { get; }
    }
}