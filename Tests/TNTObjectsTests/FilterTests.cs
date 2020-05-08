using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;
using TNT.LSD.Settings;

namespace TNTObjectsTests
{
	[TestClass]
	public class FilterTests
	{
		private CodedParts parts = new CodedParts();
		private List<PartSize> filterSizes = new List<PartSize>()
			{
				PartSize.SIZE_100,
				PartSize.SIZE_150,
				PartSize.SIZE_200
			};
		private List<PartSize> mainSizes = new List<PartSize>()
			{
				PartSize.SIZE_075,
				PartSize.SIZE_100,
				PartSize.SIZE_125,
				PartSize.SIZE_150,
				PartSize.SIZE_200
			};


		[TestMethod]
		public void SetFilterBoxBallValveTest()
		{
			filterSizes.ForEach(size =>
			{
				parts.Clear();
				Filter.SetFilterBoxBallValve(parts, size, "300 MICRON");
				Assert.AreEqual(parts[$"{size}FILTER300M"].Quantity, 1);
				Assert.AreEqual(parts[$"VBJUMBO"].Quantity, 1);
				Assert.AreEqual(parts[$"BV{size}FT"].Quantity, 1);
			});
		}

		[TestMethod]
		public void AddFittings_PVC()
		{
			filterSizes.ForEach(filterSize =>
			{
				mainSizes.ForEach(mainSize =>
				{
					parts.Clear();
					Filter.AddFittings(parts, SystemType.PVC, mainSize, filterSize);

					if (filterSize == PartSize.SIZE_100)
					{
						Assert.AreEqual(parts["AF18017"].Quantity, 1);
						Assert.AreEqual(parts["AF18011"].Quantity, 1);

						if (mainSize == PartSize.SIZE_075)
						{
							Assert.AreEqual(parts["AF18013"].Quantity, 2);
						}
						else if (mainSize == PartSize.SIZE_100)
						{
							Assert.AreEqual(parts["AF18012"].Quantity, 2);
						}
						else if (mainSize == PartSize.SIZE_125)
						{
							Assert.AreEqual(parts["AF18012"].Quantity, 2);
							Assert.AreEqual(parts["FI125SSCOUP"].Quantity, 2);
						}
					}
					else
					{
						parts.Add($"FI{filterSize}STFA", 1);
						parts.Add($"FI{filterSize}TSMA", 1);
					}
				});
			});
		}

		[TestMethod]
		public void AddFittings_POLY()
		{
			filterSizes.ForEach(filterSize =>
			{
				mainSizes.ForEach(mainSize =>
				{
					parts.Clear();
					Filter.AddFittings(parts, SystemType.POLY, mainSize, filterSize);

					if (filterSize == PartSize.SIZE_100)
					{
						Assert.AreEqual(parts["AF18017"].Quantity, 1);
						Assert.AreEqual(parts["AF18011"].Quantity, 1);

						if (mainSize == PartSize.SIZE_075)
						{
							Assert.AreEqual(parts["AF18015"].Quantity, 2);
						}
						else if (mainSize == PartSize.SIZE_100)
						{
							Assert.AreEqual(parts["AF18014"].Quantity, 2);
						}
						else if (mainSize == PartSize.SIZE_125)
						{
							Assert.AreEqual(parts["AF18018"].Quantity, 2);
						}

						var hcCode = mainSize == PartSize.SIZE_125 ? "HC150" : $"HC{mainSize}";
						Assert.AreEqual(parts[hcCode].Quantity, 2);
					}
				});
			});
		}
	}
}
