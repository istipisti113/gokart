using System.Collections;
using System.Diagnostics.Tracing;

namespace gokart
{
    class Gokart
    {
        static void Main(string[] args)
        {
            Hashtable info = new Hashtable();
            info["nev"] = "kinyirakanyar gokart";
            info["cim"] = "Kiskunmajsa amerre nem jár rendőr";
            info["telefon"] = "+36301234567";
            info["weboldal"] = "kinyirakanyar.gov.hu";

            List<String> vezeteknevek = (new StreamReader("vezeteknevek.txt").ReadToEnd()).Replace("\"", " ").Replace(" ", "").Split(",").ToList();
            List<String> keresztnevek = (new StreamReader("keresztnevek.txt").ReadToEnd()).Replace("\"", " ").Replace(" ", "").Split(",").ToList();

            List<pilota> pilotak = new List<pilota>();

            for (int i = 0; i < new Random().Next(1, 151); i++)
            {
                pilota ujpilota = new pilota
                {
                    vezeteknev = vezeteknevek[new Random().Next(vezeteknevek.Count)],
                    keresztnev = keresztnevek[new Random().Next(keresztnevek.Count)],
                };
            }
            Console.WriteLine("sadfsfdafdsa");
        }
    }

    public class pilota
    {
        public string vezeteknev;
        public string keresztnev;
        public int eletkor;
        public DateTime szuletesiido;
        public bool nagykoru;
        public string versenyazonosito;

        public string Ekezettelenit(string nevv)
        {
            string nev = nevv.ToLower();
            Hashtable betuk = new Hashtable();
            betuk['é'] = 'e';
            betuk['á'] = 'a';
            betuk['ö'] = 'o';
            betuk['ő'] = 'o';
            betuk['ó'] = 'o';
            betuk['ú'] = 'u';
            betuk['ű'] = 'u';
            betuk['ü'] = 'u';
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
        public string ekezettelen()
        {
            return Ekezettelenit(vezeteknev) + " " + Ekezettelenit(keresztnev);
        }
    }
}
