using System;
using System.ComponentModel;

namespace LandscapeSprinklerDesigner
{
	public class License
	{
		[DisplayName("Issued To")]
		public string IssuedTo { get; set; }

		[DisplayName("Expires On")]
		public DateTime ExpiresOn { get; set; }

		public License() { }
	}
}
