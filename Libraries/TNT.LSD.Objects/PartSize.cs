using System.Collections.Generic;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a part size
	/// </summary>
	sealed public class PartSize
	{
		/// <summary>
		/// Index of the size
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// Code that represents the size
		/// </summary>
		public string Code { get; private set; }

		/// <summary>
		/// Human readable format
		/// </summary>
		public string Readable { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="index">Index</param>
		/// <param name="code">Code</param>
		/// <param name="readable">Human readable format</param>
		public PartSize(int index, string code, string readable)
		{
			this.Index = index;
			this.Code = code;
			this.Readable = readable;
		}

		/// <summary>
		/// 1/2"
		/// </summary>
		public static PartSize SIZE_050 = new PartSize(0, "050", "1/2\"");
		/// <summary>
		/// 3/4"
		/// </summary>
		public static PartSize SIZE_075 = new PartSize(1, "075", "3/4\"");
		/// <summary>
		/// 1"
		/// </summary>
		public static PartSize SIZE_100 = new PartSize(2, "100", "1\"");
		/// <summary>
		/// 1-1/4"
		/// </summary>
		public static PartSize SIZE_125 = new PartSize(3, "125", "1-1/4\"");
		/// <summary>
		/// 1-1/2"
		/// </summary>
		public static PartSize SIZE_150 = new PartSize(4, "150", "1-1/2\"");
		/// <summary>
		/// 2"
		/// </summary>
		public static PartSize SIZE_200 = new PartSize(5, "200", "2\"");

		private static List<PartSize> Sizes = new List<PartSize>() { SIZE_050, SIZE_075, SIZE_100, SIZE_125, SIZE_150, SIZE_200 };

		/// <summary>
		/// Gets the <see cref="PartSize"/> represented by the index
		/// </summary>
		/// <param name="index">Index of the <see cref="PartSize"/></param>
		/// <returns>The <see cref="PartSize"/> represented by the index</returns>
		public static PartSize GetSize(int index) => Sizes.Find(size => size.Index == index);

		/// <summary>
		/// Gets the <see cref="PartSize"/> represented by the code
		/// </summary>
		/// <param name="code">Code of the <see cref="PartSize"/></param>
		/// <returns>The <see cref="PartSize"/> represented by the code</returns>
		public static PartSize GetSize(string code) => Sizes.Find(size => size.Code == code);

		/// <summary>
		/// Gets the <see cref="PartSize"/> represented by the human readable text
		/// </summary>
		/// <param name="readable">Human readable text</param>
		/// <returns>The <see cref="PartSize"/> represented by the human readable text</returns>
		public static PartSize GetSizeByReadable(string readable) => Sizes.Find(size => size.Readable == readable);

		/// <summary>
		/// Less than or equal operator
		/// </summary>
		public static bool operator <=(PartSize lhs, PartSize rhs) => lhs.Index <= rhs.Index;
		
		/// <summary>
		/// Greater than or equal operator
		/// </summary>
		public static bool operator >=(PartSize lhs, PartSize rhs) => lhs.Index >= rhs.Index;
		
		/// <summary>
		/// Less than operator
		/// </summary>
		public static bool operator <(PartSize lhs, PartSize rhs) => lhs.Index < rhs.Index;
		
		/// <summary>
		/// Greater than operator
		/// </summary>
		public static bool operator >(PartSize lhs, PartSize rhs) => lhs.Index > rhs.Index;

		/// <summary>
		/// Formatted for debugging inspection
		/// </summary>
		/// <returns><see cref="PartSize"/></returns>
		public override string ToString() => this.Code;
	}
}
