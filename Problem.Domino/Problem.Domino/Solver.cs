using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem.Domino
{
	class Solver
	{
		#region Public properties



		public long Ticks { get; set; }

		public long Memory { get; set; }

		public IEnumerable<Domino> Dominos { get; set; }



		#endregion
		#region Private methods



		private void _Load(string sFileName)
		{
			string[] dominos = System.IO.File.ReadAllLines(sFileName);
			List<Domino> tmp = new List<Domino>();
			foreach (string line in dominos.Where(d => !string.IsNullOrWhiteSpace(d)))
			{
				string[] p = line.Split(':');
				if (p.Length.Equals(2))
				{
					tmp.Add(new Domino(int.Parse(p[0]), int.Parse(p[1])));
				}
				else
					throw new InvalidOperationException(string.Format("[{0}] is invalid!", line));
			}
			Dominos = tmp;
		}

		private IEnumerable<Domino> _ForLink(int nNumber, IEnumerable<Domino> dSet)
		{
			foreach (Domino d in dSet.Where(d => d.Contain(nNumber) && !d.IsLinked))
				yield return d;
		}

		private bool _Link(Domino d, IEnumerable<Domino> dominos)
		{
			if (dominos == null) return true;
			if (d.LeftLink == null)
			{
				//Линкуем слева то что можем
				foreach (Domino ld in _ForLink(d.Left, dominos))
				{
					d.Link(ld, LinkType.Left);
					if (_Link(ld, dominos.Where(dd => dd != ld)))
						break;
				}
				if (Dominos.FirstOrDefault(dd => !dd.IsLinked) == null)
					return true;
			}
			if (d.RightLink == null)
			{
				//Линкуем слева то что можем
				foreach (Domino rd in _ForLink(d.Right, dominos))
				{
					d.Link(rd, LinkType.Right);
					if (_Link(rd, dominos.Where(dd => dd != rd)))
						break;
				}
				if (Dominos.FirstOrDefault(dd => !dd.IsLinked) == null)
					return true;
			}
			return false;
		}



		#endregion
		#region Public constructors



		public Solver()
		{
			Dominos = null;
		}



		#endregion
		#region Public methods



		public bool Solve(string sFileName)
		{
			System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
			try
			{
				_Load(sFileName);
				Domino first = Dominos.First();
				watch.Start();
				if (_Link(first, Dominos.Where(d => d != first)))
				{
					watch.Stop();
					Console.Write("YES");
					Console.Write(string.Join("-", new string[] { first.Chain(LinkType.Left, false), first.ToString(), first.Chain(LinkType.Right, false) }));
				}
				else
				{
					watch.Stop();
					Console.Write("NO");
				}
				Ticks = watch.ElapsedMilliseconds;
				Memory = GC.GetTotalMemory(false);
				return true;
			}
			catch (Exception ex)
			{
				if (watch.IsRunning) watch.Stop();
				Console.WriteLine(string.Format("ERROR: {0}", ex.Message));
				return false;
			}
		}



		#endregion
	}
}
