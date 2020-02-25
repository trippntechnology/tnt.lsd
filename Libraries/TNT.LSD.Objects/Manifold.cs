using System.Collections.Generic;

namespace TNT.LSD.Objects
{
	public sealed class Manifold
	{
		public int Count { get; private set; }
		public PartSize Size { get; private set; }

		public Manifold(int count, PartSize size)
		{
			this.Count = count;
			this.Size = size;
		}

		public static Manifold SINGLE_100 = new Manifold(1, PartSize.SIZE_100);
		public static Manifold SINGLE_150 = new Manifold(1, PartSize.SIZE_150);
		public static Manifold SINGLE_200 = new Manifold(1, PartSize.SIZE_200);
		public static Manifold DOUBLE_100 = new Manifold(2, PartSize.SIZE_100);
		public static Manifold DOUBLE_150 = new Manifold(2, PartSize.SIZE_150);
		public static Manifold DOUBLE_200 = new Manifold(2, PartSize.SIZE_200);
		public static Manifold TRIPLE_100 = new Manifold(3, PartSize.SIZE_100);
		public static Manifold QUAD_100 = new Manifold(4, PartSize.SIZE_100);

		private static List<Manifold> Manifolds = new List<Manifold>() { SINGLE_100, SINGLE_150, SINGLE_200, DOUBLE_100, DOUBLE_150, DOUBLE_200, TRIPLE_100, QUAD_100 };

		public static Manifold GetManifold(int count, PartSize size) => Manifolds.Find(manifold => manifold.Count == count && manifold.Size == size);
	}
}
