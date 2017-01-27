using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TNT.LSD.Objects
{
	public class StateInfo
	{
		public Cursor Cursor { get; set; }
		public string ToolTipText { get; set; }

		public StateInfo()
		{
			Cursor = Cursors.Default;
			ToolTipText = string.Empty;
		}

		public StateInfo(Cursor cursor, string toolTipText)
		{
			Cursor = cursor;
			ToolTipText = toolTipText;
		}
	}
}
