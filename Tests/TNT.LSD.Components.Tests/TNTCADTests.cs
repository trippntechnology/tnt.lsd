using LSDComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Components.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class TNTCADTests
	{
		private const string PI075 = "PI075";
		private const string PI100 = "PI100";
		private const string POLY075 = "POLY075";
		private const string POLY100 = "POLY100";

		[TestMethod]
		public void AddDrains_NoPipe()
		{
			var sut = new CodedParts();

			TNTCAD.AddDrains(sut, 7, SystemType.PVC);
			Assert.AreEqual(0, sut.Count);
		}

		[TestMethod]
		public void AddDrains_34Pipe()
		{
			var sut = new CodedParts();
			sut.Add(PI075, 100);
			sut.Add(POLY075, 0);
			TNTCAD.AddDrains(sut, 6, SystemType.PVC);
			Assert.AreEqual(4, sut.Count);
			Assert.AreEqual(6, sut["FI075X050SSTTEE"].Quantity);
			Assert.AreEqual(6, sut["KD22"].Quantity);
		}

		[TestMethod]
		public void AddDrains_34and1Pipe()
		{
			var sut = new CodedParts();
			sut.Add(PI075, 100);
			sut.Add(PI100, 100);
			TNTCAD.AddDrains(sut, 6, SystemType.PVC);
			Assert.AreEqual(5, sut.Count);
			Assert.AreEqual(3, sut["FI075X050SSTTEE"].Quantity);
			Assert.AreEqual(3, sut["FI100X050SSTTEE"].Quantity);
			Assert.AreEqual(6, sut["KD22"].Quantity);
		}

		[TestMethod]
		public void AddDrains_34Poly()
		{
			var sut = new CodedParts();
			sut.Add(PI075, 0);
			sut.Add(POLY075, 100);
			TNTCAD.AddDrains(sut, 6, SystemType.POLY);
			Assert.AreEqual(5, sut.Count);
			Assert.AreEqual(6, sut["PF075X050BBTTEE"].Quantity);
			Assert.AreEqual(12, sut["HC075"].Quantity);
			Assert.AreEqual(6, sut["KD22"].Quantity);
		}

		[TestMethod]
		public void AddDrains_34and1Poly()
		{
			var sut = new CodedParts();
			sut.Add(POLY075, 100);
			sut.Add(POLY100, 100);
			TNTCAD.AddDrains(sut, 6, SystemType.POLY);
			Assert.AreEqual(7, sut.Count);
			Assert.AreEqual(3, sut["PF075X050BBTTEE"].Quantity);
			Assert.AreEqual(3, sut["PF100X050BBTTEE"].Quantity);
			Assert.AreEqual(6, sut["HC075"].Quantity);
			Assert.AreEqual(6, sut["HC100"].Quantity);
			Assert.AreEqual(6, sut["KD22"].Quantity);
		}
	}
}
