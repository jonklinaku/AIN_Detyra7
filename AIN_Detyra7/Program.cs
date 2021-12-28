using System;
using System.Collections.Generic;

namespace AIN_Detyra7
{
    class Program
    {
        static readonly int days = 4;
        static readonly int periods = 4;
        static readonly int ExamNo = 5;
        static List<List<int>> SameStudentExams = new List<List<int>>();
        //static List<List<int>> solution = new List<List<int>>(days*periods);
        static void Main(string[] args)
        {
            init();
            int[] candSol = { 3, 4, 12, 0, 0, 3, 12, 1 };
            List<List<int>> ans = ToSolution(candSol);
            
            printSol(ans);
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.Write(Environment.NewLine + Environment.NewLine);
            int[] mutatedCandSol = mutateSolution(candSol,true);
            List<List<int>> MutatedAns = ToSolution(mutatedCandSol);
            printSol(MutatedAns);
            Console.ReadKey();
        }
        static void printSol(List<List<int>> arr)
        {
            int rowLength = periods;
            int colLength = days;
            string[,] SolText = new string[periods, days];
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    //SolText[i,j] = string.Join(", ", arr[i+j]);
                    foreach (var item in arr[i+ colLength*j])
                    {
                        SolText[i, j] += $"E{item + 1}";
                    }
                    if (!String.IsNullOrEmpty( SolText[i, j]))
                    Console.Write(SolText[i, j]+"\t");
                    else
                        Console.Write( "_\t");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
        static void init()
        {
            SameStudentExams.Add(new List<int>() { 0, 1 });
            SameStudentExams.Add(new List<int>() { 3, 4, 7 });
            SameStudentExams.Add(new List<int>() { 5, 3, 0 });
        }
        static int[] mutateSolution(int[] cSol,bool hardcodedCase)
        {
            if (hardcodedCase)
            {
                cSol[3] = 9;
            } else
            {
                var rand = new Random();
                var index = rand.Next(cSol.Length);
                cSol[index] = rand.Next(periods * days);
            }



            return cSol;
        }
        static int[] GenRandSol()
        {
            int[] ans = new int[ExamNo];
            var rand = new Random();
            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = rand.Next(days * periods) + 1;
            }
            return ans;
        }
        static bool IsInConflict(int val, List<List<int>> solution, int count)
        {

            foreach (var item in SameStudentExams)
            {
                if (item.Contains(val))
                {
                    foreach (var item2 in solution[count])
                    {
                        if (item2 == val)
                            continue;
                        if (item.Contains(item2))
                            return true;
                    }
                }
            }
            return false;
        }
        static List<List<int>> ToSolution(int[] c)
        {
            List<List<int>> solution = new List<List<int>>();
            for (int i = 0; i < days * periods; i++)
            {
                solution.Add(new List<int>());
            }
            for (int i = 0; i < c.Length; i++)
            {
                int count = 0;
                int skipped = 0;
                while(IsInConflict(i, solution, count + skipped))
                    skipped++;
                while (count != c[i])
                {
                    if (IsInConflict(i, solution, count + skipped)) {
                        skipped++;
                        continue;
                    }
                        
                    count++;
                }
                while (IsInConflict(i, solution, count + skipped))
                    skipped++;
                if (count >= days * periods)
                    return null;
                solution[count + skipped].Add(i);
            }
            return solution;
        }
    }
}
