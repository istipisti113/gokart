using System.Collections;
using System.Diagnostics.Tracing;

namespace gokart
{
  public static class Gokart
  {
    static public string Ekezettelenit(string nev)
    {
      Hashtable betuk = new Hashtable();
      betuk['é'] = 'e';
      betuk['É'] = 'E';
      betuk['á'] = 'a';
      betuk['Á'] = 'A';
      betuk['ö'] = 'o';
      betuk['Ö'] = 'O';
      betuk['ő'] = 'o';
      betuk['Ő'] = 'O';
      betuk['ó'] = 'o';
      betuk['Ó'] = 'O';
      betuk['ú'] = 'u';
      betuk['Ú'] = 'U';
      betuk['ű'] = 'u';
      betuk['Ű'] = 'U';
      betuk['ü'] = 'u';
      betuk['Ü'] = 'U';
      betuk['í'] = 'i';
      betuk['Í'] = 'I';
      string returning = "";
      for (int i = 0; i < nev.Length; i++)
      {
        if (betuk.ContainsKey(nev[i]))
        {
          returning += betuk[nev[i]];
        }
        else
        {
          returning += nev[i];
        }
      }
      return returning;
    }

    public static List<pilota> pilotak = new List<pilota>();
    public static bool running = true;
    public static List<List<idopont>> idopontok = new List<List<idopont>>();

    static void Main(string[] args)
    {
      Hashtable info = new Hashtable();
      info["nev"] = "kinyirakanyar gokart";
      info["cim"] = "Kiskunmajsa, amerre nem jár rendőr";
      info["telefon"] = "+36301234567";
      info["weboldal"] = "kinyirakanyar.gov.hu";

      List<String> vezeteknevek = (new StreamReader("vezeteknevek.txt").ReadToEnd()).Replace("\n", "").Replace("\'", " ").Replace(" ", "").Split(",").ToList();
      List<String> keresztnevek = (new StreamReader("keresztnevek.txt").ReadToEnd()).Replace("\n", "").Replace("\'", " ").Replace(" ", "").Split(",").ToList();

      Random random = new Random();

      for (int i = 0; i < new Random().Next(15, 151); i++)
      {
        DateTime start = DateTime.Now.AddYears(-16); // min 16 eves
        DateTime end = start.AddYears(-80); // max 96 
        int range = (start - end).Days;
        DateTime szulido = end.AddDays(random.Next(range));
        string vez = vezeteknevek[new Random().Next(vezeteknevek.Count)];
        string ker = keresztnevek[new Random().Next(keresztnevek.Count)];
        pilota ujpilota = new pilota
        {
          vezeteknev = vez,
          keresztnev = ker,
          szuletesiido = szulido,
          eletkor = DateTime.Now.Year - szulido.Year,
          nagykoru = DateTime.Now.Year - szulido.Year > 18,
          versenyazonosito = $"GO-{Ekezettelenit(vez)}{Ekezettelenit(ker)}-{szulido.Year}{szulido.Month}{szulido.Day}",
          email = $"{Ekezettelenit(vez)}.{Ekezettelenit(ker)}@gmail.com"
        };
        pilotak.Add(ujpilota);
      }
      pilotak.Add(new pilota());

      for (int i = 0; i < 9; i++)
      {
        List<idopont> nap = new List<idopont>();
        for (int j = 0; j < 11; j++)
        {
          nap.Add(new idopont{
              pilotak = new List<pilota>(),
              start = 8+j,
              nap = i+1,
            }
          );
        }
        idopontok.Add(nap);
      }
      Menu.menu(Menu.opciok, running, "mit szeretne tenni: ");
    }
  }

  public class idopont
  {
    public List<pilota> pilotak = new List<pilota>();
    public int start;
    public int nap;
    public void foglalas(pilota pilot)
    {
      this.pilotak.Add(pilot);
    }
  }
  public class pilota
  {
    public string vezeteknev = "teszt";
    public string keresztnev = "teszt";
    public int eletkor;
    public DateTime szuletesiido = DateTime.Now;
    public bool nagykoru;
    public string versenyazonosito = "teszt";
    public string email = "teszt";

    public string ekezettelen()
    {
      return Gokart.Ekezettelenit(vezeteknev) + " " + Gokart.Ekezettelenit(keresztnev);
    }
  }
}
