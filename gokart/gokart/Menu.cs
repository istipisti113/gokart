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
        static Dictionary<string, Action> opciok = new Dictionary<string, Action> {
            ["berles"] = () => { berles(); },
            ["kiiratas"] = () => { kiiratas(); },
            ["modositas"] = () => { modositas(); },
            ["helytelen"] = () => { Console.WriteLine("nincs ilyen opció vagy helytelen bemenet"); }, };
        public static void menu()
        {
            while (true) {
                Console.WriteLine("mit szeretne tenni: ");
                foreach (var item in opciok.Keys)
                {
                    Console.WriteLine(item);
                }
                string input = Console.ReadLine() ?? "helytelen";
                if (opciok.ContainsKey(input))
                {
                    opciok[input]();
                    break;
                } else { opciok["helytelen"](); }
            }
        }

        public static void berles()
        {

        }
        public static void kiiratas()
        {

        }
        public static void modositas()
        {

        }
    }
}
