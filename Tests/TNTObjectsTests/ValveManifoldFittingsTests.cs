using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;
using TNT.LSD.Settings;

namespace TNTObjectsTests
{
	[TestClass]
	public class ValveManifoldFittingsTests
	{
		private CodedParts parts;

		[TestInitialize]
		public void TestInitialize()
		{
			parts = new CodedParts();
		}

		[TestMethod]
		public void AddInletParts_Manifold_FIPT_Valve_Same_Size()
		{
			var inletThreads = "FIPT";

			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_100);
			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_150, Manifold.SINGLE_150);
			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_200, Manifold.SINGLE_200);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(1, parts["AF18010"].Quantity);
			Assert.AreEqual(1, parts["AF18010150"].Quantity);
			Assert.AreEqual(1, parts["AF18010200"].Quantity);
		}

		[TestMethod]
		public void AddInletParts_Manifold_FIPT_Valve100_Manifold150()
		{
			var inletThreads = "FIPT";
			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_150);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(1, parts["NI100X4TOE"].Quantity);
			Assert.AreEqual(1, parts["AF18012150"].Quantity);
		}

		[TestMethod]
		public void AddInletParts_Manifold_FIPT_Valve100_Manifold200()
		{
			var inletThreads = "FIPT";
			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_200);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(1, parts["NI100X4TOE"].Quantity);
			Assert.AreEqual(1, parts["AF18012200"].Quantity);
			Assert.AreEqual(1, parts["FI150X100SSRB"].Quantity);
		}

		[TestMethod]
		public void AddInletParts_Manifold_FIPT_Valve150_Manifold200()
		{
			var inletThreads = "FIPT";
			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_150, Manifold.SINGLE_200);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(1, parts["NI150X4TOE"].Quantity);
			Assert.AreEqual(1, parts["AF18012200"].Quantity);
		}

		[TestMethod]
		public void AddInletParts_Manifold_MIPT_Valve100_Manifold100()
		{
			var inletThreads = "MIPT";
			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_100, Manifold.SINGLE_100);

			Assert.AreEqual(1, parts.Count);
			Assert.AreEqual(1, parts["AF18016"].Quantity);
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void AddInletParts_Manifold_MIPT_NotImplemented()
		{
			var inletThreads = "MIPT";
			Valve.AddInletParts(parts, inletThreads, PartSize.SIZE_150, Manifold.SINGLE_100);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_075_100()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["AF18011"].Quantity, 1);
			Assert.AreEqual(parts["AF18013"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_075_150()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18011150"].Quantity, 1);
			Assert.AreEqual(parts["AF18012150"].Quantity, 1);
			Assert.AreEqual(parts["FI100X075SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_075_200()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_200, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18011200"].Quantity, 1);
			Assert.AreEqual(parts["AF18012200"].Quantity, 1);
			Assert.AreEqual(parts["FI150X075SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_100_100()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["AF18011"].Quantity, 1);
			Assert.AreEqual(parts["AF18012"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_100_150()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["AF18011150"].Quantity, 1);
			Assert.AreEqual(parts["AF18012150"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_100_200()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_200, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18011200"].Quantity, 1);
			Assert.AreEqual(parts["AF18012200"].Quantity, 1);
			Assert.AreEqual(parts["FI150X100SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_125_100()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18011"].Quantity, 1);
			Assert.AreEqual(parts["AF18012"].Quantity, 1);
			Assert.AreEqual(parts["FI125SSCOUP"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_125_150()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(4, parts.Count);
			Assert.AreEqual(parts["AF18011150"].Quantity, 1);
			Assert.AreEqual(parts["AF18012150"].Quantity, 1);
			Assert.AreEqual(parts["FI150SSCOUP"].Quantity, 1);
			Assert.AreEqual(parts["FI150X125SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_125_200()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_200, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18011200"].Quantity, 1);
			Assert.AreEqual(parts["AF18012200"].Quantity, 1);
			Assert.AreEqual(parts["FI150X125SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_150_150()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_150, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18011150"].Quantity, 1);
			Assert.AreEqual(parts["AF18012150"].Quantity, 1);
			Assert.AreEqual(parts["FI150SSCOUP"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_150_200()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_150, PartSize.SIZE_200, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["AF18011200"].Quantity, 1);
			Assert.AreEqual(parts["AF18012200"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_FIPT_200_200()
		{
			var outletThreads = "FIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_200, PartSize.SIZE_200, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18011200"].Quantity, 1);
			Assert.AreEqual(parts["AF18012200"].Quantity, 1);
			Assert.AreEqual(parts["FI200SSCOUP"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_MIPT_075_100()
		{
			var outletThreads = "MIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["AF18017"].Quantity, 1);
			Assert.AreEqual(parts["AF18013"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_MIPT_100_100()
		{
			var outletThreads = "MIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_100, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["AF18017"].Quantity, 1);
			Assert.AreEqual(parts["AF18012"].Quantity, 1);
		}

		[TestMethod]
		public void AddOnletParts_PVC_MIPT_125_100()
		{
			var outletThreads = "MIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_125, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["AF18017"].Quantity, 1);
			Assert.AreEqual(parts["AF18012"].Quantity, 1);
			Assert.AreEqual(parts["FI125SSCOUP"].Quantity, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddOnletParts_PVC_MIPT_075_150()
		{
			var outletThreads = "MIPT";
			Valve.AddOutletManifoldParts(parts, outletThreads, PartSize.SIZE_075, PartSize.SIZE_150, SystemType.PVC);
		}
	}
}
