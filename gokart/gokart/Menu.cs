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

    //public delegate void Action<in T1>(T1 arg) where T1 : allows ref struct;
    public delegate void Action<in T>(T obj) where T : allows ref struct;

    public static Dictionary<string, Action<int>> opciok = new Dictionary<string, Action<int>> {
      //["kilepes"] = () => {},
      ["berles"] = (i) => {Console.Clear(); berles(); Console.Clear();},
      ["kiiratas"] = (i) => {Console.Clear(); kiiratas(); Console.Clear();},
      ["modositas"] = (i) => {Console.Clear(); modositas();Console.Clear();},
      ["azonositok"] = (i) => {Console.Clear(); versenyazonositok();Console.Clear();},
    };

    public static void menu(Dictionary<string, Action<int>> opciok, bool folyamatos, string question)
    {
      do {
        Console.Clear();
        if (question!=""){Console.WriteLine(question);}
        for (int i=0; i<opciok.Keys.Count(); i++)
        {
          Console.WriteLine($"{i+1} - {opciok.Keys.ToList()[i]}");
        }
        Console.WriteLine("0 - kilepes");
        string input = Console.ReadKey().KeyChar.ToString();
        try{
          int valasztas = Convert.ToInt32(input);
          if (valasztas == 0){folyamatos = false;return;};
          opciok[opciok.Keys.ToList()[valasztas-1]](valasztas);
        } catch {
        }
      } while (folyamatos);
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
      List<List<idopont>> futamok = szabad_futamok();
      Dictionary<string, Action<int>> futamopciok = new Dictionary<string, Action<int>>();

      //menu opciok a nap kivalasztasahoz
      for (int i = 0;i<futamok.Count(); i++){
        futamopciok[futamok[i][0].nap.ToString()] = (kivalasztottNap) => {

          //menu opciok a napon beluli futam kivalasztasahoz
          int ii = i;
          Dictionary<string, Action<int>> idopont_opciok = new Dictionary<string, Action<int>>();
          for (int j = 0; j<futamok[ii-1].Count(); j++){ //a napon beluli futamok kozul lehet valasztani, futamok[i] a kivalasztott nap
            int jj = ii;
            idopont_opciok[$"futam {j}"] = (kivalasztottFutam) => {
              Console.Clear();
              futamok[kivalasztottNap][kivalasztottFutam].foglalas(Gokart.pilotak.Find(x=> x.versenyazonosito == azonosito));
              Console.WriteLine($"lefoglalva {kivalasztottNap} napra");
              Console.WriteLine("[enter] a visszalepeshez");
              Console.ReadLine();
              return;
            };
          }
          menu(idopont_opciok, false, "menyik idopontra szeretne foglalni? ");
          return;
        };
      }
      menu(futamopciok, false, "melyik napra szeretne foglalni? ");
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
