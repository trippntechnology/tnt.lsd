using System;
using System.Drawing;
using System.Globalization;

namespace TNT.LSD.Colorizer
{
	public static class Constants
	{
		/// <summary>
		/// Unique colors (see https://sashat.me/2017/01/11/list-of-20-simple-distinct-colors/)
		/// </summary>
		public static Color[] DEFAULT_COLORS =
		{
			Color.FromArgb(Int32.Parse("ffe6194B", NumberStyles.AllowHexSpecifier)), // Red
			Color.FromArgb(Int32.Parse("ff3cb44b", NumberStyles.AllowHexSpecifier)), // Green
			Color.FromArgb(Int32.Parse("ffffe119", NumberStyles.AllowHexSpecifier)), // Yellow
			Color.FromArgb(Int32.Parse("ff4363d8", NumberStyles.AllowHexSpecifier)), // Blue
			Color.FromArgb(Int32.Parse("fff58231", NumberStyles.AllowHexSpecifier)), // Orange
			Color.FromArgb(Int32.Parse("ff42d4f4", NumberStyles.AllowHexSpecifier)), // Cyan
			Color.FromArgb(Int32.Parse("fff032e6", NumberStyles.AllowHexSpecifier)), // Magenta
			Color.FromArgb(Int32.Parse("fffabebe", NumberStyles.AllowHexSpecifier)), // Pink
			Color.FromArgb(Int32.Parse("ff469990", NumberStyles.AllowHexSpecifier)), // Teal
			Color.FromArgb(Int32.Parse("ffe6beff", NumberStyles.AllowHexSpecifier)), // Lavender
			Color.FromArgb(Int32.Parse("ff9A6324", NumberStyles.AllowHexSpecifier)), // Brown
			Color.FromArgb(Int32.Parse("fffffac8", NumberStyles.AllowHexSpecifier)), // Beige
			Color.FromArgb(Int32.Parse("ff800000", NumberStyles.AllowHexSpecifier)), // Maroon
			Color.FromArgb(Int32.Parse("ffaaffc3", NumberStyles.AllowHexSpecifier)), // Mint
			Color.FromArgb(Int32.Parse("ff000075", NumberStyles.AllowHexSpecifier)), // Navy
			Color.FromArgb(Int32.Parse("ffa9a9a9", NumberStyles.AllowHexSpecifier))  // Grey
		};
	}
}
