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
    public static Dictionary<string, Action> opciok = new Dictionary<string, Action> {
      ["kilepes"] = () => {Gokart.running = false;},
      ["berles"] = () => {Console.Clear(); berles(); Console.ReadKey();Console.Clear();},
      ["kiiratas"] = () => {Console.Clear(); kiiratas(); Console.ReadKey();Console.Clear();},
      ["modositas"] = () => {Console.Clear(); modositas(); Console.ReadKey();Console.Clear();},
      ["azonositok"] = () => {Console.Clear(); versenyazonositok(); Console.ReadKey();Console.Clear();},
    };

    public static void menu(Dictionary<string, Action> opciok, bool running)
    {
      while (running) {
        Console.Clear();
        for (int i=0; i<opciok.Keys.Count(); i++)
        {
          Console.WriteLine($"{i} - {opciok.Keys.ToList()[i]}");
        }
        string input = Console.ReadKey().KeyChar.ToString();
        try{
          int valasztas = Convert.ToInt32(input);
          opciok[opciok.Keys.ToList()[valasztas]]();
        } catch {
        }
      }
    }

    public static void versenyazonositok(){
      Console.WriteLine(String.Join(", ", Gokart.pilotak.Select(x => x.versenyazonosito)));
    }

    public static void berles()
    {
      Console.Write("versenyazonosito: ");
      string azonosito = Console.ReadLine() ?? "";
      if (!Gokart.pilotak.Exists(x => x.versenyazonosito == azonosito)){
        Console.WriteLine("nincs ilyen versenyazonosito (nyomjon meg valamit a visszalepeshez)");
        Console.ReadKey();
        return;
      }
      Console.WriteLine("melyik napra szeretne foglalni? ");
      List<List<idopont>> futamok = szabad_futamok();
      Dictionary<string, Action> futamopciok = new Dictionary<string, Action>();
      foreach (var day in futamok){
        futamopciok[day[0].nap.ToString()] = () => {Console.WriteLine("asdffds");};
      }

      string nap = Console.ReadLine()??"";
      try {
        menu(futamopciok);
      }catch{
      }
    }

    public static void kiiratas()
    {

    }

    public static void modositas()
    {

    }
    
    static List<List<idopont>> szabad_futamok(){
      var returning = new List<List<idopont>>();
      Gokart.idopontok.ForEach(x=>{
        List<idopont> nap = new List<idopont>();
        x.ForEach(y=>{
          if (y.pilotak.Count()<20){
            nap.Add(y);
          }
        });
        if (nap.Count>0){returning.Add(nap);}
      });
      return returning;
    }
  }
}
