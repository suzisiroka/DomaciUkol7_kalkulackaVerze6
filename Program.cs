using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace Kalkulacka_v6
{                   /*
                    Zadání úkolu:

                    Naše kalkulačka už toho umí hodně. Ale umí pracovat jen s konzolí a v budoucnu ji budem chtít vytvořit grafickou verzi.

                    Jednak v grafickém prostředí konzolové čtení a výpisy nefungují, a pak také náš kód obsahuje dva základní druhy logiky – interakce s konzolí a počítání. V tuhle chvíli je to oboje smícháno dohromady a do velké míry jsou tyto dvě rozdílné věci na sobě velmi závislé.

                    Pointa OOP je mít tyto dvě věci oddělené od sebe, aby bylo možné tu počítací část použít i v grafické aplikaci, kde je načítací/vypisovací logika úplně jiná.

                    Toho lze docílit tak, že se naše Kalkulačka stane třídou:
                    vytvoř třídu Kalkulacka, která bude umět všechno, co se týče počítací logiky. Musí si tedy minimálně pamatovat aktuální výsledek a mít počítací funkce (součet, rozdíl aj.). Určitě by měla být schopna nám říct, jestli podporuje zadaný operátor.
                    veškeré parsování, výpisy do konzole, načítání z konzole, kontroly a opakovací logiku ale necháme v Main kódu. Třída Kalkulacka má opravdu držet jen matematické operace, poslendí výsledek a to je vše.
                    naším cílem v podstatě je, abychom podle této třídy mohli vytvořit více kalkulaček a každá z nich by pracovala s jinou doplňující logikou. Tedy aby šla například použít v jakékoliv z předchozích verzí. Pro jednoduchost ji tedy můžete navrhovat podle kterékoliv verze a neměla by vypadat moc složitě a umět toho příliš moc.
                    Ukázka použití v main kódu:
                    Kalkulacka kalkulacka = new...
                    // nactu, overim cislo
                    kalkulacka.NastavPrvniCislo(cislo);
                    // nactu operator
                    if (!kalkulacka.JePlatnyOperator(operator))
                    // nactu druhé číslo
                    kalkulacka.ProvedVypocet(operator, druheCislo);
                    */
    public class Kalkulacka
    {
        public readonly int PrvniCislo;
        public readonly string Znamenko;
        public readonly int DruheCislo;
        public readonly int Vysledek;

        public Kalkulacka(int prvniCislo, string znamenko, int druheCislo, int vysledek)
        {
            switch (znamenko)
            {
                case "+":
                    vysledek = prvniCislo + druheCislo;
                    break;
                case "-":
                    vysledek = prvniCislo - druheCislo;
                    break;
                case "*":
                    vysledek = prvniCislo * druheCislo;
                    break;
                case "/":
                    vysledek = prvniCislo / druheCislo;
                    break;
                case "^":
                    int mocnina = prvniCislo;
                    for (int i = druheCislo; i > 1; i--)//druhé číslo určí kolikátá je to mocnina
                    {
                        mocnina *= prvniCislo;
                    }
                    vysledek = mocnina;
                    break;
            }
            prvniCislo = vysledek;

            PrvniCislo = prvniCislo;
            Znamenko = znamenko;
            DruheCislo = druheCislo;
            Vysledek = vysledek;

        }

        public void VypisVysledek()
        {
            Console.WriteLine($"výsledek: {PrvniCislo} {Znamenko} {DruheCislo} = {Vysledek}");
        }
      

        internal class Program
        {

            static void Main()
            {
                while (true)
                {
                    Kalkulacka kalkulacka = new Kalkulacka();
                
                    Console.WriteLine("Zadej číslo (pro ukončení zadej X): ");
                    string prvniCisloOdUzivatele = Console.ReadLine();
                    bool prvniCisloSpravne = int.TryParse(prvniCisloOdUzivatele, out int prvniCislo);

                    while (!prvniCisloSpravne)//jedná se o zkrácený zápis zápisu (prvniCisloSpravne == false) - !vykřičník neguje hodnotu
                    {
                        if (prvniCisloOdUzivatele == "X")
                        {
                            return;
                        }
                        Console.WriteLine("Zadal si neplatnou hodnotu, zadej číslo znovu (pro ukončení zadej X):");
                        prvniCisloOdUzivatele = Console.ReadLine();
                        prvniCisloSpravne = int.TryParse(prvniCisloOdUzivatele, out prvniCislo);
                    }

                    while (true)
                    {
                        Console.WriteLine("Zadej znaménko + , - , * , / , nebo ^ (pro ukončení zadej X): ");
                        string znamenko = Console.ReadLine();

                        while (znamenko != "+" && znamenko != "-" && znamenko != "*" && znamenko != "/" && znamenko != "^")
                        {
                            if (znamenko == "X")
                            {
                                return;
                            }
                            Console.WriteLine("Zadal si špatné znaménko, zadej znovu (pro ukončení zadej X):");
                            znamenko = Console.ReadLine();
                        }

                        Console.WriteLine("Zadej číslo (pro ukončení zadej X):");

                        string druheCisloOdUzivatele = Console.ReadLine();
                        bool druheCisloSpravne = int.TryParse(druheCisloOdUzivatele, out int druheCislo);

                        while (!druheCisloSpravne)//jedná se o zkrácený zápis zápisu (druheCisloSpravne == false) - !vykřičník neguje hodnotu
                        {
                            if (druheCisloOdUzivatele == "X")
                            {
                                return;
                            }
                            Console.WriteLine("Zadal si neplatnou hodnotu, zadej číslo znovu (pro ukončení zadej X):");
                            druheCisloOdUzivatele = Console.ReadLine();
                            druheCisloSpravne = int.TryParse(druheCisloOdUzivatele, out druheCislo);

                            kalkulacka.VypisVysledek();
                        }
                    }
                }
            }
        }
    }


    //while (true)
    //{
    //    Console.WriteLine("Zadej číslo (pro ukončení zadej X): ");
    //    string prvniCisloOdUzivatele = Console.ReadLine();
    //    bool prvniCisloSpravne = int.TryParse(prvniCisloOdUzivatele, out int prvniCislo);

    //    while (!prvniCisloSpravne)//jedná se o zkrácený zápis zápisu (prvniCisloSpravne == false) - !vykřičník neguje hodnotu
    //    {
    //        if (prvniCisloOdUzivatele == "X")
    //        {
    //            return;
    //        }
    //        Console.WriteLine("Zadal si neplatnou hodnotu, zadej číslo znovu (pro ukončení zadej X):");
    //        prvniCisloOdUzivatele = Console.ReadLine();
    //        prvniCisloSpravne = int.TryParse(prvniCisloOdUzivatele, out prvniCislo);
    //    }  

    //    while (true)
    //    {
    //        Console.WriteLine("Zadej znaménko + , - , * , / , nebo ^ (pro ukončení zadej X): ");
    //        string znamenko = Console.ReadLine();

    //        while (znamenko != "+" && znamenko != "-" && znamenko != "*" && znamenko != "/" && znamenko != "^")
    //        {
    //            if (znamenko.ToLower() == "X")
    //            {
    //                return;
    //            }
    //            Console.WriteLine("Zadal si špatné znaménko, zadej znovu (pro ukončení zadej X):");
    //            znamenko = Console.ReadLine();
    //        }

    //        Console.WriteLine("Zadej číslo (pro ukončení zadej X):");

    //        string druheCisloOdUzivatele = Console.ReadLine();
    //        bool druheCisloSpravne = int.TryParse(druheCisloOdUzivatele, out int druheCislo);
    //        int vysledek = 0;

    //        while (!druheCisloSpravne)//jedná se o zkrácený zápis zápisu (druheCisloSpravne == false) - !vykřičník neguje hodnotu
    //        {
    //            if (druheCisloOdUzivatele.ToLower() == "X")
    //            {
    //                return;
    //            }
    //            Console.WriteLine("Zadal si neplatnou hodnotu, zadej číslo znovu (pro ukončení zadej X):");
    //            druheCisloOdUzivatele = Console.ReadLine();
    //            druheCisloSpravne = int.TryParse(druheCisloOdUzivatele, out druheCislo);
    //        }

    //        switch (znamenko)
    //        {
    //            case "+":
    //                vysledek = prvniCislo + druheCislo;
    //                break;
    //            case "-":
    //                vysledek = prvniCislo - druheCislo;
    //                break;
    //            case "*":
    //                vysledek = prvniCislo * druheCislo;
    //                break;
    //            case "/":
    //                vysledek = prvniCislo / druheCislo;
    //                break;
    //            case "^":
    //                int mocnina = prvniCislo;
    //                for (int i = druheCislo; i > 1; i--)//druhé číslo určí kolikátá je to mocnina
    //                {
    //                    mocnina *= prvniCislo;
    //                }
    //                vysledek = mocnina;
    //                break;
    //        }

    //        if (vysledek != null)
    //        {
    //            Console.WriteLine($"výsledek: {prvniCislo} {znamenko} {druheCislo} = {vysledek}");
    //        }
    //        prvniCislo = vysledek;
    //    }
    //}

}
