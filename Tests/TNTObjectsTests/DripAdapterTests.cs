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
	public class DripAdapterTests
	{
		[TestMethod]
		public void AddPVCFittings_Single_Pipe()
		{
			var parts = new CodedParts();
			var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			foreach (var pipeSize in sizes)
			{
				parts.Clear();
				DripAdapter.AddPVCFittings(parts, pipeSize, null);

				Assert.AreEqual(4, parts.Count);
				Assert.AreEqual(1, parts[$"FI{pipeSize}X050ST90"].Quantity);
				Assert.AreEqual(1, parts[$"FPSBE050"].Quantity);
				Assert.AreEqual(1, parts[$"FUNNYPIPE"].Quantity);
				Assert.AreEqual(1, parts[$"HRFIG8"].Quantity);
			}
		}

		[TestMethod]
		public void AddPVCFittings_Double_Pipe()
		{
			var parts = new CodedParts();
			var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 };

			foreach (var pipeSize in sizes)
			{
				parts.Clear();
				DripAdapter.AddPVCFittings(parts, pipeSize, pipeSize);

				Assert.AreEqual(4, parts.Count);
				Assert.AreEqual(1, parts[$"FI{pipeSize}X050SSTTEE"].Quantity);
				Assert.AreEqual(1, parts[$"FPSBE050"].Quantity);
				Assert.AreEqual(1, parts[$"FUNNYPIPE"].Quantity);
				Assert.AreEqual(1, parts[$"HRFIG8"].Quantity);
			}
		}

		[TestMethod]
		public void AddPOLYFittings_Single_Pipe()
		{
			var parts = new CodedParts();
			var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			foreach (var pipeSize in sizes)
			{
				var hc = pipeSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : pipeSize;

				parts.Clear();
				DripAdapter.AddPOLYFittings(parts, pipeSize, null);

				Assert.AreEqual(5, parts.Count);
				Assert.AreEqual(1, parts[$"PF{pipeSize}X050BT90"].Quantity);
				Assert.AreEqual(1, parts[$"FPSBE050"].Quantity);
				Assert.AreEqual(1, parts[$"FUNNYPIPE"].Quantity);
				Assert.AreEqual(1, parts[$"HRFIG8"].Quantity);
				Assert.AreEqual(1, parts[$"HC{hc}"].Quantity);
			}
		}

		[TestMethod]
		public void AddPOLYFittings_Double_Pipe()
		{
			var parts = new CodedParts();
			var sizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			foreach (var pipeSize in sizes)
			{
				parts.Clear();
				DripAdapter.AddPOLYFittings(parts, pipeSize, pipeSize);

				Assert.AreEqual(4, parts.Count);
				Assert.AreEqual(1, parts[$"PF{pipeSize}HDSADDLE"].Quantity);
				Assert.AreEqual(1, parts[$"FPSBE050"].Quantity);
				Assert.AreEqual(1, parts[$"FUNNYPIPE"].Quantity);
				Assert.AreEqual(1, parts[$"HRFIG8"].Quantity);
			}
		}
	}
}
