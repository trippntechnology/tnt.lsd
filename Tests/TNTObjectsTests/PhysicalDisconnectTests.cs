using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;

namespace TNTObjectsTests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class PhysicalDisconnectTests
	{
		[TestMethod]
		public void SetPVCPartQuantities_075_125()
		{
			var parts = new CodedParts();
			var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			sizes.ForEach(pipeSize =>
			{
				parts.Clear();
				PhysicalDisconnect.SetPVCPartQuantities(parts, pipeSize);
				Assert.AreEqual(1, parts["PD2"].Quantity);
				if (pipeSize != PartSize.SIZE_125) Assert.AreEqual(2, parts["FI100TSMA"].Quantity);
				Assert.AreEqual(1, parts["FI100STFA"].Quantity);
				Assert.AreEqual(2, parts["BV100FT"].Quantity);
				Assert.AreEqual(2, parts["PD100CTADAPTER"].Quantity);
				Assert.AreEqual(1, parts["PD100CAMCAP"].Quantity);
				Assert.AreEqual(1, parts["VBJUMBO"].Quantity);

				if (pipeSize == PartSize.SIZE_075) Assert.AreEqual(3, parts["FI100X075SSRB"].Quantity);

				if (pipeSize == PartSize.SIZE_125)
				{
					Assert.AreEqual(3, parts["FI125SSCOUP"].Quantity);
					Assert.AreEqual(3, parts["FI125X100SSRB"].Quantity);
					Assert.AreEqual(2, parts["NI100X4TOE"].Quantity);
				}
			});
		}
		[TestMethod]
		public void SetPVCPartQuantities_150_200()
		{
			var parts = new CodedParts();
			var sizes = new List<PartSize>() { PartSize.SIZE_150, PartSize.SIZE_200 };

			sizes.ForEach(pipeSize =>
			{
				parts.Clear();
				PhysicalDisconnect.SetPVCPartQuantities(parts, pipeSize);
				Assert.AreEqual(2, parts[$"PD{pipeSize}HOSE"].Quantity);
				Assert.AreEqual(1, parts[$"PD{pipeSize}IFCAMLOCK"].Quantity);
				Assert.AreEqual(2, parts[$"PD{pipeSize}MMCAMLOCK"].Quantity);
				Assert.AreEqual(1, parts[$"PD{pipeSize}CAMCAP"].Quantity);
				Assert.AreEqual(2, parts[$"BV{pipeSize}BRONZE"].Quantity);
				Assert.AreEqual(2, parts[$"NI{pipeSize}X4TOE"].Quantity);
				Assert.AreEqual(2, parts[$"FI{pipeSize}SSCOUP"].Quantity);

				if (pipeSize == PartSize.SIZE_150)
				{
					Assert.AreEqual(1, parts[$"FI{pipeSize}TSMA"].Quantity);
					Assert.AreEqual(1, parts["VBJUMBO"].Quantity);
				}
				else
				{
					Assert.AreEqual(1, parts["PF200BTMA"].Quantity);
					Assert.AreEqual(1, parts[$"FI{pipeSize}STFA"].Quantity);
					Assert.AreEqual(1, parts["VBGIANT"].Quantity);
				}
			});
		}

		[TestMethod]
		public void SetPOLYPartQuantities_075()
		{
			var parts = new CodedParts();

			PhysicalDisconnect.SetPOLYPartQuantities(parts, PartSize.SIZE_075);
			Assert.AreEqual(1, parts["PD2"].Quantity);
			Assert.AreEqual(2, parts["BV100FT"].Quantity);
			Assert.AreEqual(2, parts["PD100CTADAPTER"].Quantity);
			Assert.AreEqual(1, parts["PD100CAMCAP"].Quantity);
			Assert.AreEqual(1, parts["VBJUMBO"].Quantity);

			Assert.AreEqual(1, parts["FI100TTCOUP"].Quantity);
			Assert.AreEqual(3, parts["FI100X075TTRB"].Quantity);
			Assert.AreEqual(3, parts["PF075BTMA"].Quantity);
			Assert.AreEqual(3, parts["HC075"].Quantity);
		}

		[TestMethod]
		public void SetPOLYPartQuantities_100()
		{
			var parts = new CodedParts();

			PhysicalDisconnect.SetPOLYPartQuantities(parts, PartSize.SIZE_100);
			Assert.AreEqual(1, parts["PD2"].Quantity);
			Assert.AreEqual(2, parts["BV100FT"].Quantity);
			Assert.AreEqual(2, parts["PD100CTADAPTER"].Quantity);
			Assert.AreEqual(1, parts["PD100CAMCAP"].Quantity);
			Assert.AreEqual(1, parts["VBJUMBO"].Quantity);

			Assert.AreEqual(2, parts["PF100BTMA"].Quantity);
			Assert.AreEqual(1, parts["PF100BTFA"].Quantity);
			Assert.AreEqual(3, parts["HC100"].Quantity);
		}

		[TestMethod]
		public void SetPOLYPartQuantities_125()
		{
			var parts = new CodedParts();

			PhysicalDisconnect.SetPOLYPartQuantities(parts, PartSize.SIZE_125);
			Assert.AreEqual(1, parts["PD2"].Quantity);
			Assert.AreEqual(2, parts["BV100FT"].Quantity);
			Assert.AreEqual(2, parts["PD100CTADAPTER"].Quantity);
			Assert.AreEqual(1, parts["PD100CAMCAP"].Quantity);
			Assert.AreEqual(1, parts["VBJUMBO"].Quantity);

			Assert.AreEqual(2, parts["NI100X2"].Quantity);
			Assert.AreEqual(3, parts["FI125X100TTRB"].Quantity);
			Assert.AreEqual(3, parts["PF125BTFA"].Quantity);
			Assert.AreEqual(3, parts["HC150"].Quantity);
		}
	}
}
