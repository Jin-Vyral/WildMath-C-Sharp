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

    protected Dictionary<int, int> elements;
  }
}
