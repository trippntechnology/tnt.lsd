using System.Collections.Generic;

namespace TNT.LSD.Inventory
{
	/// <summary>
	/// Represents a <see cref="Dictionary{TKey, TValue}"/> where key is the part's code and value is the <see cref="Part"/>
	/// </summary>
	public class CodedParts : Dictionary<string, Part>
	{
		/// <summary>
		/// Adds <paramref name="quantity"/> to the <paramref name="code"/>. If a part with <paramref name="code"/> doesn't exist,
		/// it is created first.
		/// </summary>
		/// <param name="code">Code assocated with part</param>
		/// <param name="quantity">Amount to increase</param>
		public void Add(string code, int quantity)
		{
			if (!ContainsKey(code))
			{
				base.Add(code, new Part() { Code = code});
			}

			this[code].Quantity += quantity;
		}
	}
}
