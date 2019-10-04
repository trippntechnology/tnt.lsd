using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TNT.Cryptography;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner
{
	public static class Global
	{
		private static string _license_path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "license.txt");
		private static License _license = null;

		public static ApplicationRegistry userRegistry = null; // Initialized in Main()
		public static ApplicationRegistry machineRegistry = new ApplicationRegistry(Registry.LocalMachine, Properties.Resources.Company, Properties.Resources.Application);

		public static License GetLicense(bool swallowException = true)
		{
			if (_license == null)
			{
				var lines = File.ReadAllLines(_license_path).ToList();
				try
				{
					_license = Decrypt(lines);
				}
				catch (Exception)
				{
					if (!swallowException) throw;
				}
			}

			return _license;
		}

		public static License SetLicense(List<string> lines)
		{
			_license = Decrypt(lines);
			File.WriteAllLines(_license_path, lines);
			return _license;
		}

		private static License Decrypt(List<string> lines)
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