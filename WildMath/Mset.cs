// WildMath Library
//   By David Kaplan
//
//   Mset.cs
//
//   Defines the Mset (multiset) template class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildMath
{
  //NOTE: This class was used as an initial test. It hasn't been verified or maintained
  public class Mset<TYPE>
  {
    public Mset()
    {
      elements = new Dictionary<TYPE, int>();
    }

    public Mset(TYPE[] elements)
    {
      this.elements = new Dictionary<TYPE, int>(elements.Length);

      foreach(TYPE elem in elements)
        AddElement(elem);
    }

    public Mset(TYPE[] positiveElements, TYPE[] negativeElements)
    {
      this.elements = new Dictionary<TYPE, int>();

      foreach(TYPE elem in positiveElements)
        AddElement(elem);

      foreach(TYPE elem in negativeElements)
        SubtractElement(elem);
    }

    public void AddElement(TYPE item, int count = 1)
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

    public void SubtractElement(TYPE item, int count = 1)
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

    public static Mset<TYPE> operator *(Mset<TYPE> a, Mset<TYPE> b)
    {
      Mset<TYPE> sum = new Mset<TYPE>();

      foreach(KeyValuePair<TYPE, int> elem in a.elements)
      {
        int tot = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
          tot += bcnt;

        if(tot != 0)
          sum.elements.Add(elem.Key, tot);
      }

      foreach(KeyValuePair<TYPE, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key))
          sum.elements.Add(elem.Key, elem.Value);
      }

      return sum;
    }

    public static Mset<TYPE> operator /(Mset<TYPE> a, Mset<TYPE> b)
    {
      Mset<TYPE> diff = new Mset<TYPE>();

      foreach(KeyValuePair<TYPE, int> elem in a.elements)
      {
        int tot = elem.Value;
        int bcnt = 0;

        if(b.elements.TryGetValue(elem.Key, out bcnt))
          tot -= bcnt;

        if(tot != 0)
          diff.elements.Add(elem.Key, tot);
      }

      foreach(KeyValuePair<TYPE, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key))
          diff.elements.Add(elem.Key, -elem.Value);
      }

      return diff;
    }

    public static Mset<TYPE> operator +(Mset<TYPE> a, Mset<TYPE> b)
    {
      Mset<TYPE> min = new Mset<TYPE>();

      foreach(KeyValuePair<TYPE, int> elem in a.elements)
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

      foreach(KeyValuePair<TYPE, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key)&&(elem.Value < 0))
          min.elements.Add(elem.Key, elem.Value);
      }

      return min;
    }

    public static Mset<TYPE> operator -(Mset<TYPE> a, Mset<TYPE> b)
    {
      Mset<TYPE> max = new Mset<TYPE>();

      foreach(KeyValuePair<TYPE, int> elem in a.elements)
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

      foreach(KeyValuePair<TYPE, int> elem in b.elements)
      {
        if(!a.elements.ContainsKey(elem.Key) && (elem.Value > 0))
          max.elements.Add(elem.Key, elem.Value);
      }

      return max;
    }

    public override string ToString()
    {
      string str = "{ ";

      foreach(KeyValuePair<TYPE, int> elem in elements)
        str += elem.Key + "[" + elem.Value + "] ";

      return str + "}";
    }

    protected Dictionary<TYPE, int> elements;
  }
}
