using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
  public class Pixel
  {
    public Pixel(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public int X { get { return x; } }
    public int Y { get { return y; } }

    public Pixel Transpose()
    {
      return new Pixel(y, x);
    }

    public bool IsDiagonal { get { return (x == y); } }
    public bool IsRowCollinear(Pixel p) { return (x == p.x); }
    public bool IsColumnCollinear(Pixel p) { return (y == p.y); }

    public bool IsEqual(Pixel p) { return ((x == p.x) && (y == p.y)); }

    public override bool Equals(object obj)
    {
      if((obj != null)&&(obj is Pixel))
        return this.Equals(obj as Pixel);

      return false;
    }

    public bool Equals(Pixel other)
    {
      return ((x == other.x) && (y == other.y));
    }

    public static bool operator ==(Pixel a, Pixel b)
    {
      if(a == null)
      {
        if(b == null)
          return false;

        return true;
      }

      return a.Equals(b);
    }

    public static bool operator !=(Pixel a, Pixel b)
    {
      if(a == null)
      {
        if(b == null)
          return true;

        return false;
      }

      return !a.Equals(b);
    }

    public override int GetHashCode()
    {
      return (int) (x * 0x00010000 + y);
    }

    public override string ToString()
    {
      return "(" + x.ToString() + "," + y.ToString() + ")";
    }

    protected int x;
    protected int y;
  }
}
