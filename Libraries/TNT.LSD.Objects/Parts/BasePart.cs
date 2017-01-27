using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using TNT.LSD.Inventory;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Base class for all sprinkler system parts
	/// </summary>
	public abstract class BasePart : TNTObject
	{
		#region Static Members

		protected static TypeConverters.PipeSizeList m_PipeSizeList = new TypeConverters.PipeSizeList();

		#endregion

		#region Properties

		/// <summary>
		/// Returns the position of the part
		/// </summary>
		[Browsable(false)]
		virtual public Point Position { get { return ControlPoints.First().Position; } }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public BasePart()
			: base()
		{
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <param name="obj">Object to copy</param>
		public BasePart(BasePart obj)
			: base(obj)
		{
		}

		#endregion

		/// <summary>
		/// Call to set the parts quantities
		/// </summary>
		/// <param name="parts">Dictionary of parts</param>
		abstract public void SetPartQuantity(Dictionary<string, Part> parts);

		/// <summary>
		/// Called to deterimine if this pipe type can be added to this part
		/// </summary>
		/// <param name="pipeType">Pipe type</param>
		/// <param name="reason">When false, reason pipe can not be added</param>
		/// <returns>True if pipe type can be added, false otherwise</returns>
		virtual public bool CanAddPipe(Type pipeType, out string reason)
		{
			reason = string.Empty;
			return true;
		}

		/// <summary>
		/// Called to determine if part should terminate a pipe segment
		/// </summary>
		/// <returns>True if pipe should be terminated, false otherwise. (Default: false)</returns>
		virtual public bool TerminatePipe()
		{
			return false;
		}

		/// <summary>
		/// Get's the part with the key if exists, otherwise creates a part with the key
		/// </summary>
		/// <param name="parts">Listing of Parts</param>
		/// <param name="key">Key associated with the part</param>
		/// <returns>Part with the key if exists, otherwise creates a part with the key</returns>
		virtual public Part GetPart(Dictionary<string, Part> parts, string key)
		{
			if (!parts.ContainsKey(key))
			{
				parts.Add(key, new Part() { Code = key });
			}

			return parts[key];
		}
	}	
}
