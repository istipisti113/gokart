using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace gokart
{
  static class Menu
  {
    //readkey v readline, readkey = true
    public static void menu(Dictionary<string, Action<int>> opciok, bool folyamatos, string question, bool keyorline) 
    {
      do {
        Console.Clear();
        if (question!=""){Console.WriteLine(question);}
        for (int i=0; i<opciok.Keys.Count(); i++)
        {
          Console.WriteLine($"{i+1} - {opciok.Keys.ToList()[i]}");
        }
        Console.WriteLine("0 - kilepes");
        string input = "";
        if (keyorline){ input = Console.ReadKey().KeyChar.ToString();
        } else {input = Console.ReadLine()??"0";}
        try{
          int valasztas = Convert.ToInt32(input);
          if (valasztas == 0){folyamatos = false;return;};
          opciok[opciok.Keys.ToList()[valasztas-1]](valasztas-1);
        } catch {
        }
      } while (folyamatos);
    }

  }
}
