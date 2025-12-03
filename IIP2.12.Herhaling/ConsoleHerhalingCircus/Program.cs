using System;
using System.Collections.Generic;

namespace ConsoleHerhalingCircus
{
    class Program
    {
        // Constanten voor de ticketprijzen
        private const decimal PRIJS_VOLWASSENE = 19.90m;
        private const decimal PRIJS_KIND = 12.50m;

        // Opslag voor het winkelmandje: TicketType, Aantal
        private static Dictionary<string, int> winkelmandje = new Dictionary<string, int>()
        {
            { "Volwassene", 0 },
            { "Kind", 0 }
        };
        
        // Gebruikt voor de willekeurige korting bij optie Q
        private static Random randomGenerator = new Random(); 

        static void Main(string[] args)
        {
            char keuze = ' ';

            // Herhaal tot 'q' ingetikt wordt
            while (keuze != 'q')
            {
                // Wis telkens eerst de console
                Console.Clear();
                
                ToonHoofdMenu();

                // Lees de invoer
                Console.Write("Je keuze: ");
                string invoer = Console.ReadLine()?.ToLower();
                
                if (!string.IsNullOrEmpty(invoer) && invoer.Length == 1)
                {
                    keuze = invoer[0];

                    switch (keuze)
                    {
                        case 'a':
                            TicketsToevoegen();
                            break;
                        case 'b':
                            WinkelmandjeTonen();
                            break;
                        case 'c':
                            WinkelmandjeWissen();
                            break;
                        case 'q':
                            BestellingAfronden();
                            // De 'while' lus stopt nu door 'keuze = q', dus het programma sluit.
                            break;
                            
                        // Afhandeling voor onbekende keuze
                        default:
                            Console.WriteLine("\nOnbekende keuze."); 
                            Console.WriteLine("...druk een toets om verder te gaan.");
                            Console.ReadKey(true);
                            break;
                    }
                }
            }
        }
        
        // --- FUNCTIES VOOR MENU-OPTIES ---

        private static void ToonHoofdMenu()
        {
            Console.WriteLine("Welkom bij de ticketshop voor \"Circus Stromboli\"");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("(a) Tickets toevoegen");
            Console.WriteLine("(b) Winkelmandje tonen");
            Console.WriteLine("(c) Winkelmandje wissen");
            Console.WriteLine("(q) Bestelling afronden");
        }

        private static void TicketsToevoegen()
        {
            int toegevoegdVolwassenen = 0;
            int toegevoegdKinderen = 0;

            Console.Clear();
            Console.WriteLine("--- Tickets toevoegen ---");
            
            // Vraag om aantal volwassenen tickets
            Console.Write("Volwassenen: ");
            if (int.TryParse(Console.ReadLine(), out int volwassenAantal) && volwassenAantal >= 0)
            {
                winkelmandje["Volwassene"] += volwassenAantal;
                toegevoegdVolwassenen = volwassenAantal; 
            }

            // Vraag om aantal kindertickets
            Console.Write("Kinderen: ");
            if (int.TryParse(Console.ReadLine(), out int kindAantal) && kindAantal >= 0)
            {
                winkelmandje["Kind"] += kindAantal;
                toegevoegdKinderen = kindAantal; 
            }
            
            Console.WriteLine($"\nEr zijn tickets voor {toegevoegdVolwassenen} volwassenen en {toegevoegdKinderen} kinderen toegevoegd aan je winkelmandje.");

            Console.WriteLine("\n...druk een toets om verder te gaan.");
            Console.ReadKey(true);
        }

        private static void WinkelmandjeTonen()
        {
            Console.Clear();
            Console.WriteLine("--- Jouw Winkelmandje ---");
            
            int aantalVolwassenen = winkelmandje["Volwassene"];
            int aantalKinderen = winkelmandje["Kind"];
            int totaalAantal = aantalVolwassenen + aantalKinderen;

            if (totaalAantal == 0)
            {
                Console.WriteLine("\nJe winkelmandje is leeg.");
            }
            else
            {
                decimal totaalPrijs = (aantalVolwassenen * PRIJS_VOLWASSENE) + (aantalKinderen * PRIJS_KIND);
                
                Console.WriteLine($"Volwassenen: {aantalVolwassenen}");
                Console.WriteLine($"Kinderen: {aantalKinderen}");
                
                Console.WriteLine("-------------------------");
                Console.WriteLine($"Totaalprijs (excl. korting): {totaalPrijs:C}");
            }

            Console.WriteLine("\n...druk een toets om verder te gaan.");
            Console.ReadKey(true);
        }

        private static void WinkelmandjeWissen()
        {
            Console.Clear();
            winkelmandje["Volwassene"] = 0;
            winkelmandje["Kind"] = 0;
            
            Console.WriteLine("Winkelmandje is gewist.");
            
            Console.WriteLine("\n...druk een toets om verder te gaan.");
            Console.ReadKey(true);
        }

        private static void BestellingAfronden()
        {
            Console.Clear();
            
            int aantalVolwassenen = winkelmandje["Volwassene"];
            int aantalKinderen = winkelmandje["Kind"];
            decimal totaalPrijsInitieel = (aantalVolwassenen * PRIJS_VOLWASSENE) + (aantalKinderen * PRIJS_KIND);
            
            if (aantalVolwassenen + aantalKinderen == 0)
            {
                Console.WriteLine("Je winkelmandje is leeg. Geen bestelling om af te ronden.");
                Console.WriteLine("\n...druk een toets om verder te gaan.");
                return; 
            }

            Console.WriteLine($"Totaalprijs: {totaalPrijsInitieel:C}");

            Console.Write("Ben je jarig vandaag (j/n)? ");
            string invoerJarig = Console.ReadLine()?.ToLower();
            
            bool isJarig = (invoerJarig == "j");
            decimal teBetalenBedrag = totaalPrijsInitieel;

            if (isJarig)
            {
                // Willekeurige korting tussen 5% (0.05) en 10% (0.10)
                double kortingPercentageDouble = randomGenerator.NextDouble() * 0.05 + 0.05;
                
                decimal kortingPercentage = Math.Round((decimal)kortingPercentageDouble, 4);
                int getoondPercentage = (int)Math.Round(kortingPercentage * 100); 

                decimal kortingsBedrag = totaalPrijsInitieel * kortingPercentage;
                teBetalenBedrag = totaalPrijsInitieel - kortingsBedrag;

                Console.WriteLine($"Gefeliciteerd! Je krijgt {getoondPercentage}% korting op je totaalprijs.");
            }

            teBetalenBedrag = Math.Round(teBetalenBedrag, 2);

            Console.WriteLine($"Te betalen bedrag: {teBetalenBedrag:C}");

            // Bonuspunten (per €10 één bonuspunt)
            int bonuspunten = (int)Math.Floor(teBetalenBedrag / 10m);
            
            Console.WriteLine($"Je hebt {bonuspunten} bonuspunten verzameld.");
            
            Console.WriteLine("Tot ziens!");
        }
    }
}