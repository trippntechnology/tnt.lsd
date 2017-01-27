using System.Windows.Forms;

namespace TNT.LSD.Objects
{
	public static class TNTCursors
	{
		private static Cursor m_AddPoint;
		private static Cursor m_RemovePoint;
		private static Cursor m_CurvePoint;
		private static Cursor m_CurveTool;
		private static Cursor m_LineTool;
		private static Cursor m_RectangleTool;
		private static Cursor m_TextTool;
		private static Cursor m_CircleTool;
		private static Cursor m_SelectTool;
		private static Cursor m_LatPipe;
		private static Cursor m_NoLatPipe;
		private static Cursor m_MainPipe;
		private static Cursor m_NoMainPipe;
		private static Cursor m_RotationCursor;
		private static Cursor m_MovePoint;

		#region Properties

		public static Cursor SelectObject { get { return Cursors.Hand; } }

		public static Cursor RotateObject { get { return SelectObject; } }

		public static Cursor AddPoint
		{
			get
			{
				if (m_AddPoint == null)
				{
					m_AddPoint = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.AddPoint.cur");
				}

				return m_AddPoint;
			}
		}

		public static Cursor RemovePoint
		{
			get
			{
				if (m_RemovePoint == null)
				{
					m_RemovePoint = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.RemovePoint.cur");
				}

				return m_RemovePoint;
			}
		}

		public static Cursor MovePoint
		{
			get
			{
				if (m_MovePoint == null)
				{
					m_MovePoint = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.MovePoint.cur");
				}

				return m_MovePoint;
			}
		}

		public static Cursor CurvePoint
		{
			get
			{
				if (m_CurvePoint == null)
				{
					m_CurvePoint = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.AddCurve.cur");
				}

				return m_CurvePoint;
			}
		}

		public static Cursor CurveTool
		{
			get
			{
				if (m_CurveTool == null)
				{
					m_CurveTool = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.Curve.cur");
				}

				return m_CurveTool;
			}
		}

		public static Cursor LineTool
		{
			get
			{
				if (m_LineTool == null)
				{
					m_LineTool = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.Line.cur");
				}

				return m_LineTool;
			}
		}

		public static Cursor RectangleTool
		{
			get
			{
				if (m_RectangleTool == null)
				{
					m_RectangleTool = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.Rectangle.cur");
				}

				return m_RectangleTool;
			}
		}

		public static Cursor TextTool
		{
			get
			{
				if (m_TextTool == null)
				{
					m_TextTool = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.Text.cur");
				}

				return m_TextTool;
			}
		}

		public static Cursor CircleTool
		{
			get
			{
				if (m_CircleTool == null)
				{
					m_CircleTool = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.Circle.cur");
				}

				return m_CircleTool;
			}
		}

		public static Cursor SelectTool
		{
			get
			{
				if (m_SelectTool == null)
				{
					m_SelectTool = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.Select.cur");
				}

				return m_SelectTool;
			}
		}

		public static Cursor LatPipe
		{
			get
			{
				if (m_LatPipe == null)
				{
					m_LatPipe = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.LatPipe.cur");
				}

				return m_LatPipe;
			}
		}

		public static Cursor NoLatPipe
		{
			get
			{
				if (m_NoLatPipe == null)
				{
					m_NoLatPipe = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.No_LatPipe.cur");
				}

				return m_NoLatPipe;
			}
		}

		public static Cursor MainPipe
		{
			get
			{
				if (m_MainPipe == null)
				{
					m_MainPipe = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.MainPipe.cur");
				}

				return m_MainPipe;
			}
		}

		public static Cursor NoMainPipe
		{
			get
			{
				if (m_NoMainPipe == null)
				{
					m_NoMainPipe = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.No_MainPipe.cur");
				}

				return m_NoMainPipe;
			}
		}

		public static Cursor RotationCursor
		{
			get
			{
				if (m_RotationCursor == null)
				{
					m_RotationCursor = Utilities.Utilities.LoadColorCursor("TNT.LSD.Objects.Cursors.Rotate.cur");
				}

				return m_RotationCursor;
			}
		}

		public static Cursor Default { get { return Cursors.Default; } }

		#endregion
	}
}
