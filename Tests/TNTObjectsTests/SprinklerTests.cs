using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace TNTObjectsTests
{
	[TestClass]
	public class SprinklerTests
	{
		[TestMethod]
		public void SetPOLYFittings_Single_Pipe()
		{
			var parts = new CodedParts();
			var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
			var outletSizes = new List<PartSize>() { PartSize.SIZE_050, PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			foreach (var pipeSize in pipeSizes)
			{
				var hc = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

				foreach (var outletSize in outletSizes)
				{
					if (outletSize > pipeSize) continue;

					parts.Clear();
					Sprinkler.SetPOLYFittings(parts, 1, pipeSize, outletSize);

					if (pipeSize == outletSize)
					{
						Assert.AreEqual(1, parts[$"PF{pipeSize}BT90"].Quantity);
					}
					else
					{
						Assert.AreEqual(1, parts[$"PF{pipeSize}X{outletSize}BT90"].Quantity);
					}

					Assert.AreEqual(1, parts[$"HC{hc}"].Quantity);
				}
			}
		}

		[TestMethod]
		public void SetPOLYFittings_Double_Pipe()
		{
			var parts = new CodedParts();
			var pipeSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
			var outletSizes = new List<PartSize>() { PartSize.SIZE_050, PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			foreach (var pipeSize in pipeSizes)
			{
				var hc = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

				foreach (var outletSize in outletSizes)
				{
					if (outletSize > pipeSize) continue;

					parts.Clear();
					Sprinkler.SetPOLYFittings(parts, 2, pipeSize, outletSize);

					if (outletSize == PartSize.SIZE_050)
					{
						Assert.AreEqual(1, parts[$"PF{pipeSize}HDSADDLE"].Quantity);
					}
					else
					{
						Assert.AreEqual(1, parts[$"PF{pipeSize}X{outletSize}BBTTEE"].Quantity);
						Assert.AreEqual(2, parts[$"HC{hc}"].Quantity);
					}
				}
			}
		}
	}
}
