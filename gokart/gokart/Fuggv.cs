namespace gokart
{
  public static class Fuggv{
    public static void versenyazonositok(){
      Console.WriteLine(String.Join("\n", Gokart.pilotak.Select(x => $"{x.vezeteknev} {x.keresztnev}({x.versenyazonosito}) {x.email}")));
      Console.WriteLine("[enter]");
      Console.ReadLine();
    }

    public static void berles(string azon)
    {
      string azonosito;
      if (azon==""){
        azonosito = getid();
      } else {
        azonosito = azon;
      }
      //Console.WriteLine("egy vagy ketto futam?");
      Menu.menu(new Dictionary<string, Action<int>>
          {
            ["egy"] = (valasztas) => {},
            ["ketto"] = (valasztas) => {},
          }
          , false, "egy vagy ket futam", true);
      var futamok = filterfutamok("szabad", azonosito);
      var kivalaszott = futamkivalasztas(futamok, azonosito);
      futamok[kivalaszott.Item1][kivalaszott.Item2].foglalas(Gokart.pilotak.Find(x=> x.versenyazonosito == azonosito));
      Console.Clear();
      Console.WriteLine($"lefoglalva a {Gokart.idopontok[kivalaszott.Item1][0].nap} napra");
      Console.WriteLine("[enter]");
      Console.ReadLine();
    }

    public static void foglalasok(){
      bool van = false;
      Gokart.idopontok.ForEach(x=>{
        //Console.WriteLine(string.Join(", ", x.FindAll(y=>y.pilotak.Count()>0).Select(y=> y.start)));
        x.ForEach(y=> {
          if (y.pilotak.Count()>0){
            van=true;
            Console.WriteLine($"{string.Join(", ", y.pilotak.Select(z=>z.versenyazonosito))} {y.nap}, {y.start}");
          }
        });
      });
      if (!van){Console.WriteLine("nincs foglalas");}
      Console.ReadLine();
    }

    public static void kiiratas()
    {
      var fejlec = Enumerable.Range(8, 11).Select(x=>$"{x}-{x+1}".PadLeft(5, ' '));
      var a = "  |"+String.Join("|", fejlec);
      //a sorok, sorok es fejlec koze elvalaszto
      string elvalaszto = $"--+{String.Join('+',fejlec.Select(x=> new string('-', x.Length)))}"; 
      Console.WriteLine(a);
      Console.WriteLine(elvalaszto);
      List<string> sorok = new List<string>();
      foreach (List<idopont> nap in Gokart.idopontok){
        sorok.Add($"{nap[0].nap.ToString("D2")}|"+ //melyik nap
        $"{string.Join('|', nap.Select(x=> { //szinkivalasztas
          string color = "";
          if (x.pilotak.Count <8){color="[33m";} //sargaval irja ki, ANSI escape code
          else if (x.pilotak.Count < 20){color="[32m";}//zold
          else {color="[31m";}//piros
        return $"\u001b{color}{x.pilotak.Count.ToString("D5")}\u001b[0m"; 
        }))}\n");
      }
      Console.WriteLine(string.Join(elvalaszto+"\n", sorok)+ "\n[enter]");
      Console.ReadLine();
    }

    public static void modositas()
    {
      Console.WriteLine("foglalas modositas");
      string azonosito = getid();
      torles(azonosito);
      berles(azonosito);
    }

    static List<List<idopont>> filterfutamok(string alapjan, string azonosito="", int mennyi=1){
      if (alapjan=="pilota"&&azonosito==""){azonosito = getid();}
      var returning = new List<List<idopont>>();
      //Gokart.idopontok.ForEach(x=>{
      for (int i = 0; i<Gokart.idopontok.Count-mennyi; i++){
        List<idopont> x = Gokart.idopontok[i];
        List<idopont> nap = new List<idopont>();
        x.ForEach(y=>{
          switch (alapjan){
            case "szabad":
              if (y.pilotak.Count()<20 && !y.pilotak.Exists(z=>z.versenyazonosito==azonosito)){
                nap.Add(y);
              }
              break;
            case "pilota":
              if (y.pilotak.Exists(z=>z.versenyazonosito==azonosito)){
                nap.Add(y);
              }
              break;
            default:
              Console.WriteLine("nem jo parameter!");
              break;
          }
        });
        if (nap.Count>0){returning.Add(nap);}
      };
      return returning;
    }
    
    static void torles(string azon){
      string azonosito;
      if (azon ==""){
        azonosito = getid();
      } else {azonosito=azon;}
      var futamok = filterfutamok("pilota", azonosito);
      var kiv = futamkivalasztas(futamok);
      futamok[kiv.Item1][kiv.Item2].pilotak.RemoveAll(x=>x.versenyazonosito==azonosito);
    }

    static string getid(){
      Console.Write("versenyazonosito: ");
      string azonosito = Console.ReadLine() ?? "";
      if (!Gokart.pilotak.Exists(x => x.versenyazonosito == azonosito)){
        Console.WriteLine("nincs ilyen versenyazonosito (nyomjon meg valamit a visszalepeshez)");
        Console.ReadKey();
        return "";
      }
      return azonosito;
    }
    
    static (int, int) futamkivalasztas(List<List<idopont>> futamok, string azonosito=""){
      (int, int) returning = (-1,-1);
      //List<List<idopont>> futamok = filterfutamok("szabad");
      Dictionary<string, Action<int>> futamopciok = new Dictionary<string, Action<int>>();
      //menu opciok a nap kivalasztasahoz
      for (int i = 0;i<futamok.Count(); i++){
        futamopciok[futamok[i][0].nap.ToString()] = (kivalasztottNap) => {
          //menu opciok a napon beluli futam kivalasztasahoz
          Dictionary<string, Action<int>> idopont_opciok = new Dictionary<string, Action<int>>();
          //a napon beluli futamok kozul lehet valasztani
          foreach (idopont j in futamok[kivalasztottNap].FindAll(x=>!x.pilotak.Select(y=>y.versenyazonosito).Contains(azonosito))){
            idopont_opciok[$"futam {j.start}"] = (kivalasztottFutam) => {
              returning = (kivalasztottNap, kivalasztottFutam);
              return;
            };
          }
          Menu.menu(idopont_opciok, false, "valasszon egy futamot ", false);
          return;
        };
      }
      Menu.menu(futamopciok, false, "valasszon egy napot ",false);
      return returning;
    }
  }
}
