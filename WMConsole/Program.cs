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
    static int maxElements = 7;
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
          Maxel a = GenerateRandomMaxel(maxElements, minValue, maxValue, minCount, maxCount);
          Maxel b = GenerateRandomMaxel(maxElements, minValue, maxValue, minCount, maxCount);
          Maxel c = GenerateRandomMaxel(maxElements, minValue, maxValue, minCount, maxCount);
          Maxel d = GenerateRandomMaxel(maxElements, minValue, maxValue, minCount, maxCount);

          // verify the math operations

          if((a * b) * c != a * (b * c))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((a + b) + c != a + (b + c))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a * b != b * a)
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a + b != b + a)
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a * (b + c) != (a * b) + (a * c))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((a ^ (b * c)) != (a ^ b) * (a ^ c))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a + a != a)
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((Maxel.Zero ^ a) != Maxel.Zero)
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((0 ^ a) != Maxel.Zero)
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a * Maxel.Zero != a)
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a != ~(~a))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a / b != a * (~b))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((a * c) / (b * c) != a / b)
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a - b != a * b / (a + b))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((a / b) + (c / d) != ((a * d) + (b * c)) / (b * d))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a + ((b * c) / (b + c)) != ((a + b) * (a + c)) / ((a + b) + (a + c)))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a + (b - c) != (a + b) - (a + c))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a + b + c != a * b * c / (a - b) / (a - c) / (b - c) * (a - b - c))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((a * b) >> 1 != (a >> 1) * (b >> 1))
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if(a != a.Transpose().Transpose())
          {
            MaxelFail(a, b, c, d);
            break;
          }

          if((a ^ b).Transpose() != ((b.Transpose()) ^ (a.Transpose())))
          {
            MaxelFail(a, b, c, d);
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

    private static Maxel GenerateRandomMaxel(int maxElements, int minValue, int maxValue, int minCount, int maxCount)
    {
      Maxel m = new Maxel();
      int elementCount = rand.Next(maxElements + 1);

      for(int i = 0;i < elementCount;i++)
        m.AddElement(new Pixel(rand.Next(Program.minValue, Program.maxValue), rand.Next(Program.minValue, Program.maxValue)), rand.Next(minCount, maxCount));

      return m;
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
  }
}
