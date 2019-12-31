using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TNT.Cryptography;
using TNT.LiveData;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner
{
	public static class Global
	{
		private static string _license_path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "license.txt");
		public static LiveData<License> LicenseLive = new LiveData<License>();

		public static ApplicationRegistry userRegistry = null; // Initialized in Main()

		public static License GetLicense(bool swallowException = true)
		{
			if (LicenseLive.Value == null && File.Exists(_license_path))
			{
				var lines = File.ReadAllLines(_license_path).ToList();
				try
				{
					LicenseLive.Value = Decrypt(lines);
				}
				catch (Exception)
				{
					if (!swallowException) throw;
				}
			}

			return LicenseLive.Value;
		}

		public static License SetLicense(List<string> lines)
		{
			LicenseLive.Value = Decrypt(lines);
			File.WriteAllLines(_license_path, lines);
			return LicenseLive.Value;
		}

		public static License Decrypt(List<string> lines)
		{
			var encryptedText = lines.Count > 1 ? Symmetric.RemoveTags(lines) : lines.First();
			var cipher = new Cipher(Convert.FromBase64String(encryptedText));
			var symmetric = new Symmetric(Properties.Resources.key);
			var decryptedBytes = symmetric.Decrypt(cipher, cipher.IV);
			var plainText = Symmetric.Deserialize<string>(decryptedBytes);
			return Utilities.Deserialize<object>(plainText, new Type[] { typeof(License) }) as License;
		}
	}
}