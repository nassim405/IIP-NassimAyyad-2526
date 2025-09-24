using System;

namespace ConsoleGangsterName;
   class Program
   {
      static void Main(string[] args)
      {
		string header = 
		$@"******************************
		|		GANGSTA NAME BUILDER     |
		******************************";
		Console.WriteLine(header);
		Console.Write("Give the first name of any Disney character: ");
		string naam = Console.ReadLine();
		Console.Write("Give any workbench tool: ");
		string tool = Console.ReadLine();
		Console.Write("What is your last name: ");
		string achternaam = Console.ReadLine();
		string gangstaName = $"{naam} 'the {tool}' {achternaam}";
		Console.Write("\nJe gangsta naam is: ");
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(gangstaName);
		Console.ResetColor();
		
      }
   }



