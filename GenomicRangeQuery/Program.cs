using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenomicRangeQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            string S = "CAGCCTA";
            int[] P = { 2, 5, 0 };
            int[] Q = { 4, 5, 6 };
            Console.WriteLine(string.Join(", ", Solution(S, P, Q)));
        }

        public static int[] Solution(string S, int[] P, int[] Q)
        {
            Dictionary<char, byte> nucleotides = new Dictionary<char, byte>()
            {
                {'A', 1 },
                {'C', 2 },
                {'G', 3 },
                {'T', 4 }
            };
            int[] results = new int[P.Length];
            byte MIN_FACTOR_VALUE = nucleotides.Select(x => x.Value).Min();
            byte rangeMinimum = 0;

            byte[][] minimums = new byte[S.Length][];

            for (int i = 0; i < S.Length; i++)
            {
                minimums[i] = new byte[S.Length - i];
                for (int j = i; j < minimums[i].Length + i; j++)
                {
                    rangeMinimum = nucleotides[S[j]];
                    if (rangeMinimum == MIN_FACTOR_VALUE)
                    {
                        FillWithMin(ref minimums, i, j - i, minimums[i].Length, MIN_FACTOR_VALUE);
                        break;
                    }
                    for (int k = i; k < j + 1; k++)
                    {
                        if (rangeMinimum > nucleotides[S[k]]) rangeMinimum = nucleotides[S[k]];
                        if (rangeMinimum == MIN_FACTOR_VALUE) break;
                    }
                    minimums[i][j - i] = rangeMinimum;
                }
            }

            for (int i = 0; i < P.Length; i++)
            {
                if (P[i] == Q[i])
                {
                    results[i] = nucleotides[S[P[i]]];
                    continue;
                }

                results[i] = minimums[P[i]][Q[i] - P[i]];
            }

            return results;
        }

        public static void FillWithMin(ref byte[][] mins, int row, int begin, int end, byte minValue)
        {
            for (int i = begin; i < end; i++)
            {
                mins[row][i] = minValue;
            }
        }
    }
}
