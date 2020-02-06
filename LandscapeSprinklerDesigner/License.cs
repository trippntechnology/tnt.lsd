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

		[DisplayName("Application ID")]
		public int ApplicationID { get; set; }

		public string Secret { get; set; }

		[DisplayName("Service Endpoint")]
		public string ServiceEndpoint { get; set; }

		public License() { }
	}
}
