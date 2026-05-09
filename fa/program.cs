using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fans
{
  public class State
  {
    public string Name;
    public Dictionary<char, State> Transitions;
    public bool IsAcceptState;
  }

  public class FA1
  {
    State stInit = new State() { Name = "init", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State stOnes = new State() { Name = "ones", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State stZero = new State() { Name = "zero", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State stOk = new State() { Name = "ok", IsAcceptState = true, Transitions = new Dictionary<char, State>() };
    State stErr = new State() { Name = "err", IsAcceptState = false, Transitions = new Dictionary<char, State>() };

    public FA1()
    {
        stInit.Transitions['0'] = stZero;
        stInit.Transitions['1'] = stOnes;
        stOnes.Transitions['0'] = stOk;
        stOnes.Transitions['1'] = stOnes;
        stZero.Transitions['0'] = stErr;
        stZero.Transitions['1'] = stOk;
        stOk.Transitions['0'] = stErr;
        stOk.Transitions['1'] = stOk;
        stErr.Transitions['0'] = stErr;
        stErr.Transitions['1'] = stErr;
    }
        
    public bool? Run(IEnumerable<char> s)
    {
      State curr = stInit;
        foreach (var ch in s)
        {
            if (curr.Transitions.ContainsKey(ch))
                curr = curr.Transitions[ch];
            else
                return null;
        }
        return curr.IsAcceptState;
    }
  }

  public class FA2
  {
    State evEv = new State() { Name = "ee", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State evOd = new State() { Name = "eo", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State odEv = new State() { Name = "oe", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State odOd = new State() { Name = "oo", IsAcceptState = true, Transitions = new Dictionary<char, State>() };

    public FA2()
    {
        evEv.Transitions['0'] = odEv;
        evEv.Transitions['1'] = evOd;
        evOd.Transitions['0'] = odOd;
        evOd.Transitions['1'] = evEv;
        odEv.Transitions['0'] = evEv;
        odEv.Transitions['1'] = odOd;
        odOd.Transitions['0'] = evOd;
        odOd.Transitions['1'] = odEv;
    }
    public bool? Run(IEnumerable<char> s)
    {
      State curr = evEv;
        foreach (var ch in s)
        {
            if (curr.Transitions.ContainsKey(ch))
                curr = curr.Transitions[ch];
            else
                return null;
        }
        return curr.IsAcceptState;
    }
  }
  
  public class FA3
  {
    State qStart = new State() { Name = "start", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State qOne = new State() { Name = "one", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
    State qFinal = new State() { Name = "final", IsAcceptState = true, Transitions = new Dictionary<char, State>() };

    public FA3()
    {
        qStart.Transitions['0'] = qStart;
        qStart.Transitions['1'] = qOne;
        qOne.Transitions['0'] = qStart;
        qOne.Transitions['1'] = qFinal;
        qFinal.Transitions['0'] = qFinal;
        qFinal.Transitions['1'] = qFinal;
    }
    public bool? Run(IEnumerable<char> s)
    {
      State curr = qStart;
        foreach (var ch in s)
        {
            if (curr.Transitions.ContainsKey(ch))
                curr = curr.Transitions[ch];
            else
                return null;
        }
        return curr.IsAcceptState;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      String s = "01111";
      FA1 fa1 = new FA1();
      bool? result1 = fa1.Run(s);
      Console.WriteLine(result1);
      FA2 fa2 = new FA2();
      bool? result2 = fa2.Run(s);
      Console.WriteLine(result2);
      FA3 fa3 = new FA3();
      bool? result3 = fa3.Run(s);
      Console.WriteLine(result3);
    }
  }
}
