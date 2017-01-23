// WildMath Library
//   By David Kaplan
//   Based on "Vexel Theory" of Dr. Norman Wildberger UNSW
//
//   Vexel.cs
//
//   Defines the Vexel class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
  ///<summary>
  /// The Vexel class
  ///</summary>
  public class Vexel
  {
    public Vexel()
    {
      elements = new Dictionary<int, int>();
    }

    ///<summary>
    /// Creates a Vexel with a given set of ints to add
    ///</summary>
    public Vexel(int[] elements)
    {
      this.elements = new Dictionary<int, int>(elements.Length);

      foreach(int elem in elements)
        AddElement(elem);
    }

    ///<summary>
    /// Creates a Vexel with given sets of ints to add and subtract
    ///</summary>
    public Vexel(int[] positiveElements, int[] negativeElements)
    {
      this.elements = new Dictionary<int, int>();

      foreach(int elem in positiveElements)
        AddElement(elem);

      foreach(int elem in negativeElements)
        SubtractElement(elem);
    }

    ///<summary>
    /// The number of unique ints in the Vexel
    ///</summary>
    public int Count { get { return elements.Count; } }

    ///<summary>
    /// The magnitude of the Vexel (can be negative or zero)
    /// NOTE: This is implemented as defined in MF159
    ///</summary>
    public int Size
    {
      get
      {
        int size = 0;

        foreach(KeyValuePair<int, int> elem in elements)
          size += elem.Value;

        return size;
      }
    }

    ///<summary>
    /// Adds an individual int to the Vexel [count] times
    ///</summary>
    public void AddElement(int item, int count = 1)
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
    /// Subtracts an individual int from the Vexel [count] times
    ///</summary>
    public void SubtractElement(int item, int count = 1)
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
    /// Zeroes-out the Vexel (removes all ints)
    ///</summary>
    public void ClearElements()
    {
      elements.Clear();
    }

    public Dictionary<int, int> Elements { get { return elements; } }

    ///<summary>
    /// Adds Maxel 'b' to Maxel 'a'
    ///</summary>
    public static Vexel operator *(Vexel a, Vexel b)
    {
      Vexel sum = new Vexel();

      foreach(KeyValuePair<int, int> elem in a.elements)
      {
        int tot = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
          tot += bcnt;

        if(tot != 0)
          sum.elements.Add(elem.Key, tot);
      }

      foreach(KeyValuePair<int, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key))
          sum.elements.Add(elem.Key, elem.Value);
      }

      return sum;
    }

    ///<summary>
    /// Subtracts Vexel 'b' from Vexel 'a'
    ///</summary>
    public static Vexel operator /(Vexel a, Vexel b)
    {
      Vexel diff = new Vexel();

      foreach(KeyValuePair<int, int> elem in a.elements)
      {
        int tot = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
          tot -= bcnt;

        if(tot != 0)
          diff.elements.Add(elem.Key, tot);
      }

      foreach(KeyValuePair<int, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key))
          diff.elements.Add(elem.Key, -elem.Value);
      }

      return diff;
    }

    ///<summary>
    /// Returns the intersection of Vexels 'a' and 'b'
    ///</summary>
    public static Vexel operator -(Vexel a, Vexel b)
    {
      Vexel min = new Vexel();

      foreach(KeyValuePair<int, int> elem in a.elements)
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

      foreach(KeyValuePair<int, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key) && (elem.Value < 0))
          min.elements.Add(elem.Key, elem.Value);
      }

      return min;
    }

    ///<summary>
    /// Returns the union of Vexels 'a' and 'b'
    ///</summary>
    public static Vexel operator +(Vexel a, Vexel b)
    {
      Vexel max = new Vexel();

      foreach(KeyValuePair<int, int> elem in a.elements)
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

      foreach(KeyValuePair<int, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key) && (elem.Value > 0))
          max.elements.Add(elem.Key, elem.Value);
      }

      return max;
    }

    ///<summary>
    /// Multiplies Vexel 'a' by Vexel 'b'
    ///</summary>
    public static Vexel operator ^(Vexel a, Vexel b)
    {
      Vexel mul = new Vexel();

      foreach(KeyValuePair<int, int> ae in a.elements)
      {
        foreach(KeyValuePair<int, int> be in b.elements)
          mul.AddElement(ae.Key + be.Key, ae.Value * be.Value);
      }

      return mul;
    }

    ///<summary>
    /// Multiplies Vexel 'a' by integer 'b'
    ///</summary>
    public static Vexel operator ^(Vexel a, int b)
    {
      Vexel mul = new Vexel();

      foreach(KeyValuePair<int, int> elem in a.elements)
        mul.AddElement(elem.Key, elem.Value * b);

      return mul;
    }

    ///<summary>
    /// Multiplies Vexel 'a' by integer 'b'
    ///</summary>
    public static Vexel operator ^(int b, Vexel a)
    {
      return a ^ b;
    }

    ///<summary>
    /// Shifts Vexel 'a' up by 'k' steps
    ///</summary>
    public static Vexel operator <<(Vexel a, int k)
    {
      Vexel shift = new Vexel();

      foreach(KeyValuePair<int, int> elem in a.elements)
        shift.elements.Add(elem.Key + k, elem.Value);

      return shift;
    }

    ///<summary>
    /// Shifts Vexel 'a' down by 'k' steps
    ///</summary>
    public static Vexel operator >>(Vexel a, int k)
    {
      Vexel shift = new Vexel();

      foreach(KeyValuePair<int, int> elem in a.elements)
        shift.elements.Add(elem.Key - k, elem.Value);

      return shift;
    }

    ///<summary>
    /// Negates Maxel 'm'
    ///</summary>
    public static Vexel operator ~(Vexel v)
    {
      Vexel neg = new Vexel();

      foreach(KeyValuePair<int, int> elem in v.elements)
        neg.elements.Add(elem.Key, -elem.Value);

      return neg;
    }

    ///<summary>
    /// Implements C# generic Equals method
    ///</summary>
    public override bool Equals(object obj)
    {
      if((obj != null) && (obj is Vexel))
        return this.Equals(obj as Vexel);

      return false;
    }

    ///<summary>
    /// Tests for equality with Vexel 'other'
    ///</summary>
    public bool Equals(Vexel other)
    {
      if(elements.Count != other.elements.Count)
        return false;

      foreach(KeyValuePair<int, int> elem in elements)
      {
        int bcnt = 0;
        if(!other.elements.TryGetValue(elem.Key, out bcnt) || (bcnt != elem.Value))
          return false;
      }

      return true;
    }

    ///<summary>
    /// Tests for equality between Vexels 'a' and 'b'
    ///</summary>
    public static bool operator ==(Vexel a, Vexel b)
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
    /// Tests for inequality between Vexels 'a' and 'b'
    ///</summary>
    public static bool operator !=(Vexel a, Vexel b)
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
    /// Returns the cross-Maxel of Vexel 'a' and Vexel 'b'
    ///</summary>
    public static Maxel Cross(Vexel a, Vexel b)
    {
      Maxel cross = new Maxel();

      foreach(KeyValuePair<int, int> ae in a.elements)
      {
        foreach(KeyValuePair<int, int> be in b.elements)
          cross.AddElement(new Pixel(ae.Key, be.Key), ae.Value * be.Value);
      }

      return cross;
    }

    ///<summary>
    /// The support of the Vexel
    ///</summary>
    public Vexel Support
    {
      get
      {
        Vexel supp = new Vexel();

        foreach(KeyValuePair<int, int> elem in elements)
          supp.AddElement(elem.Key);

        return supp;
      }
    }

    ///<summary>
    /// Is the Vexel a support?
    ///</summary>
    public bool IsSupport
    {
      get
      {
        foreach(KeyValuePair<int, int> elem in elements)
        {
          if(elem.Value != 1)
            return false;
        }

        return true;
      }
    }

    ///<summary>
    /// Is the Vexel zero?
    ///</summary>
    public bool IsZero()
    {
      return elements.Count == 0;
    }

    ///<summary>
    /// The Zero Vexel
    ///</summary>
    public static Vexel Zero { get { return new Vexel(); } }

    ///<summary>
    /// Converts the Vexel into a more readable form
    ///</summary>
    public override string ToString()
    {
      string str = "[ ";

      foreach(KeyValuePair<int, int> elem in elements)
        str += elem.Key + "[" + elem.Value + "] ";

      return str + "]";
    }

    protected Dictionary<int, int> elements;
  }
}
