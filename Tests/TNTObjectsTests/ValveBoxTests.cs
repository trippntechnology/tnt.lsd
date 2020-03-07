using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace TNTObjectsTests
{
	[TestClass]
	public class ValveBoxTests
	{
		[TestMethod]
		public void AddPVCMainlineAdapter()
		{
			var parts = new CodedParts();
			var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 };
			var manifolds = new List<Manifold>() { Manifold.DOUBLE_100, Manifold.DOUBLE_150, Manifold.DOUBLE_200 };

			foreach (var manifold in manifolds)
			{
				var manifoldSize = manifold.Size;

				foreach (var pipeSize in pipeSizes)
				{
					if (pipeSize > manifoldSize && pipeSize != PartSize.SIZE_125) break;

					Debug.WriteLine($"{manifoldSize}:{pipeSize}");

					parts.Clear();
					ValveBox.AddPVCMainLineAdapter(parts, pipeSize, manifold);

					if (manifoldSize == PartSize.SIZE_100 && pipeSize == PartSize.SIZE_075)
					{
						Assert.AreEqual(1, parts[$"AF18013"].Quantity);
					}
					else if (manifoldSize == PartSize.SIZE_100)
					{
						Assert.AreEqual(1, parts[$"AF18012"].Quantity);

						if (pipeSize == PartSize.SIZE_125)
						{
							Assert.AreEqual(1, parts[$"FI125SSCOUP"].Quantity);
						}
					}
					else
					{
						Assert.AreEqual(1, parts[$"AF18012{manifoldSize}"].Quantity);

						if (manifoldSize == pipeSize)
						{
							Assert.AreEqual(1, parts[$"FI{manifoldSize}SSCOUP"].Quantity);
						}
					}
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddPOLYMainlineAdapter_NotSupported_Manifold()
		{
			ValveBox.AddPOLYMainLineAdapter(new CodedParts(), PartSize.SIZE_100, Manifold.DOUBLE_150);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddPOLYMainlineAdapter_NotSupported_PipSize()
		{
			ValveBox.AddPOLYMainLineAdapter(new CodedParts(), PartSize.SIZE_150, Manifold.DOUBLE_100);
		}

		[TestMethod]
		public void AddPOLYMainlineAdapter_PVC()
		{
			var parts = new CodedParts();
			var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			var manifold = Manifold.DOUBLE_100;
			var manifoldSize = manifold.Size;

			foreach (var pipeSize in pipeSizes)
			{
				var clamp = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

				parts.Clear();
				ValveBox.AddPOLYMainLineAdapter(parts, pipeSize, manifold);

				if (pipeSize == PartSize.SIZE_075)
				{
					Assert.AreEqual(1, parts[$"AF18015"].Quantity);
				}
				else if (pipeSize == PartSize.SIZE_100)
				{
					Assert.AreEqual(1, parts[$"AF18014"].Quantity);
				}
				else
				{
					Assert.AreEqual(1, parts[$"AF18018"].Quantity);
				}

				Assert.AreEqual(1, parts[$"HC{clamp}"].Quantity);
			}
		}
	}
}
