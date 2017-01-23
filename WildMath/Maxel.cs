// WildMath Library
//   By David Kaplan
//   Based on "Maxel Theory" of Dr. Norman Wildberger UNSW
//
//   Maxel.cs
//
//   Defines the Maxel class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
  ///<summary>
  /// The Maxel class
  ///</summary>
  public class Maxel
  {
    public Maxel()
    {
      elements = new Dictionary<Pixel, int>();
    }

    ///<summary>
    /// Creates a Maxel with a given set of Pixels to add
    ///</summary>
    public Maxel(Pixel[] elements)
    {
      this.elements = new Dictionary<Pixel, int>(elements.Length);

      foreach(Pixel elem in elements)
        AddElement(elem);
    }

    ///<summary>
    /// Creates a Maxel with given sets of Pixels to add and subtract
    ///</summary>
    public Maxel(Pixel[] positiveElements, Pixel[] negativeElements)
    {
      this.elements = new Dictionary<Pixel, int>();

      foreach(Pixel elem in positiveElements)
        AddElement(elem);

      foreach(Pixel elem in negativeElements)
        SubtractElement(elem);
    }

		///<summary>
		/// Creates a Maxel with the same elements as the Maxel 'other'
		///</summary>
		public Maxel(Maxel other)
		{
			elements = new Dictionary<Pixel, int>(other.elements.Count);

			foreach(KeyValuePair<Pixel, int> elem in other.elements)
				AddElement(elem.Key, elem.Value);
		}

		///<summary>
		/// The number of unique pixels in the Maxel
		///</summary>
		public int Count { get { return elements.Count; } }

    ///<summary>
    /// The magnitude of the Maxel (can be negative or zero)
    /// NOTE: This is implemented as defined in MF159
    ///</summary>
    public int Size
    {
      get
      {
        int size = 0;

        foreach(KeyValuePair<Pixel, int> elem in elements)
          size += elem.Value;

        return size;
      }
    }

    ///<summary>
    /// Adds an individual Pixel to the Maxel [count] times
    ///</summary>
    public void AddElement(Pixel item, int count = 1)
    {
      if(count == 0)
        return;

      int current = 0;

      if(elements.TryGetValue(item, out current))
      {
        int newCount = current + count;
        if(newCount == 0)
          elements.Remove(item);
        else
          elements[item] = newCount;
      }
      else
        elements.Add(item, count);
    }

    ///<summary>
    /// Subtracts an individual Pixel from the Maxel [count] times
    ///</summary>
    public void SubtractElement(Pixel item, int count = 1)
    {
      if(count == 0)
        return;

      int current = 0;

      if(elements.TryGetValue(item, out current))
      {
        int newCount = current - count;
        if(newCount == 0)
          elements.Remove(item);
        else
          elements[item] = newCount;
      }
      else
        elements.Add(item, -count);
    }

    ///<summary>
    /// Zeroes-out the Maxel (removes all Pixels)
    ///</summary>
    public void ClearElements()
    {
      elements.Clear();
    }

    public Dictionary<Pixel, int> Elements { get { return elements; } }

    ///<summary>
    /// Adds Maxel 'b' to Maxel 'a'
    ///</summary>
    public static Maxel operator *(Maxel a, Maxel b)
    {
      Maxel sum = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in a.elements)
      {
        int tot = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
          tot += bcnt;

        if(tot != 0)
          sum.elements.Add(elem.Key, tot);
      }

      foreach(KeyValuePair<Pixel, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key))
          sum.elements.Add(elem.Key, elem.Value);
      }

      return sum;
    }

    ///<summary>
    /// Subtracts Maxel 'b' from Maxel 'a'
    ///</summary>
    public static Maxel operator /(Maxel a, Maxel b)
    {
      Maxel diff = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in a.elements)
      {
        int tot = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
          tot -= bcnt;

        if(tot != 0)
          diff.elements.Add(elem.Key, tot);
      }

      foreach(KeyValuePair<Pixel, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key))
          diff.elements.Add(elem.Key, -elem.Value);
      }

      return diff;
    }

    ///<summary>
    /// Returns the intersection of Maxels 'a' and 'b'
    ///</summary>
    public static Maxel operator -(Maxel a, Maxel b)
    {
      Maxel min = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in a.elements)
      {
        int acnt = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
        {
          if(acnt < bcnt)
            min.elements.Add(elem.Key, acnt);
          else
            min.elements.Add(elem.Key, bcnt);
        }
        else if(acnt < 0)
          min.elements.Add(elem.Key, acnt);
      }

      foreach(KeyValuePair<Pixel, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key) && (elem.Value < 0))
          min.elements.Add(elem.Key, elem.Value);
      }

      return min;
    }

    ///<summary>
    /// Returns the union of Maxels 'a' and 'b'
    ///</summary>
    public static Maxel operator +(Maxel a, Maxel b)
    {
      Maxel max = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in a.elements)
      {
        int acnt = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
        {
          if(acnt > bcnt)
            max.elements.Add(elem.Key, acnt);
          else
            max.elements.Add(elem.Key, bcnt);
        }
        else if(acnt > 0)
          max.elements.Add(elem.Key, acnt);
      }

      foreach(KeyValuePair<Pixel, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key) && (elem.Value > 0))
          max.elements.Add(elem.Key, elem.Value);
      }

      return max;
    }

    ///<summary>
    /// Multiplies Maxel 'a' by Maxel 'b'
    ///</summary>
    public static Maxel operator ^(Maxel a, Maxel b)
    {
      Maxel mul = new Maxel();

      foreach(KeyValuePair<Pixel, int> ae in a.elements)
      {
        foreach(KeyValuePair<Pixel, int> be in b.elements)
        {
          if(ae.Key.Y == be.Key.X)
            mul.AddElement(new Pixel(ae.Key.X, be.Key.Y), ae.Value * be.Value);
        }
      }

      return mul;
    }

    ///<summary>
    /// Multiplies Maxel 'a' by integer 'b'
    ///</summary>
    public static Maxel operator ^(Maxel a, int b)
    {
      Maxel mul = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in a.elements)
        mul.AddElement(elem.Key, elem.Value * b);

      return mul;
    }

    ///<summary>
    /// Multiplies Maxel 'a' by integer 'b'
    ///</summary>
    public static Maxel operator ^(int b, Maxel a)
    {
      return a ^ b;
    }

    ///<summary>
    /// Shifts Maxel 'a' up the diagonal by 'k' steps
    ///</summary>
    public static Maxel operator <<(Maxel a, int k)
    {
      Maxel shift = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in a.elements)
        shift.elements.Add(new Pixel(elem.Key.X + k, elem.Key.Y + k), elem.Value);

      return shift;
    }

    ///<summary>
    /// Shifts Maxel 'a' down the diagonal by 'k' steps
    ///</summary>
    public static Maxel operator >>(Maxel a, int k)
    {
      Maxel shift = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in a.elements)
        shift.elements.Add(new Pixel(elem.Key.X - k, elem.Key.Y - k), elem.Value);

      return shift;
    }

    ///<summary>
    /// Negates Maxel 'm'
    ///</summary>
    public static Maxel operator ~(Maxel m)
    {
      Maxel neg = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in m.elements)
        neg.elements.Add(elem.Key, -elem.Value);

      return neg;
    }

    ///<summary>
    /// Implements C# generic Equals method
    ///</summary>
    public override bool Equals(object obj)
    {
      if((obj != null) && (obj is Maxel))
        return this.Equals(obj as Maxel);

      return false;
    }

    ///<summary>
    /// Tests for equality with Maxel 'other'
    ///</summary>
    public bool Equals(Maxel other)
    {
      if(elements.Count != other.elements.Count)
        return false;

      foreach(KeyValuePair<Pixel, int> elem in elements)
      {
        int bcnt = 0;
        if(!other.elements.TryGetValue(elem.Key, out bcnt) || (bcnt != elem.Value))
          return false;
      }

      return true;
    }

    ///<summary>
    /// Tests for equality between Maxels 'a' and 'b'
    ///</summary>
    public static bool operator ==(Maxel a, Maxel b)
    {
      if(ReferenceEquals(a, null))
      {
        if(ReferenceEquals(b, null))
          return true;

        return false;
      }

      return a.Equals(b);
    }

    ///<summary>
    /// Tests for inequality between Maxels 'a' and 'b'
    ///</summary>
    public static bool operator !=(Maxel a, Maxel b)
    {
      return !(a == b);
    }

    ///<summary>
    /// Returns default hash code
    ///</summary>
    public override int GetHashCode()
    {
      // Any ideas for a good hash are welcome
      // For now this is just here to get it to compile
      return base.GetHashCode();
    }

    ///<summary>
    /// The extent of the Maxel
    ///</summary>
    public Pixel Extent
    {
      get
      {
        int xMin = 0;
        int xMax = 0;
        int yMin = 0;
        int yMax = 0;

        foreach(KeyValuePair<Pixel, int> elem in elements)
        {
          Pixel p = elem.Key;

          if(p.X < xMin)
            xMin = p.X;
          else if(p.X > xMax)
            xMax = p.X;
          if(p.Y < yMin)
            yMin = p.Y;
          else if(p.Y > yMax)
            yMax = p.Y;
        }

        return new Pixel(xMax - xMin, yMax - yMin);
      }
    }

    ///<summary>
    /// The support of the Maxel
    ///</summary>
    public Maxel Support
    {
      get
      {
        Maxel supp = new Maxel();

        foreach(KeyValuePair<Pixel, int> elem in elements)
          supp.AddElement(elem.Key);

        return supp;
      }
    }

    ///<summary>
    /// Is the Maxel a support?
    ///</summary>
    public bool IsSupport
    {
      get
      {
        foreach(KeyValuePair<Pixel, int> elem in elements)
        {
          if(elem.Value != 1)
            return false;
        }

        return true;
      }
    }

    ///<summary>
    /// Is the Maxel diagonal?
    ///</summary>
    public bool IsDiagonal
    {
      get
      {
        foreach(KeyValuePair<Pixel, int> elem in elements)
        {
          if(!elem.Key.IsDiagonal)
            return false;
        }

        return true;
      }
    }

    ///<summary>
    /// Transposes the Maxel
    ///</summary>
    public Maxel Transpose()
    {
      Maxel trans = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in elements)
        trans.elements.Add(elem.Key.Transpose(), elem.Value);

      return trans;
    }

    ///<summary>
    /// Is the Maxel symmetric?
    ///</summary>
    public bool IsSymmetric
    {
      get
      {
        foreach(KeyValuePair<Pixel, int> elem in elements)
        {
          Pixel p = elem.Key;

          if(!p.IsDiagonal)
          {
            int sym = 0;

            if(!elements.TryGetValue(new Pixel(p.Y, p.X), out sym) || (sym != elem.Value))
              return false;
          }
        }

        return true;
      }
    }

    ///<summary>
    /// Returns a Vexel of the given 'row' in the Maxel
    ///</summary>
    public Vexel Row(int row)
    {
      Vexel rv = new Vexel();

      foreach(KeyValuePair<Pixel, int> elem in elements)
      {
        if(elem.Key.X == row)
          rv.AddElement(elem.Key.Y, elem.Value);
      }

      return rv;
    }

    ///<summary>
    /// Returns a Vexel of the given 'column' in the Maxel
    ///</summary>
    public Vexel Column(int column)
    {
      Vexel cv = new Vexel();

      foreach(KeyValuePair<Pixel, int> elem in elements)
      {
        if(elem.Key.Y == column)
          cv.AddElement(elem.Key.X, elem.Value);
      }

      return cv;
    }

    ///<summary>
    /// Is the Maxel zero?
    ///</summary>
    public bool IsZero()
    {
      return elements.Count == 0; 
    }

    ///<summary>
    /// The Zero Maxel
    ///</summary>
    public static Maxel Zero { get { return new Maxel(); } }

    ///<summary>
    /// Converts the Maxel into a more readable form
    ///</summary>
    public override string ToString()
    {
      string str = "[ ";

      foreach(KeyValuePair<Pixel, int> elem in elements)
        str += elem.Key + "[" + elem.Value + "] ";

      return str + "]";
    }

    ///<summary>
    /// The dictionary of Pixels and their associated multiplicities
    ///</summary>
    protected Dictionary<Pixel, int> elements;
  }
}

//Note: Deriving Maxel from Mset<Vexel> would save duplication but C# makes it near impossible to derive classes from templates
