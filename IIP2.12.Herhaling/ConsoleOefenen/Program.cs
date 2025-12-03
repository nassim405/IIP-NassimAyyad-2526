using System;

class Program
{
   static void Main(string[] args)
   {
      const int MAX_KNIKKERS = 100;
int totaalKnikkers = 0;

while (totaalKnikkers < MAX_KNIKKERS)
{
    Console.Write("Aantal toe te voegen knikkers: ");

    int toevoeging = int.Parse(Console.ReadLine());

    // Controleer of de toevoeging niet te veel is
    if (totaalKnikkers + toevoeging > MAX_KNIKKERS)
    {
        int overschot = MAX_KNIKKERS - totaalKnikkers;
        Console.WriteLine($"Teveel! Er kunnen nog maar {overschot} knikkers bij.");
    }
    else
    {
        totaalKnikkers += toevoeging;
    }
}
Console.WriteLine("De pot is vol");
   }
}