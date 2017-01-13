using System;
using System.Linq;

namespace Problem.Domino
{
	class Program
	{
		static void ShowHelp()
		{
			Console.WriteLine("");
			Console.WriteLine("Incorrect arguments :(");
			Console.WriteLine("Please use Problem.Domino [command] [DominoFile]");
			Console.WriteLine("");
			Console.WriteLine("Commands:");
			Console.WriteLine("");
			Console.WriteLine("-g    generate random domino file");
			Console.WriteLine("-s    solve domino file");
			Console.WriteLine("");
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Problem.Domino v0.0.1");
			Console.WriteLine("---------------------------------------");
			if (args.Length.Equals(2) && (new string[] { "-G", "-S" }).FirstOrDefault(c => args[0].ToUpper().Equals(c)) != null)
			{
				switch (args[0].ToUpper())
				{
					case "-G":
						Console.Write(string.Format("Generate new set in file [{0}] ...", args[1]));
						Generator g = new Generator();
						Console.WriteLine(string.Format("{0}!", g.Generate(args[1]) ? "Success" : "Fail"));
						Console.WriteLine(string.Format("{0}", g.Ticks));
						break;
					case "-S":
						Console.Write(string.Format("Solve set in file [{0}] ...", args[1]));
						Solver s = new Solver();
						Console.WriteLine(string.Format("{0}!", s.Solve(args[1]) ? "Success" : "Fail"));
						Console.WriteLine(string.Format("{0} ms", s.Ticks));
						Console.WriteLine(string.Format("M: {0}", s.Memory));
						break;
					default:
						ShowHelp();
						break;
				}
			}
			else
			{
				ShowHelp();
			}
			Console.WriteLine("---------------------------------------");
			Console.ReadLine();
		}
	}
}
