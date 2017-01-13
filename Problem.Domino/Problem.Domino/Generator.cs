using System;
using System.Text;

namespace Problem.Domino
{
	class Generator
	{
		#region Public properties



		public long Ticks { get; set; }



		#endregion
		#region Public constructors



		public Generator()
		{

		}



		#endregion
		#region Public methods



		public bool Generate(string sFileName)
		{
			try
			{
				long dStart = DateTime.Now.Ticks;
				Random rnd = new Random();
				int nDominoCount = rnd.Next(Properties.Settings.Default.MaxDomino);
				System.Text.StringBuilder sb = new StringBuilder();
				for (int i = 0; i < nDominoCount; i++)
				{
					int nLeft = rnd.Next(6) + 1;
					int nRight = rnd.Next(6) + 1;
					sb.AppendLine(string.Format("{0}:{1}", nLeft, nRight));
				}
				Ticks = DateTime.Now.Ticks - dStart;
				System.IO.File.WriteAllText(sFileName, sb.ToString());
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("ERROR: {0}", ex.Message));
				return false;
			}
		}



		#endregion
	}
}
