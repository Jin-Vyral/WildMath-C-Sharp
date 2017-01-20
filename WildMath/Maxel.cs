using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
  public class Maxel
  {
    public Maxel()
    {
      elements = new Dictionary<Pixel, int>();
    }

    public Maxel(Pixel[] elements)
    {
      this.elements = new Dictionary<Pixel, int>(elements.Length);

      foreach(Pixel elem in elements)
        AddElement(elem);
    }

    public Maxel(Pixel[] positiveElements, Pixel[] negativeElements)
    {
      this.elements = new Dictionary<Pixel, int>();

      foreach(Pixel elem in positiveElements)
        AddElement(elem);

      foreach(Pixel elem in negativeElements)
        SubtractElement(elem);
    }

    public int Size { get { return elements.Count; } }

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

    public void ClearElements()
    {
      elements.Clear();
    }

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

    public bool IsDiagonal
    {
      get
      {
        foreach(KeyValuePair<Pixel, int> elem in elements)
        {
          Pixel p = elem.Key;

          if(!p.IsDiagonal)
            return false;
        }

        return true;
      }
    }

    public Maxel Transpose()
    {
      Maxel trans = new Maxel();

      foreach(KeyValuePair<Pixel, int> elem in elements)
        trans.elements.Add(elem.Key.Transpose(), elem.Value);

      return trans;
    }

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

    public bool IsZero()
    {
      return elements.Count == 0; 
    }

    public static Maxel Zero { get { return new Maxel(); } }

    public override string ToString()
    {
      string str = "{ ";

      foreach(KeyValuePair<Pixel, int> elem in elements)
        str += elem.Key + "[" + elem.Value + "] ";

      return str + " }";
    }

    protected Dictionary<Pixel, int> elements;
  }
}

//Note: Deriving Maxel from Mset<Vexel> would save duplication but C# makes it near impossible to derive classes from templates
