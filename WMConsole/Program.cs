// WildMath Console
//   By David Kaplan
//   Based on "Maxel Theory" of Dr. Norman Wildberger UNSW
//
//   Program.cs
//
//   Used to test the WildMath library

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WildMath;

namespace WMConsole
{
  class Program
  {
    static int maxElements = 50;
    static int minValue = -5;
    static int maxValue = 5;
    static int minCount = -10;
    static int maxCount = 10;
    static Random rand = new Random();

    static void Main(string[] args)
    {
      PrintGreeting();

      Console.WriteLine("Testing Maxels...");

      int testsRun = 0;

      while(true)
      {
        while(true)
        {
          Maxel a = GenerateRandomMaxel();
          Maxel b = GenerateRandomMaxel();
          Maxel c = GenerateRandomMaxel();
          Maxel d = GenerateRandomMaxel();

          // verify the Maxel math operations

          if(((a * b) * c != a * (b * c))
          ||((a + b) + c != a + (b + c))
          ||(a * b != b * a)
          ||(a + b != b + a)
          ||(a * (b + c) != (a * b) + (a * c))
          ||((a ^ (b * c)) != (a ^ b) * (a ^ c))
          ||(a + a != a)
          ||((Maxel.Zero ^ a) != Maxel.Zero)
          ||((0 ^ a) != Maxel.Zero)
          ||(a * Maxel.Zero != a)
          ||(a != ~(~a))
          ||(a / b != a * (~b))
          ||((a * c) / (b * c) != a / b)
          ||(a - b != a * b / (a + b))
          ||((a / b) + (c / d) != ((a * d) + (b * c)) / (b * d))
          ||(a + ((b * c) / (b + c)) != ((a + b) * (a + c)) / ((a + b) + (a + c)))
          ||(a + (b - c) != (a + b) - (a + c))
          ||(a + b + c != a * b * c / (a - b) / (a - c) / (b - c) * (a - b - c))
          ||((a * b) >> 1 != (a >> 1) * (b >> 1))
          ||(a != a.Transpose().Transpose())
          ||((a ^ b).Transpose() != ((b.Transpose()) ^ (a.Transpose()))))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          // verify the Vexel math operations

          Vexel r = (a * b * c * d).Row(rand.Next(minValue, maxValue));
          Vexel s = r.Support;
          Vexel t = Vexel.Zero;
          Vexel u = Vexel.Zero;

          if((!s.IsSupport)
          ||(Vexel.Cross(r, r).Support != Vexel.Cross(s, s)))
          {
            VexelFail(r, s, t, u);
            break;
          }

          r = GenerateRandomVexel();
          s = GenerateRandomVexel();
          t = GenerateRandomVexel();
          u = GenerateRandomVexel();

          if(((r * s) * t != r * (s * t))
          ||((r + s) + t != r + (s + t))
          ||(r * s != s * r)
          ||(r + s != s + r)
          ||(r * (s + t) != (r * s) + (r * t))
          ||((r ^ (s * t)) != (r ^ s) * (r ^ t))
          ||(r + r != r)
          ||((Vexel.Zero ^ r) != Vexel.Zero)
          ||((0 ^ r) != Vexel.Zero)
          ||(r * Vexel.Zero != r)
          ||(r != ~(~r))
          ||(r / s != r * (~s))
          ||((r * t) / (s * t) != r / s)
          ||(r - s != r * s / (r + s))
          ||((r / s) + (t / u) != ((r * u) + (s * t)) / (s * u))
          ||(r + ((s * t) / (s + t)) != ((r + s) * (r + t)) / ((r + s) + (r + t)))
          ||(r + (s - t) != (r + s) - (r + t))
          ||(r + s + t != r * s * t / (r - s) / (r - t) / (s - t) * (r - s - t))
          ||((r * s) >> 1 != (r >> 1) * (s >> 1)))
          {
            VexelFail(r, s, t, u);
            break;
          }

          if(Console.KeyAvailable)
          {
            Console.WriteLine();
            Console.WriteLine("Paused");
            Console.WriteLine("Last test values:");
            Console.WriteLine("a = " + a);
            Console.WriteLine("b = " + b);
            Console.WriteLine("c = " + c);
            Console.WriteLine("d = " + d);
            Console.WriteLine("r = " + r);
            Console.WriteLine("s = " + s);
            Console.WriteLine("t = " + t);
            Console.WriteLine("u = " + u);
            Console.WriteLine();
            Console.WriteLine(++testsRun + " tests passed!");
            break;
          }

          if(++testsRun % 1000 == 0)
          {
            if(testsRun % 10000 == 0)
              Console.WriteLine(" " + (testsRun / 1000) + "k tests passed!");
            else
              Console.Write(".");
          }
        }

        if(Console.ReadKey().Key == ConsoleKey.Enter)
          break;

        Console.WriteLine();
        Console.WriteLine("Press Enter to exit, any other key to resume...");

        if(Console.ReadKey().Key == ConsoleKey.Enter)
          break;

        Console.WriteLine();
        Console.WriteLine("Resuming...");
      }

      Finish();
    }

    private static Maxel GenerateRandomMaxel()
    {
      Maxel m = new Maxel();
      int elementCount = rand.Next(maxElements + 1);

      for(int i = 0;i < elementCount;i++)
        m.AddElement(new Pixel(rand.Next(Program.minValue, Program.maxValue), rand.Next(Program.minValue, Program.maxValue)), rand.Next(minCount, maxCount));

      return m;
    }

    private static Vexel GenerateRandomVexel()
    {
      Vexel v = new Vexel();
      int elementCount = rand.Next(maxElements + 1);

      for(int i = 0;i < elementCount;i++)
        v.AddElement(rand.Next(Program.minValue, Program.maxValue), rand.Next(minCount, maxCount));

      return v;
    }

    private static void PrintGreeting()
    {
      Console.WriteLine("WildMath Console");
      Console.WriteLine("By David Kaplan");
      Console.WriteLine("Based on \"Maxel Theory\" of Dr. Norman Wildberger UNSW");
      Console.WriteLine();
      Console.WriteLine("Running...");
      Console.WriteLine();
      Console.WriteLine();
    }

    private static void Finish()
    {
      Console.WriteLine();
      Console.WriteLine("Exiting...");
      Console.WriteLine();
      Console.WriteLine();
    }

    private static void MaxelFail(Maxel a, Maxel b, Maxel c, Maxel d)
    {
      Console.WriteLine();
      Console.WriteLine("Maxel test failed!");
      Console.WriteLine("Last test values:");
      Console.WriteLine("a = " + a);
      Console.WriteLine("b = " + b);
      Console.WriteLine("c = " + c);
      Console.WriteLine("d = " + d);
      Console.WriteLine();
    }

    private static void VexelFail(Vexel r, Vexel s, Vexel t, Vexel u)
    {
      Console.WriteLine();
      Console.WriteLine("Vexel test failed!");
      Console.WriteLine("Last test values:");
      Console.WriteLine("r = " + r);
      Console.WriteLine("s = " + s);
      Console.WriteLine("t = " + t);
      Console.WriteLine("u = " + u);
      Console.WriteLine();
    }
  }
}
