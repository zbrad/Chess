using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeBruijn
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                Console.WriteLine("usage: genscan 1..{0}", 1 << 26);
            else
            {
                var gen = new GenScan(int.Parse(args[0]));
                gen.Run();
            }
        }
    }

    class GenScan
    {
        int match;
        Stopwatch watch = new Stopwatch();
        int dbcount;
        ulong locked = pow2[32]; // optimization to exclude 32
        bool isFound;

        public GenScan(int match)
        {
            this.match = match;
        }

        public void Run()
        {
            watch.Start();
            try { find(0, 64 - 6, 0, 6); } catch { }
            watch.Stop();

            Console.WriteLine("// {0:F3} seconds for {1} De Bruijn sequences found", watch.Elapsed.TotalSeconds, dbcount);
        }

        //============================================
        // recursive search
        //============================================

        void find(ulong seq, int depth, int vtx, int nz)
        {
            if (isFound)
                return;

            if ((locked & pow2[vtx]) == 0 && nz <= 32)
            {
                // only if vertex is not locked
                if (depth == 0)
                {
                    // depth zero, De Bruijn sequence found, see remarks
                    if (++dbcount == match)
                        found(seq);
                }
                else
                {
                    locked ^= pow2[vtx]; // set bit, lock the vertex to don't appear multiple
                    if (vtx == 31 && depth > 2)
                    {
                        // optimization, see remarks
                        find(seq | pow2[depth - 1], depth - 2, 62, nz + 1);
                    }
                    else
                    {
                        find(seq, depth - 1, (2 * vtx) & 63, nz + 1); // even successor
                        find(seq | pow2[depth - 1], depth - 1, (2 * vtx + 1) & 63, nz); // odd successor
                    }

                    locked ^= pow2[vtx]; // reset bit, unlock
                }
            }
        }

        //==========================================
        // print the bitscan routine and throw
        //==========================================
        void found(ulong deBruijn)
        {
            int[] index = new int[64];
            for (var i = 0; i < index.Length; i++) // init magic array
                index[(int) ((deBruijn << i) >> (64 - 6))] = i;

            Console.WriteLine(@"
        const long magic = 0x{0:x08}{1:x08}; // the {2}",
                    (int)(deBruijn >> 32), (int)(deBruijn), dbcount);

            Console.Write(@"
        static readonly int[] magictable =
        {   ");

            for (var i = 0; i < index.Length; i++)
            {
                if ((i & 7) == 0)
                    Console.Write(@"
            ");
                Console.Write("{0},", index[i]);
            }

            Console.WriteLine(@"
        };

        int bitScan(ulong b)
        {
            var lb = (long)b;
            return magictable[(int) (((lb & -lb) * magic) >> 58)];
        }
");

            isFound = true;
            // throw 0; // unwind the stack until catched
        }

        static ulong[] pow2;
        static GenScan()
        {
            pow2 = new ulong[64];
            pow2[0] = 1;
            for (var i = 1; i < pow2.Length; i++)
                pow2[i] = 2 * pow2[i - 1];
        }
    }
}
