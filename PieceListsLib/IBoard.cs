using System;
using PieceTypesLib;

namespace PieceListsLib
{
    public interface IBoard : IEquatable<IBoard>, IComparable<IBoard>
    {
        Pieces Black { get; }
        CastleState Castle { get; }
        EnPassant EnPassant { get; }
        bool IsWhiteMove { get; }
        Pieces White { get; }
    }
}