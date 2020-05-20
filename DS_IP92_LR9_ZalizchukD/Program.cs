using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace DS_IP92_LR9_ZalizchukD
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "input.txt";
            Graph graph = new Graph(path);
            
            graph.Traveler();
            //graph.Test();
        }
    }

    class Graph
    {
        private int n;
        private int[,] rebra;
        private double[,] distances;
        private List<int> rows, columns;
        private List<double> minRow, minColumn;

        public Graph(string path)
        {
            StreamReader sr = new StreamReader(path);
            string read = sr.ReadLine();
            string[] temp = read.Split(' ');
            n = Convert.ToInt32(temp[0]);
            distances = new double[n, n];
            rebra = new int[n, 2];
            rows = new List<int>();
            columns = new List<int>();
            
            for (int i = 0; i < n; i++)
            {
                read = sr.ReadLine();
                temp = read.Split(' ');
                int a = Convert.ToInt32(temp[0]),
                    b = Convert.ToInt32(temp[1]);
                rebra[i, 0] = a;
                rebra[i, 1] = b;
                rows.Add(i);
                columns.Add(i);
            }
            
            CalculateDistances();
        }

        private void CalculateDistances()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        distances[i, j] = Double.MaxValue;
                    else
                    {
                        distances[i, j] = Math.Round(Math.Sqrt(Math.Pow(rebra[j, 0] - rebra[i, 0], 2) + Math.Pow(rebra[j, 1] - rebra[i, 1], 2)), 2);
                    }
                }
            }
        }

        public void Output(List<List<double>> towns)
        {
            Console.Write($"{" ", 6}");
            for (int i = 0; i < columns.Count; i++)
            {
                Console.Write($"{columns[i] + 1, 6}");
            }
            Console.WriteLine();
            
            for (int i = 0; i < rows.Count; i++)
            {
                Console.Write($"{rows[i] + 1, 6}");
                for (int j = 0; j < n; j++)
                {
                    if (towns[i][j] != Double.MaxValue)
                        Console.Write($"{towns[i][j], 6}");
                    else
                    {
                        Console.Write($"{"INF", 6}");
                    }
                }
                Console.WriteLine();
            }
        }

        public void Traveler()
        {
            List<List<double>> towns = new List<List<double>>();
            InitTowns(towns);
            Output(towns);
            
            EachRowMin(towns);
            RowReduction(towns);
            EachColumnMin(towns);
            ColumnReduction(towns);

            
        }

        private void InitTowns(List<List<double>> towns)
        {
            for (int i = 0; i < n; i++)
            {
                towns.Add(new List<double>());
                for (int j = 0; j < n; j++)
                {
                    towns[i].Add(distances[i, j]);
                }
            }
        }

        private void EachRowMin(List<List<double>> towns)
        {
            minRow = new List<double>();
            for (int i = 0; i < towns.Count; i++)
            {
                minRow.Add(towns[i].Min());
            }
        }

        private void RowReduction(List<List<double>> towns)
        {
            for (int i = 0; i < towns.Count; i++)
            {
                for (int j = 0; j < towns[i].Count; j++)
                {
                    if (towns[i][j] != Double.MaxValue)
                        towns[i][j] = Math.Round(towns[i][j] - minRow[i], 2);
                }
            }
            
            Console.WriteLine("Редукция строк");
            Output(towns);
            
        }

        private void EachColumnMin(List<List<double>> towns)
        {
            minColumn = new List<double>();
            for (int i = 0; i < towns.Count; i++)
            {
                minColumn.Add(towns[0][i]);
                for (int j = 0; j < towns[i].Count; j++)
                {
                    if (minColumn[i] > towns[j][i])
                        minColumn[i] = towns[j][i];
                }
            }
        }

        private void ColumnReduction(List<List<double>> towns)
        {
            for (int i = 0; i < towns.Count; i++)
            {
                minColumn.Add(towns[0][i]);
                for (int j = 0; j < towns[i].Count; j++)
                {
                    towns[j][i] = Math.Round(towns[j][i] - minColumn[i], 2);
                }
            }
            
            Console.WriteLine("Редукция столбцов");
            Output(towns);
            
        }
        
    }
    
}