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
    static int maxCount = 10;
    static int minValue = -10;
    static int maxValue = 10;
    static Random rand = new Random();

    static void Main(string[] args)
    {
      PrintGreeting();

      Console.WriteLine("Testing Maxels...");

      do
      {
        while(!Console.KeyAvailable)
        {
          Maxel a = GenerateRandomMaxel(maxCount, minValue, maxValue);
          Maxel b = GenerateRandomMaxel(maxCount, minValue, maxValue);
          Maxel c = GenerateRandomMaxel(maxCount, minValue, maxValue);

          // verify the math operations

          if((a * b) * c != a * (b * c))
          {
            Console.WriteLine("Failed!");
            break;
          }

          if((a + b) + c != a + (b + c))
          {
            Console.WriteLine("Failed!");
            break;
          }

          if(a * (b + c) != (a * b) + (a * c))
          {
            Console.WriteLine("Failed!");
            break;
          }

          if((a ^ (b * c)) != (a ^ b) * (a ^ c))
          {
            Console.WriteLine("Failed!");
            break;
          }

        }

        if(Console.ReadKey().Key == ConsoleKey.Enter)
          break;

        Console.WriteLine("Paused. Press Enter to exit, any other key to resume...");

        if(Console.ReadKey().Key == ConsoleKey.Enter)
          break;
      }
      while(true);

      Finish();
    }

    private static Maxel GenerateRandomMaxel(int maxCount, int minValue, int maxValue)
    {
      Pixel[] pos = new Pixel[rand.Next(maxCount)];
      for(int i = 0;i < pos.Length;i++)
        pos[i] = new Pixel(rand.Next(Program.minValue, Program.maxValue), rand.Next(Program.minValue, Program.maxValue));

      Pixel[] neg = new Pixel[rand.Next(maxCount)];
      for(int i = 0;i < neg.Length;i++)
        neg[i] = new Pixel(rand.Next(Program.minValue, Program.maxValue), rand.Next(Program.minValue, Program.maxValue));

      return new Maxel(pos, neg);
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
