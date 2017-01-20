// WildMath Console
//   By David Kaplan
//
//   Program.cs
//
//   Used to test the WildMath library

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildMath;

namespace WMConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      PrintGreeting();

      Mset<int> M = new Mset<int>();

      M.AddElement(2);
      M.AddElement(3);
      M.AddElement(1);
      M.AddElement(2);
      M.AddElement(1);
      M.AddElement(1, 2);

      M.SubtractElement(1, 3);
      M.SubtractElement(2, 4);

      Mset<int> N = new Mset<int>(new int[] { 1, 1, 2, 2, 2, 3 });
      Mset<int> O = new Mset<int>(new int[] { 1, 1, 2, 2, 2, 3 }, new int[] { 1, 2, 4, 4, 4 });

      Mset<int> P = M * N; // addition
      Mset<int> Q = M / N; // subtraction
      Mset<int> R = O + M; // union
      Mset<int> S = O - M; // intersection

      Console.WriteLine("M = " + M);
      Console.WriteLine("N = " + N);
      Console.WriteLine("O = " + O);
      Console.WriteLine("M * N = " + P);
      Console.WriteLine("M / N = " + Q);
      Console.WriteLine("O + N = " + R);
      Console.WriteLine("O - N = " + S);

      Maxel X = new Maxel();

      X.AddElement(new Pixel(1, 1));
      X.AddElement(new Pixel(1, 1));
      X.AddElement(new Pixel(2, 2));
      X.AddElement(new Pixel(2, 2));
      X.AddElement(new Pixel(2, 2));
      X.AddElement(new Pixel(-2, -3), 2);
      X.AddElement(new Pixel(-3, -2), 2);
      X.SubtractElement(new Pixel(3, 3), 2);

      Maxel Y = new Maxel(new Pixel[] { new Pixel(1, 1), new Pixel(3, 3), new Pixel(4, 4), new Pixel(4, 4) });

      Maxel Z = X * Y;
      Maxel W = X / Y;
      Maxel V = X + Y;
      Maxel U = X - Y;

      Console.WriteLine();
      Console.WriteLine("X = " + X);
      Console.WriteLine("Y = " + Y);
      Console.WriteLine("X * Y = " + Z);
      Console.WriteLine("X / Y = " + W);
      Console.WriteLine("X + Y = " + V);
      Console.WriteLine("X - Y = " + U);

      Finish();
    }

    private static void PrintGreeting()
    {
      Console.WriteLine("WildMath Console");
      Console.WriteLine("By David Kaplan");
      Console.WriteLine("Based on the teachings of Dr Norman Wildberger UNSW");
      Console.WriteLine();
      Console.WriteLine("Running...");
      Console.WriteLine();
      Console.WriteLine();
    }

    private static void Finish()
    {
      Console.WriteLine();
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
      Console.WriteLine();
      Console.WriteLine();
    }
  }
}
