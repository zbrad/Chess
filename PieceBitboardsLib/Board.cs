using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PieceTypesLib;

namespace PieceBitboardsLib
{
    public struct Board : IComparable<Board>, IEquatable<Board>
    {
        const int MaxPieces = 8;
        public BitBoard[] Pieces;
        bool isWhiteMove;
        CastleState state;
        EnPassant enpassant;


        public Board(bool isWhite, Piece[] set, CastleState state, EnPassant ep)
        {
            this.isWhiteMove = isWhite;
            this.state = state;
            this.enpassant = ep;

            Pieces = new BitBoard[MaxPieces];
            for (var i = 0; i < set.Length; i++)
            {
                Pieces[(int)set[i].Color] |= set[i].Board;
                Pieces[(int)set[i].PieceSet] |= set[i].Board;
            }
        }

        public bool IsWhiteMove { get { return isWhiteMove; } }
        public CastleState State { get { return state; } }
        public EnPassant EnPassant { get { return enpassant; } }

        public int CompareTo(Board other)
        {
            int d = isWhiteMove ? (other.IsWhiteMove ? 0 : 1) : (other.IsWhiteMove ? -1 : 0);
            if (d != 0)
                return d;

            d = enpassant.CompareTo(other.EnPassant);
            if (d != 0)
                return d;

            d = state.CompareTo(other.State);
            if (d != 0)
                return d;

            return comparePieces(other);
        }

        int comparePieces(Board other)
        {
            var d = other.Pieces.Length - Pieces.Length;
            if (d != 0)
                return d;

            for (var i = 0; i < Pieces.Length; i++)
                if ((d = other.Pieces[i].CompareTo(Pieces[i])) != 0)
                    return d;

            return 0;
        }

        public bool Equals(Board other)
        {
            return isWhiteMove == other.IsWhiteMove
                && state.Equals(other.State)
                && enpassant.Equals(other.EnPassant)
                && equalPieces(other);
        }

        bool equalPieces(Board other)
        {
            if (Pieces.Length != other.Pieces.Length)
                return false;

            for (var i = 0; i < Pieces.Length; i++)
                if (!Pieces[i].Equals(other.Pieces[i]))
                    return false;

            return true;
        }

        BitBoard GetSet(PieceColor color, PieceType type)
        {
            return Pieces[(int)color] & Pieces[(int)type];
        }
    }

}
