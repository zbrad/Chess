using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections;
using PieceTypesLib;

namespace PieceListsLib
{

    public struct Piece : IPiece
    {
        PieceType type;
        Square square;

        public PieceType Type { get { return type; } }
        public Square Square { get { return square; } }

        public Piece(PieceType type, Square square)
        {
            this.type = type;
            this.square = square;
        }

        public int CompareTo(IPiece other)
        {
            if (other == null)
                return -1;

            var d = type.CompareTo(other.Type);
            if (d != 0)
                return d;

            return square.CompareTo(other.Square);
        }

        public bool Equals(IPiece other)
        {
            if (other == null)
                return false;
            return type == other.Type && square.Equals(other.Square);
        }
    }

    public interface IPieces : IComparable<IPieces>, IEquatable<IPieces>
    {
        Square King { get; }
        Piece[] List { get; }
        Square[] Pawns { get; }
    }


    public struct Pieces : IPieces
    {
        Square king;
        Piece[] list;
        Square[] pawns;

        public Square King { get { return king; } }
        public Piece[] List { get { return list; } }
        public Square[] Pawns { get { return pawns; } }

        public Pieces(Square king, Piece[] list, Square[] pawns)
        {
            this.king = king;
            this.list = list == null ? null : (Piece[]) list.Clone();
            this.pawns = pawns == null ? null : (Square[]) pawns.Clone();
        }

        public int CompareTo(IPieces other)
        {
            if (other == null)
                return -1;

            var d = king.CompareTo(other.King);
            if (d != 0)
                return d;
            d = comparePieces(other);
            if (d != 0)
                return d;

            return comparePawns(other);
        }

        int comparePieces(IPieces other)
        {
            if (list == null)
            {
                if (other.List == null)
                    return 0;
                return -1;
            }

            if (other.List == null)
                return 1;

            var d = list.Length - other.List.Length;
            if (d != 0)
                return d;

            for (var i = 0; i < list.Length; i++)
                if ((d = list[i].CompareTo(other.List[i])) != 0)
                    return d;

            return 0;
        }

        int comparePawns(IPieces other)
        {
            if (pawns == null)
            {
                if (other.Pawns == null)
                    return 0;
                return -1;
            }

            if (other.Pawns == null)
                return 1;

            var d = pawns.Length - other.Pawns.Length;
            if (d != 0)
                return d;

            for (var i = 0; i < pawns.Length; i++)
                if ((d = pawns[i].CompareTo(other.Pawns[i])) != 0)
                    return d;

            return 0;
        }


        public bool Equals(IPieces other)
        {
            if (other == null)
                return false;
            return king.Equals(other.King) && comparePieces(other) == 0 && comparePawns(other) == 0;
        }
    }


    public struct Board : IBoard
    {
        bool isWhiteMove;
        Pieces white;
        Pieces black;
        CastleState castle;
        EnPassant enpassant;

        public Board(bool isWhite, Pieces white, Pieces black, CastleState castle, EnPassant ep)
        {
            this.isWhiteMove = isWhite;
            this.white = white;
            this.black = black;
            this.castle = castle;
            this.enpassant = ep;
        }

        public bool IsWhiteMove { get { return isWhiteMove; } }
        public Pieces White { get { return white; } }
        public Pieces Black { get { return black; } }
        public CastleState Castle { get { return castle; } }
        public EnPassant EnPassant { get { return enpassant; } }

        public bool Equals(IBoard other)
        {
            if (other == null)
                return false;
            return isWhiteMove == other.IsWhiteMove
                && white.Equals(other.White)
                && black.Equals(other.Black)
                && castle.Equals(other.Castle)
                && enpassant.Equals(other.EnPassant);             
        }

        public int CompareTo(IBoard other)
        {
            if (other == null)
                return -1;

            int d = isWhiteMove ? (other.IsWhiteMove ? 0 : 1) : (other.IsWhiteMove ? -1 : 0);
            if (d != 0)
                return d;
                            
            d = enpassant.CompareTo(other.EnPassant);
            if (d != 0)
                return d;

            d = castle.CompareTo(other.Castle);
            if (d != 0)
                return d;

            d = white.CompareTo(other.White);
            if (d != 0)
                return d;

            return black.CompareTo(other.Black);
        }
    }
}
