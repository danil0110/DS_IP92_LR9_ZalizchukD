using System;
using System.Collections.Immutable;
using System.IO;

namespace DS_IP92_LR9_ZalizchukD
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "input.txt";
            Graph graph = new Graph(path);
            
            graph.Test();
        }
    }

    class Graph
    {
        private int n;
        private int[,] rebra;
        private double[,] distances;

        public Graph(string path)
        {
            StreamReader sr = new StreamReader(path);
            string read = sr.ReadLine();
            string[] temp = read.Split(' ');
            n = Convert.ToInt32(temp[0]);
            distances = new double[n, n];
            rebra = new int[n, 2];
            
            for (int i = 0; i < n; i++)
            {
                read = sr.ReadLine();
                temp = read.Split(' ');
                int a = Convert.ToInt32(temp[0]),
                    b = Convert.ToInt32(temp[1]);
                rebra[i, 0] = a;
                rebra[i, 1] = b;
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

        public void Test()
        {
            Console.WriteLine("Координаты точек");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i + 1}: ({rebra[i, 0]}, {rebra[i, 1]});");
            }
            Console.WriteLine();
            Console.WriteLine("Матрица расстояний");

            Console.Write($"{" ", 6}");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{i + 1, 6}");
            }
            Console.WriteLine();
            
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{i + 1, 6}");
                for (int j = 0; j < n; j++)
                {
                    if (distances[i, j] != Double.MaxValue)
                        Console.Write($"{distances[i, j], 6}");
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
            
        }
        
    }
    
}