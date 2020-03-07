using System.Collections.Generic;
using System.Drawing;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents drawing options used by Draw method
	/// </summary>
	public class DrawingOptions
	{
		/// <summary>
		/// Indicates the coverage should be drawn
		/// </summary>
		public bool ShowCoverage { get; set; }

		/// <summary>
		/// Contains the objects in the parts layer
		/// </summary>
		public List<TNTObject> PartsLayer { get; set; }

		/// <summary>
		/// Indicates the pipes should auto size
		/// </summary>
		public bool AutoSizePipes { get; set; }

		/// <summary>
		/// Specifies the base font that should be used
		/// </summary>
		public Font BaseFont { get; set; }

		/// <summary>
		/// Specifies whether the heads should be labeled
		/// </summary>
		public bool LabelHeads { get; set; }

		/// <summary>
		/// Specifies whether distances should even be shown when object isn't selected
		/// </summary>
		public bool AlwaysShowDistances { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public DrawingOptions() { }

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj"><see cref="DrawingOptions"/> to copy</param>
		public DrawingOptions(DrawingOptions obj) : this()
		{
			this.ShowCoverage = obj.ShowCoverage;
			this.PartsLayer = obj.PartsLayer;
			this.AutoSizePipes = obj.AutoSizePipes;
			this.BaseFont = obj.BaseFont;
			this.LabelHeads = obj.LabelHeads;
			this.AlwaysShowDistances = obj.AlwaysShowDistances;
		}
	}
}
