using System;

namespace Problem.Domino
{
	class Domino
	{
		#region Public properties



		public int Left { get; set; }

		public int Right { get; set; }

		public Domino LeftLink { get; set; }

		public Domino RightLink { get; set; }

		public bool IsTurned { get; set; }

		public bool IsLinked { get { return LeftLink != null || RightLink != null; } }

		public bool IsFullLinked { get { return LeftLink != null && RightLink != null; } }



		#endregion
		#region Public constructors



		public Domino()
		{
			LeftLink = null;
			RightLink = null;
			IsTurned = false;
		}

		public Domino(int nLeft, int nRight)
			: this()
		{
			Left = nLeft;
			Right = nRight;
		}



		#endregion
		#region Public methods



		public void Turn()
		{
			if (Left.Equals(Right)) return;

			if (LeftLink == null && RightLink == null)
			{
				int tmp = Left;
				Left = Right;
				Right = tmp;
				IsTurned = !IsTurned;
			}
			else
				throw new InvalidOperationException();
		}

		public bool TryTurn()
		{
			try
			{
				Turn();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Contain(int Number)
		{
			return Left.Equals(Number) || Right.Equals(Number);
		}

		public void Link(Domino domino, LinkType linkType)
		{
			if (linkType == LinkType.Left && LeftLink == null && !domino.IsLinked)
			{
				if (!Left.Equals(domino.Right))
					domino.Turn();
				LeftLink = domino;
				domino.RightLink = this;
				return;
			}
			if (linkType == LinkType.Right && RightLink == null && !domino.IsLinked)
			{
				if (!Right.Equals(domino.Left))
					domino.Turn();
				RightLink = domino;
				domino.LeftLink = this;
				return;
			}
			throw new InvalidOperationException();
		}

		public override string ToString()
		{
			if (IsTurned)
				return string.Format("[T({0}:{1})]", Left, Right);
			return string.Format("[{0}:{1}]", Left, Right);
		}

		public string Chain(LinkType linkType, bool bUseSelf = true)
		{
			switch (linkType)
			{
				case LinkType.Left:
					if (LeftLink == null)
						return bUseSelf ? ToString() : null;
					if (bUseSelf)
						return string.Join(" - ", new string[] { LeftLink.Chain(LinkType.Left), ToString() });
					else
						return LeftLink.Chain(LinkType.Left);
				case LinkType.Right:
					if (RightLink == null)
						return bUseSelf ? ToString() : null;
					if (bUseSelf)
						return string.Join(" - ", new string[] { ToString(), RightLink.Chain(LinkType.Right) });
					else
						return RightLink.Chain(LinkType.Right);
				default:
					return string.Empty;
			}
		}



		#endregion
	}
}
