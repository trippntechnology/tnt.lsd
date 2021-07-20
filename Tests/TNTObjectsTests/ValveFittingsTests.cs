using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Objects;
using TNT.LSD.Settings;

namespace TNTObjectsTests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class ValveFittingsTests
	{
		private CodedParts parts;

		[TestInitialize]
		public void TestInitialize()
		{
			parts = new CodedParts();
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_075_100()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 1, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["FI100X075TTRB"].Quantity, 1);
			Assert.AreEqual(parts["NI075X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI075SS90"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 2, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["FI100X075TTRB"].Quantity, 1);
			Assert.AreEqual(parts["NI075X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI075SSSTEE"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_075_150()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 1, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["FI150X075TTRB"].Quantity, 1);
			Assert.AreEqual(parts["NI075X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI075SS90"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_100_100()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 1, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI100SS90"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 2, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI100SSSTEE"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_125_100()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_125, 1, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI125SS90"].Quantity, 1);
			Assert.AreEqual(parts["FI125X100SSRB"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_125, 2, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI125SSSTEE"].Quantity, 1);
			Assert.AreEqual(parts["FI125X100SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_150_100()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 1, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI150SS90"].Quantity, 1);
			Assert.AreEqual(parts["FI150X100SSRB"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 2, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI150SSSTEE"].Quantity, 1);
			Assert.AreEqual(parts["FI150X100SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_150_150()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 1, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["NI150X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI150SS90"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_150, 2, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["NI150X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI150SSSTEE"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_200_150()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 1, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["NI150X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI200SS90"].Quantity, 1);
			Assert.AreEqual(parts["FI200X150SSRB"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 2, PartSize.SIZE_150, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["NI150X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI200SSSTEE"].Quantity, 1);
			Assert.AreEqual(parts["FI200X150SSRB"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_PVC_FIPT_200_200()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 1, PartSize.SIZE_200, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["NI200X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI200SS90"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_200, 2, PartSize.SIZE_200, SystemType.PVC);

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(parts["NI200X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI200SSSTEE"].Quantity, 1);
		}

		[TestMethod]
		public void AddInletParts_NULL_MAINSIZE()
		{
			var threads = "FIPT";
			Valve.AddInletParts(parts, threads, null, 1, PartSize.SIZE_200, SystemType.PVC);
			Assert.AreEqual(0, parts.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddInletParts_PVC_MIPT_NOT_100_VALVE()
		{
			var threads = "MIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 1, PartSize.SIZE_150, SystemType.PVC);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddInletParts_PVC_MIPT_075_MAIN()
		{
			var threads = "MIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 1, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["FI075X100STFA"].Quantity, 1);
			Assert.AreEqual(parts["NI075X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI075SS90"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_075, 2, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["FI075X100STFA"].Quantity, 1);
			Assert.AreEqual(parts["NI075X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI075SSSTEE"].Quantity, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddInletParts_PVC_MIPT_100_MAIN()
		{
			var threads = "MIPT";
			Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 1, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["FI100STFA"].Quantity, 1);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI100SS90"].Quantity, 1);

			parts.Clear();
			Valve.AddInletParts(parts, threads, PartSize.SIZE_100, 2, PartSize.SIZE_100, SystemType.PVC);

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual(parts["FI100STFA"].Quantity, 1);
			Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
			Assert.AreEqual(parts["FI100SSSTEE"].Quantity, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddInletParts_PVC_MIPT_GT_100_MAIN()
		{
			var threads = "MIPT";
			var mainSizes = new List<PartSize>() { PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 };

			mainSizes.ForEach(ps =>
			{
				parts.Clear();
				Valve.AddInletParts(parts, threads, ps, 1, PartSize.SIZE_100, SystemType.PVC);

				Assert.AreEqual(4, parts.Count);
				Assert.AreEqual(parts["FI100STFA"].Quantity, 1);
				Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
				Assert.AreEqual(parts[$"FI{ps}SS90"].Quantity, 1);
				Assert.AreEqual(parts[$"FI{ps}X{100}SSRB"].Quantity, 1);

				parts.Clear();
				Valve.AddInletParts(parts, threads, ps, 2, PartSize.SIZE_100, SystemType.PVC);

				Assert.AreEqual(4, parts.Count);
				Assert.AreEqual(parts["FI100STFA"].Quantity, 1);
				Assert.AreEqual(parts["NI100X4TOE"].Quantity, 1);
				Assert.AreEqual(parts[$"FI{ps}SSSTEE"].Quantity, 1);
				Assert.AreEqual(parts[$"FI{ps}X{100}SSRB"].Quantity, 1);
			});
		}

		[TestMethod]
		public void AddOutletParts_PVC_FIPT()
		{
			var sizes = new Dictionary<PartSize, List<PartSize>>() {
				{ PartSize.SIZE_100, new List<PartSize>(){PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 } },
				{ PartSize.SIZE_150, new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150 } },
				{ PartSize.SIZE_200, new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125, PartSize.SIZE_150, PartSize.SIZE_200 }},
			};
			var threads = "FIPT";

			foreach (var pair in sizes)
			{
				var valveSize = pair.Key;
				pair.Value.ForEach(pipeSize =>
				{
					parts.Clear();
					Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.PVC);

					Assert.AreEqual(valveSize == pipeSize ? 2 : 3, parts.Count);
					Assert.AreEqual(parts[$"NI{valveSize}X4TOE"].Quantity, 1);

					if (valveSize > pipeSize)
					{
						Assert.AreEqual(parts[$"FI{valveSize}SSCOUP"].Quantity, 1);
						Assert.AreEqual(parts[$"FI{valveSize}X{pipeSize}SSRB"].Quantity, 1);
					}
					else if (valveSize < pipeSize)
					{
						Assert.AreEqual(parts[$"FI{pipeSize}SSCOUP"].Quantity, 1);
						Assert.AreEqual(parts[$"FI{pipeSize}X{valveSize}SSRB"].Quantity, 1);
					}
					else
					{
						Assert.AreEqual(parts[$"FI{valveSize}SSCOUP"].Quantity, 1);
					}
				});
			}
		}

		[TestMethod]
		public void AddOutletParts_PVC_MIPT()
		{
			var sizes = new Dictionary<PartSize, List<PartSize>>() {
				{ PartSize.SIZE_100, new List<PartSize>(){PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 } },
			};
			var threads = "MIPT";

			foreach (var pair in sizes)
			{
				var valveSize = pair.Key;
				pair.Value.ForEach(pipeSize =>
				{
					parts.Clear();
					Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.PVC);

					Assert.AreEqual(valveSize == pipeSize ? 1 : 2, parts.Count);

					if (valveSize > pipeSize)
					{
						Assert.AreEqual(parts[$"FI{valveSize}STFA"].Quantity, 1);
						Assert.AreEqual(parts[$"FI{valveSize}X{pipeSize}SSRB"].Quantity, 1);
					}
					else if (valveSize < pipeSize)
					{
						Assert.AreEqual(parts[$"FI{pipeSize}SSCOUP"].Quantity, 1);
						Assert.AreEqual(parts[$"FI{pipeSize}X{valveSize}STRB"].Quantity, 1);
					}
					else
					{
						Assert.AreEqual(parts[$"FI{valveSize}STFA"].Quantity, 1);
					}
				});
			}
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddInletParts_POLY_Invalid_Valve_Size()
		{
			Valve.AddInletParts(parts, "FIPT", PartSize.SIZE_100, 1, PartSize.SIZE_150, SystemType.POLY);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddInletParts_POLY_Invalid_Pipe_Size()
		{
			Valve.AddInletParts(parts, "FIPT", PartSize.SIZE_150, 1, PartSize.SIZE_100, SystemType.POLY);
		}

		[TestMethod]
		public void AddInletParts_POLY_FIPT_100()
		{
			var threads = "FIPT";
			var valveSize = PartSize.SIZE_100;
			var mainSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };

			mainSizes.ForEach(mainSize =>
			{
				var smallestSize = valveSize < mainSize ? valveSize : mainSize;
				var largestSize = valveSize > mainSize ? valveSize : mainSize;
				var hcCode = mainSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : mainSize;

				parts.Clear();
				Valve.AddInletParts(parts, threads, mainSize, 1, PartSize.SIZE_100, SystemType.POLY);

				Assert.AreEqual(smallestSize != largestSize ? 4 : 3, parts.Count);
				Assert.AreEqual(parts[$"NI{smallestSize}X4"].Quantity, 1);
				Assert.AreEqual(parts[$"PF{mainSize}BT90"].Quantity, 1);
				if (smallestSize != largestSize)
				{
					Assert.AreEqual(parts[$"FI{largestSize}X{smallestSize}TTRB"].Quantity, 1);
				}
				Assert.AreEqual(parts[$"HC{hcCode}"].Quantity, 1);

				parts.Clear();
				Valve.AddInletParts(parts, threads, mainSize, 2, PartSize.SIZE_100, SystemType.POLY);

				Assert.AreEqual(smallestSize != largestSize ? 4 : 3, parts.Count);
				Assert.AreEqual(parts[$"NI{smallestSize}X4"].Quantity, 1);
				Assert.AreEqual(parts[$"PF{mainSize}BBTTEE"].Quantity, 1);
				if (smallestSize != largestSize)
				{
					Assert.AreEqual(parts[$"FI{largestSize}X{smallestSize}TTRB"].Quantity, 1);
				}
				Assert.AreEqual(parts[$"HC{hcCode}"].Quantity, 2);
			});
		}

		[TestMethod]
		public void AddOutletParts_POLY_FIPT_100()
		{
			var threads = "FIPT";
			var valveSize = PartSize.SIZE_100;
			var mainSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
			var partCounts = new List<int>() { 4, 2, 4 };

			mainSizes.ForEach(pipeSize =>
			{
				var smallestSize = valveSize < pipeSize ? valveSize : pipeSize;
				var largestSize = valveSize > pipeSize ? valveSize : pipeSize;

				parts.Clear();
				Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.POLY);

				Assert.AreEqual(partCounts[mainSizes.IndexOf(pipeSize)], parts.Count);
				Assert.AreEqual(parts[$"PF{valveSize}BTMA"].Quantity, 1);
				if (smallestSize != largestSize)
				{
					Assert.AreEqual(parts[$"PF{largestSize}X{smallestSize}BBRB"].Quantity, 1);
				}

				if (pipeSize == PartSize.SIZE_075)
				{
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, 2);
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_075).Code}"].Quantity, 1);
				}
				else if (pipeSize == PartSize.SIZE_125)
				{
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, 2);
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_125).Code}"].Quantity, 1);
				}
				else
				{
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(largestSize).Code}"].Quantity, 1);
				}
			});
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddOutletParts_POLY_Unsupported_Valve()
		{
			Valve.AddOutletParts(parts, "FIPT", PartSize.SIZE_150, PartSize.SIZE_100, SystemType.POLY);
		}


		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void AddOutletParts_POLY_Unsupported_Pipe_Size()
		{
			Valve.AddOutletParts(parts, "FIPT", PartSize.SIZE_100, PartSize.SIZE_150, SystemType.POLY);
		}


		[TestMethod]
		public void AddOutletParts_POLY_MIPT_100()
		{
			var threads = "MIPT";
			var valveSize = PartSize.SIZE_100;
			var mainSizes = new List<PartSize>() { PartSize.SIZE_075, PartSize.SIZE_100, PartSize.SIZE_125 };
			var partCounts = new List<int>() { 4, 2, 4 };

			mainSizes.ForEach(pipeSize =>
			{
				var smallestSize = valveSize < pipeSize ? valveSize : pipeSize;
				var largestSize = valveSize > pipeSize ? valveSize : pipeSize;

				parts.Clear();
				Valve.AddOutletParts(parts, threads, valveSize, pipeSize, SystemType.POLY);

				Assert.AreEqual(partCounts[mainSizes.IndexOf(pipeSize)], parts.Count);
				Assert.AreEqual(parts[$"PF{valveSize}BTFA"].Quantity, 1);
				if (smallestSize != largestSize)
				{
					Assert.AreEqual(parts[$"PF{largestSize}X{smallestSize}BBRB"].Quantity, 1);
				}

				if (pipeSize == PartSize.SIZE_075)
				{
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, 2);
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_075).Code}"].Quantity, 1);
				}
				else if (pipeSize == PartSize.SIZE_125)
				{
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(valveSize).Code}"].Quantity, 2);
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(PartSize.SIZE_125).Code}"].Quantity, 1);
				}
				else
				{
					Assert.AreEqual(parts[$"HC{SizeToHoseClamp(largestSize).Code}"].Quantity, 1);
				}
			});
		}

		private PartSize SizeToHoseClamp(PartSize partSize) => partSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : partSize;
	}
}