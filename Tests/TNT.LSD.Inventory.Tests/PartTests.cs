using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNT.LSD.Inventory.Tests
{
	[TestClass]
	public class PartTests
	{
		[TestMethod]
		public void Constructor()
		{
			var part = new Part();
			Assert.IsNull(part.Code);
			Assert.IsNull(part.Description);
			Assert.AreEqual(0, part.Quantity);
			Assert.AreEqual(false, part.Glueable);
			Assert.IsNull(part.ExternalPart);
		}

		[TestMethod]
		public void Copy_Constructor()
		{
			var part = new Part()
			{
				Code = "code",
				Description = "description",
				Quantity = 7,
				Glueable = true,
				ExternalPart = new Part()
				{
					Code = "excode",
					Description = "exdescription",
				}
			};

			var sut = new Part(part);

			Assert.AreEqual("code",part.Code);
			Assert.AreEqual("description",part.Description);
			Assert.AreEqual(7, part.Quantity);
			Assert.AreEqual(true, part.Glueable);
			Assert.IsNotNull(part.ExternalPart);
			Assert.AreEqual("excode", part.ExternalPart.Code);
			Assert.AreEqual("exdescription", part.ExternalPart.Description);
		}

		[TestMethod]
		public new void ToString()
		{
			var part = new Part()
			{
				Code = "code",
				Description = "description",
				Quantity = 7,
				ExternalPart = new Part()
				{
					Code = "excode",
					Description = "exdescription",
				}
			};

			Assert.AreEqual("(7) code: description", part.ToString());
		}
	}
}
