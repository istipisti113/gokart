using System.Collections;
using System.Diagnostics.Tracing;

namespace gokart
{
    class Gokart
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
            Random random = new Random();

            for (int i = 0; i < new Random().Next(1, 151); i++)
            {
                DateTime start = DateTime.Now.AddDays(-16*12*30); // min 16 eves
                DateTime end = start.AddYears(80); // max 96 
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
                    versenyazonosito = $"GO-{}"
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

        public string ekezettelen()
        {
            return Ekezettelenit(vezeteknev) + " " + Ekezettelenit(keresztnev);
        }
    }
}
