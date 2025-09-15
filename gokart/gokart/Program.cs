using System.Collections;
using System.Diagnostics.Tracing;

Hashtable info = new Hashtable();
info["nev"] = "kinyirakanyar gokart";
info["cim"] = "Kiskunmajsa amerre nem jár rendőr";
info["telefon"] = "+36301234567";
info["weboldal"] = "kinyirakanyar.gov.hu";

List<String> vezeteknevek = (new StreamReader("vezeteknevek.txt").ReadToEnd()).Replace("\"", " ").Replace(" ", "").Split(",").ToList();
List<String> keresztnevek = (new StreamReader("keresztnevek.txt").ReadToEnd()).Replace("\"", " ").Replace(" ", "").Split(",").ToList();

for (int i = 0; i < new Random().Next(1,151); i++) {

}

class pilota {
    string nev;
    int eletkor;
    DateTime szuletesiido;
    bool nagykoru;
    string versenyazonosito;

    string Ekezettelen() {
        foreach (string s in nev)
        {
        }
    }
}
