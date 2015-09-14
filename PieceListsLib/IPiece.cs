using System;
using PieceTypesLib;

namespace PieceListsLib
{
    public interface IPiece : IComparable<IPiece>, IEquatable<IPiece>
    {
        Square Square { get; }
        PieceType Type { get; }
    }
}