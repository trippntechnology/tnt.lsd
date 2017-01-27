using Ionic.Zip;
using LSDComponents.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using TNT.LSD.Inventory;
using TNT.LSD.Inventory.DAL;
using TNT.LSD.Objects;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;
using TNT.LSD.Objects.Interfaces;
using TNT.Math;
using TNT.Utilities;
using TNT.Utilities.CommandManagement;

namespace LSDComponents
{
	using BitmapList = System.Collections.Generic.List<System.Drawing.Bitmap>;
	using ObjectList = System.Collections.Generic.List<TNT.LSD.Objects.TNTObject>;
	using UndoActionList = List<UndoAction>;
	using UndoActionStack = Stack<List<UndoAction>>;

	public delegate void ObjectsSelectedDelegate(object[] objs);
	public delegate void PartsUpdatedDelegate(List<Part> parts);
	public delegate void FileNameChanged(string fileName);

	public partial class TNTCAD : Control
	{
		#region Members

		Type[] m_ExpectedTypes = null;

		private Point m_LastLocation = Point.Empty;
		protected BitmapList m_DrawingLayers = new BitmapList();
		protected TNTObject m_ConstructedObject = null;
		protected UndoActionList m_PropertyGridUndoActions = null;
		protected internal TNTCADState m_State = null;
		protected string m_CurrentFileName = string.Empty;

		protected int m_Scale = 100;
		protected int m_ActiveLayer = 1;
		protected DrawingModes.DrawingMode m_DrawingMode = new DrawingModes.NullMode();

		protected UndoActionStack m_UndoActions = new UndoActionStack();

		protected List<int> m_DownKeys = new List<int>();

		protected DrawingOptions m_DrawingOptions = null;

		protected TNT.LSD.Objects.TypeConverters.PipeSizeList m_PipeSizeList = new TNT.LSD.Objects.TypeConverters.PipeSizeList();

		protected SaveFileDialog m_SaveFileDialog = new SaveFileDialog() { DefaultExt = "lsdx", Filter = "Landscape Sprinkler Design files|*.lsdx|Landscape Sprinkler Design files|*.lsd", Title = "Save Layout" };

		#endregion

		#region Properties

		[DefaultValue(100)]
		public int DisplayScale
		{
			get { return m_Scale; }
			set
			{
				if (value > 0 && value < 201)
				{
					m_Scale = value;
					HeightInFeet = m_State.HeightInFeet;
					WidthInFeet = m_State.WidthInFeet;
				}
			}
		}

		[DefaultValue(typeof(Color), "Aqua")]
		public Color GridColor
		{
			get { return m_State.GridColor; }
			set
			{
				m_State.GridColor = value;
				DrawLayers(0);
			}
		}

		[DefaultValue(150)]
		public int GridColorAlphaValue
		{
			get { return m_State.GridColorAlphaValue; }
			set
			{
				if (value > -1 && value < 256)
				{
					m_State.GridColorAlphaValue = value;
					DrawLayers(0);
				}
			}
		}

		[DefaultValue(1)]
		public int DrawingLayers
		{
			get { return m_DrawingLayers != null ? m_DrawingLayers.Count - 1 : 0; }
			set
			{
				if (value < 1)
				{
					while (m_DrawingLayers.Count > 1)
					{
						m_DrawingLayers.RemoveAt(m_DrawingLayers.Count - 1);
					}

					while (m_DrawingLayers.Count < 1)
					{
						m_DrawingLayers.Add(new Bitmap(1, 1));
					}
				}
				else if (m_DrawingLayers.Count < value + 1)
				{
					while (m_DrawingLayers.Count < value + 1)
					{
						m_DrawingLayers.Add(new Bitmap(1, 1));
					}
				}
				else if (m_DrawingLayers.Count > value + 1)
				{
					while (m_DrawingLayers.Count > value + 1)
					{
						m_DrawingLayers.RemoveAt(m_DrawingLayers.Count - 1);
					}
				}

				while (m_State.ObjectLayers.Count < m_DrawingLayers.Count)
				{
					m_State.ObjectLayers.Add(new ObjectList());
				}

				while (m_State.ObjectLayers.Count > m_DrawingLayers.Count)
				{
					m_State.ObjectLayers.RemoveAt(m_State.ObjectLayers.Count - 1);
				}

				DrawLayers(0);
			}
		}

		[DefaultValue(1)]
		public int ActiveLayer
		{
			get { return m_ActiveLayer; }
			set
			{
				UnselectAll();
				DrawLayers(1);

				if (value < 1)
				{
					m_ActiveLayer = 1;
				}
				else if (value >= m_DrawingLayers.Count)
				{
					value = m_DrawingLayers.Count - 1;
				}
				else
				{
					m_ActiveLayer = value;
				}
			}
		}

		[DefaultValue(false)]
		public bool SnapToGrid { get; set; }

		[DefaultValue(200)]
		public int HeightInFeet
		{
			get { return m_State.HeightInFeet; }
			set
			{
				m_State.HeightInFeet = value;
				Height = (int)(m_State.HeightInFeet * TNTConstants.PIXELS_PER_FOOT * m_Scale / 100.0);
			}
		}

		[DefaultValue(200)]
		public int WidthInFeet
		{
			get { return m_State.WidthInFeet; }
			set
			{
				m_State.WidthInFeet = value;
				Width = (int)(m_State.WidthInFeet * TNTConstants.PIXELS_PER_FOOT * m_Scale / 100.0);
			}
		}

		public DrawingModes.DrawingMode DrawingMode
		{
			get { return m_DrawingMode; }
			set
			{
				m_DrawingMode.Reset(this);

				m_DrawingMode = value;
				m_DrawingMode.ShowPartsToolTip = ShowPartsToolTip;
				m_DrawingMode.Reset(this);
				UnselectAll();
				DrawLayers(m_ActiveLayer);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TNTCADState State
		{
			get { return m_State; }
			set
			{
				m_State = value;
				m_UndoActions.Clear();
				DrawLayers(0);
			}
		}

		public CADSettings Settings { get { return m_State.Settings as CADSettings; } }

		public bool HasUnsavedChanges { get { return m_UndoActions.Count > 0; } }

		private Type[] ExpectedTypes
		{
			get
			{
				if (m_ExpectedTypes == null)
				{
					List<Type> types = new List<Type>();
					types.AddRange(Utilities.GetNameSpaceTypes("TNT.LSD.Objects", string.Concat(Application.StartupPath, "\\", "TNT.LSD.Objects.dll"), typeof(TNTObject)));
					types.AddRange(Utilities.GetNameSpaceTypes("TNT.LSD.Objects.ControlPoints", string.Concat(Application.StartupPath, "\\", "TNT.LSD.Objects.dll"), typeof(TNTControlPoint)));
					types.AddRange(Utilities.GetNameSpaceTypes("LSDComponents.Settings", string.Concat(Application.StartupPath, "\\", "LSDComponents.dll"), typeof(CADSettings)));
					m_ExpectedTypes = types.ToArray();
				}

				return m_ExpectedTypes;
			}
		}

		public DrawingOptions DrawingOptions { get { return m_DrawingOptions; } }

		public List<TNTObject> ActiveObjects { get { return m_State.ObjectLayers[ActiveLayer]; } }

		public List<TNTObject> SelectedObjects
		{
			get
			{
				List<TNTObject> selected = new ObjectList();
				m_State.ObjectLayers.ForEach(ol => selected.AddRange(ol.FindAll(o => o.Selected)));
				return selected;
			}
		}

		[XmlIgnore()]
		public Command ShowPartsToolTip { get; set; }

		[XmlIgnore()]
		public Image Design { get { return m_DrawingLayers.Last(); } }

		public bool AreaAvailable { get { return (from s in Selected<TNTObject>() where s is IShape select s).ToList().Count > 0; } }

		public bool LengthAvailable { get { return (from s in Selected<TNTObject>() where s is IMeasurable select s).ToList().Count > 0; } }

		internal UndoActionStack UndoActions { get { return m_UndoActions; } }

		public string CurrentFileName
		{
			get { return m_CurrentFileName; }
			set
			{
				m_CurrentFileName = value;

				if (OnFileNameChanged != null)
				{
					OnFileNameChanged(m_CurrentFileName);
				}
			}
		}

		#endregion

		#region Events

		public event ObjectsSelectedDelegate OnObjectsSelected;
		public event PartsUpdatedDelegate OnPartsUpdated;
		public event FileNameChanged OnFileNameChanged;

		#endregion

		public TNTCAD()
		{
			InitializeComponent();

			m_State = new TNTCADState(this);
			DoubleBuffered = true;
			m_DrawingLayers.Add(new Bitmap(1, 1));
			m_DrawingOptions = new DrawingOptions() { BaseFont = Font };
		}

		#region Drawing methods

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			// Draw the last layer
			if (m_DrawingLayers != null && m_DrawingLayers.Count > 0)
			{
				Graphics graphics = pe.Graphics;
				float scale = (float)(m_Scale / 100.0);

				if (Parent != null)
				{
					graphics.DrawImage(m_DrawingLayers.Last(), pe.ClipRectangle, pe.ClipRectangle, GraphicsUnit.Pixel);
				}

				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.ResetTransform();
				graphics.ScaleTransform(scale, scale);

				if (SelectedObjects.Count > 0)
				{
					// Draw selected
					foreach (TNTObject obj in SelectedObjects)
					{
						obj.Draw(graphics, DrawingOptions);
					}
				}
				else if (m_ConstructedObject != null)
				{
					m_ConstructedObject.Draw(graphics, DrawingOptions);
				}
			}
		}

		protected internal void DrawLayers(int startLayer)
		{
			if (Width > 0 && Height > 0)
			{
				for (int layer = startLayer; layer < m_DrawingLayers.Count; layer++)
				{
					DrawLayer(layer);
				}

				Refresh();
			}

			if (OnPartsUpdated != null)
			{
				OnPartsUpdated(GetPartsList());
			}
		}

		protected internal void DrawLayer(int layer)
		{
			// Create new bitmap for layer with matching Width and Height match
			while (m_DrawingLayers.Count <= layer)
			{
				m_DrawingLayers.Add(new Bitmap(Width, Height));
			}

			if (m_DrawingLayers[layer].Width != Width || m_DrawingLayers[layer].Height != Height)
			{
				m_DrawingLayers[layer] = new Bitmap(Width, Height);
			}

			if (layer == 0)
			{
				// Draw the grid layer
				DrawGridLayer();
			}
			else if (layer > 0)
			{
				float scale = (float)(m_Scale / 100.0);
				DrawingOptions.PartsLayer = State.ObjectLayers[2];

				// Get the layer
				Bitmap layerBitmap = m_DrawingLayers[layer];
				// Setup a Graphics object for the layer
				Graphics graphics = Graphics.FromImage(layerBitmap);

				var valves = (from o in m_State.ObjectLayers.Last() where o is Valve select (o as Valve)).ToList();

				#region Before drawing, size the pipe for capacity

				if (layerBitmap == m_DrawingLayers.Last() && DrawingOptions.AutoSizePipes)
				{
					// Mark each part as unsized
					m_State.ObjectLayers.Last().ForEach(o =>
					{
						if (o is TNTPart)
						{
							(o as TNTPart).Sized = false;
						}
					});

					// Size from valves first
					valves.ForEach(v => v.SizePipe(null));

					// Size from source down
					var sources = (from o in m_State.ObjectLayers.Last() where o is TNTSource select (o as TNTSource)).ToList();
					sources.ForEach(s => s.SizePipe(null));
				}

				#endregion

				#region Colorize the zone

				// Set all the objects back to black
				m_State.ObjectLayers.Last().ForEach(o =>
					{
						if (o is TNTPart && !(o is Valve))
						{
							(o as TNTPart).Color = Color.Black;
						}
					});

				valves.ForEach(v => v.ColorizeZone());

				#endregion

				// Since this layer is changed, restore to the previous layer so that
				// layer can be updated
				graphics.DrawImage(m_DrawingLayers[layer - 1], 0, 0);

				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.ResetTransform();
				graphics.ScaleTransform(scale, scale);

				if (layer < m_State.ObjectLayers.Count)
				{
					// For the layer indicated, draw unselected objects; pipe first other objects next. Objects 
					// are drawn in reverse since the first object is on top and needs to be drawn last.

					for (int index = m_State.ObjectLayers[layer].Count - 1; index >= 0; index--)
					{
						TNTObject obj = m_State.ObjectLayers[layer][index];

						if (!obj.Selected)
						{
							obj.Draw(graphics, DrawingOptions);
						}
					}
				}
			}
		}

		protected internal void DrawGridLayer()
		{
			Bitmap gridLayer = m_DrawingLayers[0];

			Graphics graphicsGrid = Graphics.FromImage(gridLayer);

			float scale = (float)(m_Scale / 100.0);
			Rectangle rect = new Rectangle(0, 0, Width, Height);

			// Clear the canvas
			graphicsGrid.FillRectangle(new SolidBrush(Color.White), rect);

			if (State.BackgroundImage != null && State.ShowBackgroundImage)
			{
				graphicsGrid.DrawImage(State.BackgroundImage, rect);
			}

			Pen pen = new Pen(Color.FromArgb(m_State.GridColorAlphaValue, m_State.GridColor));
			graphicsGrid.ScaleTransform(scale, scale);

			#region Draw the grid

			if (State.DrawGrid)
			{
				int adjWidth = (int)(Width * 1.0 / scale);
				int adjHeight = (int)(Height * 1.0 / scale);

				for (int x = 0; x < adjWidth; x += TNTConstants.PIXELS_PER_FOOT)
				{
					if (x % (10 * TNTConstants.PIXELS_PER_FOOT) == 0)
					{
						pen.Width = 3;
						graphicsGrid.DrawLine(pen, x, 0, x, adjHeight);
						pen.Width = 1;
					}
					else if (m_Scale >= 70)
					{
						graphicsGrid.DrawLine(pen, x, 0, x, adjHeight);
					}
					else if (m_Scale >= 60 && (x % (2 * TNTConstants.PIXELS_PER_FOOT) == 0))
					{
						graphicsGrid.DrawLine(pen, x, 0, x, adjHeight);
					}
				}

				for (int y = 0; y < Height * 1.0 / scale; y += TNTConstants.PIXELS_PER_FOOT)
				{
					if (y % (10 * TNTConstants.PIXELS_PER_FOOT) == 0)
					{
						pen.Width = 3;
						graphicsGrid.DrawLine(pen, 0, y, adjWidth, y);
						pen.Width = 1;
					}
					else if (m_Scale >= 70)
					{
						graphicsGrid.DrawLine(pen, 0, y, adjWidth, y);
					}
					else if (m_Scale >= 60 && (y % (2 * TNTConstants.PIXELS_PER_FOOT) == 0))
					{
						graphicsGrid.DrawLine(pen, 0, y, adjWidth, y);
					}
				}
			}
			#endregion // Draw the grid

			#region Draw the units

			if (m_State.DrawUnits)
			{
				SolidBrush sBrush = new SolidBrush(Color.Black);
				Font myFont = new Font(Font.Name, Font.Size);
				RectangleF textRect = new RectangleF(-100.0f, -25.0f, 200.0f, 50.0f);
				StringFormat format = new StringFormat();
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Center;
				graphicsGrid.TranslateTransform((10 * TNTConstants.PIXELS_PER_FOOT), TNTConstants.PIXELS_PER_FOOT * 2);

				for (int x = (10 * TNTConstants.PIXELS_PER_FOOT); x < Width * 1.0 / scale; x += (10 * TNTConstants.PIXELS_PER_FOOT))
				{
					graphicsGrid.DrawString((x / TNTConstants.PIXELS_PER_FOOT).ToString(), myFont, sBrush, textRect, format);
					graphicsGrid.TranslateTransform((10 * TNTConstants.PIXELS_PER_FOOT), 0);
				}

				graphicsGrid.ResetTransform();
				graphicsGrid.ScaleTransform(scale, scale);
				graphicsGrid.TranslateTransform(TNTConstants.PIXELS_PER_FOOT * 2, (10 * TNTConstants.PIXELS_PER_FOOT));

				for (int x = (10 * TNTConstants.PIXELS_PER_FOOT); x < Height * 1.0 / scale; x += (10 * TNTConstants.PIXELS_PER_FOOT))
				{
					graphicsGrid.DrawString((x / TNTConstants.PIXELS_PER_FOOT).ToString(), myFont, sBrush, textRect, format);
					graphicsGrid.TranslateTransform(0, (10 * TNTConstants.PIXELS_PER_FOOT));
				}
			}

			#endregion // Draw the units
		}

		#endregion

		#region Overridden methods

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			// This check is done so that OnMouseMove isn't called the the location hasn't really changed
			// So that undo events aren't done
			if (m_LastLocation != e.Location)
			{
				m_DrawingMode.OnMouseMove(this, e, ModifierKeys);
				m_LastLocation = e.Location;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			m_DrawingMode.OnMouseUp(this, e, ModifierKeys);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			m_DrawingMode.OnMouseDown(this, e, ModifierKeys);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);
			m_DrawingMode.OnMouseDoubleClick(this, e);
		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			m_DrawingMode.OnMouseClick(this, e, ModifierKeys);
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			base.OnClientSizeChanged(e);
			DrawLayers(0);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.Text = string.Empty;

			// Refresh to remove location point.
			Refresh();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.Focus();
		}

		public new Graphics CreateGraphics()
		{
			Graphics graphics = base.CreateGraphics();
			float scale = (float)(m_Scale / 100.0);

			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.ScaleTransform(scale, scale);

			return graphics;
		}

		public new void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			// Only send one key down event
			if (!m_DownKeys.Contains(e.KeyValue))
			{
				m_DownKeys.Add(e.KeyValue);
				m_DrawingMode.OnKeyDown(this, e);
			}
		}

		public new void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			m_DownKeys.Remove(e.KeyValue);
			m_DrawingMode.OnKeyUp(this, e);
		}

		#endregion

		#region Protected Methods

		protected internal void UnselectAll(TNTObject excludedObject)
		{
			Unselect(SelectedObjects.FindAll(o => o != excludedObject));
		}

		protected internal void Unselect(ObjectList objs)
		{
			objs.ForEach(o => o.Selected = false);

			if (OnObjectsSelected != null)
			{
				OnObjectsSelected(new object[0]);
			}
		}

		protected internal Rectangle CreateRectangle(Point p1, Point p2)
		{
			int x1 = Math.Min(p1.X, p2.X);
			int y1 = Math.Min(p1.Y, p2.Y);
			int width = Math.Abs(x1 - Math.Max(p1.X, p2.X));
			int height = Math.Abs(y1 - Math.Max(p1.Y, p2.Y));

			return new Rectangle(x1, y1, width, height);
		}

		protected internal void PrepareToUndoSelected()
		{
			UndoActionList uaList = new UndoActionList();

			foreach (TNTObject o in SelectedObjects)
			{
				uaList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, o, m_State.ObjectLayers[m_ActiveLayer]));
			}

			m_UndoActions.Push(uaList);
		}

		protected internal void TriggerOnObjectsSelected()
		{
			if (OnObjectsSelected != null)
			{
				m_PropertyGridUndoActions = new UndoActionList();

				if (SelectedObjects.Count > 0)
				{
					foreach (TNTObject o in SelectedObjects)
					{
						m_PropertyGridUndoActions.Add(new UndoAction(UndoAction.UndoActionType.uaModify, o, m_State.ObjectLayers[m_ActiveLayer]));
					}

					if (SelectedObjects.Find(o => !(o is Pipe)) != null)
					{
						OnObjectsSelected(SelectedObjects.FindAll(o => !(o is Pipe)).ToArray());
					}
					else
					{
						OnObjectsSelected(SelectedObjects.ToArray());
					}
				}
			}
		}

		protected void CreateUndoAction(TNTObject obj, UndoAction.UndoActionType undoType)
		{
			ObjectList list = new ObjectList();
			list.Add(obj);
			CreateUndoActions(list, undoType);
		}

		protected void CreateUndoActions<T>(List<T> objs, UndoAction.UndoActionType undoType) where T : TNTObject
		{
			UndoActionList uaList = new UndoActionList();

			foreach (T o in objs)
			{
				uaList.Add(new UndoAction(undoType, o, m_State.ObjectLayers[m_ActiveLayer]));
			}

			m_UndoActions.Push(uaList);
		}

		/// <summary>
		/// Applies each of the transforms found in the Transforms directory that are newer than the current version
		/// of the doc
		/// </summary>
		/// <param name="doc">Document to transform</param>
		/// <returns>XmlDocument with the transforms applied</returns>
		protected XmlDocument ApplyTransformations(XmlDocument doc)
		{
			var transformFiles = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Transforms"));
			Dictionary<Version, string> transforms = new Dictionary<Version, string>();

			foreach (var transformFile in transformFiles)
			{
				try
				{
					transforms.Add(new Version(Path.GetFileNameWithoutExtension(transformFile)), transformFile);
				}
				catch { }
			}

			XmlNode versionNode = doc.DocumentElement.SelectSingleNode("/TNTCADState/Version");
			Version version = new Version(versionNode.InnerText);

			// Find all transforms that are newer than the file being loaded
			var newerTransforms = (from t in transforms orderby t.Key where t.Key > version select t).ToList();

			XslCompiledTransform xsl = new XslCompiledTransform();
			StringBuilder sb = new StringBuilder();

			// Apply each transform
			foreach (var file in newerTransforms)
			{
				using (StringWriter sw = new StringWriter(sb))
				{
					xsl.Load(file.Value);
					xsl.Transform(doc, null, sw);
				}

				doc.LoadXml(sb.ToString());
				sb.Length = 0;
			}

			return doc;
		}

		#endregion

		#region Public Methods

		public void UnselectAll()
		{
			UnselectAll(null);
		}

		/// <summary>
		/// This method was added to eliminate the need to call the same three properties that do the same thing thus eliminating
		/// several duplicate calls
		/// </summary>
		/// <param name="paletteProperties">Object containing the information</param>
		public void SetPalletNodeTag(PaletteProperties paletteProperties)
		{
			int layer = paletteProperties.DrawingMode.Layer;

			if (layer > 0 && layer < m_DrawingLayers.Count)
			{
				if (layer > m_ActiveLayer)
				{
					UnselectAll();
					DrawLayer(m_ActiveLayer);
				}

				m_ActiveLayer = layer;
			}

			DrawingMode = paletteProperties.DrawingMode;
		}

		public void AddObject(TNTObject obj)
		{
			AddObjects(new ObjectList(new TNTObject[] { obj }));
		}

		public void AddObjects(List<TNTObject> objs)
		{
			// Make sure layer exists
			while (m_State.ObjectLayers.Count < m_ActiveLayer + 1)
			{
				m_State.ObjectLayers.Add(new ObjectList());
			}

			foreach (TNTObject obj in objs)
			{
				if (obj is Pipe)
				{
					// Add to back so that pipe is always drawn first
					m_State.ObjectLayers[m_ActiveLayer].Add(obj);
				}
				else
				{
					m_State.ObjectLayers[m_ActiveLayer].Insert(0, obj);
				}
			}

			CreateUndoActions<TNTObject>(objs, UndoAction.UndoActionType.uaDelete);

			if (m_State.ShowLegend)
			{
				DrawLayers(1);
			}
			else
			{
				DrawLayers(m_ActiveLayer);
			}
		}

		public void Copy()
		{
			List<TNTObject> copiedObjects = new ObjectList();
			UndoActionList uaList = new UndoActionList();

			foreach (TNTObject o in SelectedObjects)
			{
				if (o.CanClone)
				{
					TNTObject obj = o.Clone();

					if (SnapToGrid)
					{
						obj.AlignToGrid();
					}

					copiedObjects.Add(obj);
				}
			}

			UnselectAll();

			CreateUndoActions(copiedObjects, UndoAction.UndoActionType.uaDelete);

			foreach (TNTObject o in copiedObjects)
			{
				o.MoveBy(TNTConstants.PIXELS_PER_FOOT, TNTConstants.PIXELS_PER_FOOT, false);
				o.Selected = true;
				m_State.ObjectLayers[m_ActiveLayer].Add(o);
			}

			DrawLayers(m_ActiveLayer);
		}

		public void Delete()
		{
			UndoActionList uaList = new UndoActionList();

			List<TNTObject> selectedObjs = (from o in SelectedObjects where !(o is Legend) orderby !(o is Pipe) select o).ToList();

			foreach (TNTObject o in selectedObjs)
			{
				// Anything that is selected will be deleted so add uaInsert/uaAdd events
				uaList.Add(new UndoAction(o is Pipe ? UndoAction.UndoActionType.uaAdd : UndoAction.UndoActionType.uaInsert, o, ActiveObjects));
			}

			foreach (Pipe p in selectedObjs.FindAll(o => o is Pipe))
			{
				// Add a auModify for any non-select part associated with a selected pipe
				List<TNTPart> parts = new List<TNTPart>(new TNTPart[] { p.Part1, p.Part2 });

				foreach (TNTPart part in parts.FindAll(p1 => !p1.Selected))
				{
					if (part is Fitting && part.Pipes.Count == 1)
					{
						// This fitting will be orphaned so select it an add it to selectedObjs to be removed
						uaList.Add(new UndoAction(UndoAction.UndoActionType.uaInsert, part, ActiveObjects));
						selectedObjs.Add(part);
					}
					else
					{
						uaList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, part, ActiveObjects));
						part.Pipes.Remove(p);
					}
				}
			}

			// Remove parts
			selectedObjs.ForEach(o => ActiveObjects.Remove(o));

			// Clears property editor
			UnselectAll();

			m_UndoActions.Push(uaList);

			if (m_State.ShowLegend)
			{
				DrawLayers(1);
			}
			else
			{
				DrawLayers(m_ActiveLayer);
			}
		}

		public void Undo()
		{
			UnselectAll();

			if (m_UndoActions.Count > 0)
			{
				UndoActionList uaList = m_UndoActions.Pop();

				foreach (UndoAction ua in uaList)
				{
					ua.Execute();
				}

				DrawLayers(m_ActiveLayer);
			}
		}

		public void ShowPropertyChanges()
		{
			if (m_PropertyGridUndoActions != null)
			{
				// Add the undo actions for objects in property editor
				m_UndoActions.Push(m_PropertyGridUndoActions);
			}

			DrawLayers(m_ActiveLayer);
		}

		public void Repaint(int? layer = null)
		{
			DrawLayers(layer == null ? m_ActiveLayer : (int)layer);
		}

		public string Serialize()
		{
			Repaint();
			return Utilities.Serialize<TNTCADState>(m_State, ExpectedTypes);
		}

		public void Deserialize(string content)
		{
			TNTCADState cadState = Utilities.Deserialize<TNTCADState>(content, ExpectedTypes);

			cadState.Parent = this;

			// Reconcile object/events that weren't serialized.
			cadState.ResolveReferences();

			// Force Height and Width in Feet to refresh so that Hieght and Width get updated.
			cadState.HeightInFeet = cadState.HeightInFeet;
			cadState.WidthInFeet = cadState.WidthInFeet;

			State = cadState;

			DrawLayers(0);
		}

		public new void BringToFront()
		{
			// Remove selected objects and add to end of list
			foreach (TNTObject obj in SelectedObjects)
			{
				m_State.ObjectLayers[m_ActiveLayer].Remove(obj);
				m_State.ObjectLayers[m_ActiveLayer].Insert(0, obj);
			}

			UnselectAll();
			DrawLayers(m_ActiveLayer);
		}

		public new void SendToBack()
		{
			// Remove selected objects and add to end of list
			foreach (TNTObject obj in SelectedObjects)
			{
				m_State.ObjectLayers[m_ActiveLayer].Remove(obj);
				m_State.ObjectLayers[m_ActiveLayer].Add(obj);
			}

			UnselectAll();
			DrawLayers(m_ActiveLayer);
		}

		public void ClearUndoQueue()
		{
			m_UndoActions.Clear();
		}

		public void SelectAll()
		{
			State.SelectAll();

			// Redraw all layers that contain objects.
			DrawLayers(1);

			// Add objects to property editor
			OnObjectsSelected(SelectedObjects.ToArray());
		}

		public void SetStateInfo(StateInfo stateInfo)
		{
			if (stateInfo != null)
			{
				this.Cursor = stateInfo.Cursor;
				this.Text = stateInfo.ToolTipText;
			}
			else
			{
				this.Cursor = Cursors.Default;
				this.Text = string.Empty;
			}
		}

		public void SaveAsImage(string fileName)
		{
			switch (Path.GetExtension(fileName))
			{
				case ".jpg":

					m_DrawingLayers.Last().Save(fileName, ImageFormat.Jpeg);
					break;

				case ".png":

					m_DrawingLayers.Last().Save(fileName, ImageFormat.Png);
					break;
			}
		}

		public void AlignToGrid()
		{
			CreateUndoActions(SelectedObjects, UndoAction.UndoActionType.uaModify);

			foreach (TNTObject o in SelectedObjects)
			{
				o.AlignToGrid();
			}

			Repaint();
		}

		public void SpaceSelectedEqually()
		{
			// This only works with TNTPart
			List<TNTPart> parts = (from s in SelectedObjects where s is TNTPart select s as TNTPart).ToList();
			int initialCount = parts.Count;

			CreateUndoActions(parts, UndoAction.UndoActionType.uaModify);

			// Find the two parts that are furthest appart
			TNTPart initialPart = null;
			TNTPart terminalPart = null;
			double maxDist = 0;

			for (int index1 = 0; index1 < parts.Count - 1; index1++)
			{
				Point p1 = parts[index1].Position;
				Point[] points = (from i in parts.GetRange(index1 + 1, parts.Count - (index1 + 1)) select i.Position).ToArray();
				double distance = 0;

				Point p2 = p1.FindExtremePoints(points, out distance);

				if (distance > maxDist)
				{
					initialPart = parts[index1];
					terminalPart = parts.Find(i => i.Position == p2);
					maxDist = distance;
				}
			}

			parts.Remove(initialPart);
			parts.Remove(terminalPart);

			parts.Insert(0, initialPart);
			parts.Add(terminalPart);

			Vector vector = new Vector(initialPart.Position, terminalPart.Position);
			Vector segment = vector.Unit * (vector.Magnitude / (parts.Count - 1));

			for (int index = 1; index < parts.Count - 1; index++)
			{
				PointF newPosition = initialPart.Position + (vector.Unit * vector.Magnitude * index / (parts.Count - 1));
				parts[index].MoveTo((int)newPosition.X, (int)newPosition.Y, true);
			}

			Repaint();
		}

		/// <summary>
		/// Call to get the selected objects that are of type T
		/// </summary>
		/// <typeparam name="T">Type of selected objects to return</typeparam>
		/// <returns>List of type T objects that are selected</returns>
		public List<T> Selected<T>() where T : TNTObject
		{
			return (from s in SelectedObjects where s is T select s as T).ToList();
		}

		public List<Part> GetPartsList()
		{
			List<TNTObject> partsObjects = m_State.ObjectLayers[2];
			Dictionary<string, Part> inventoryParts = DALPart.GetParts();
			List<BasePart> parts = (from p in partsObjects where p is BasePart select p as BasePart).ToList();

			// When calculating parts, deterimine if valves are within a valve box and create the association
			List<ValveBox> valveBoxes = (from p in parts where p is ValveBox select p as ValveBox).ToList();
			List<Valve> valves = (from p in parts where p is Valve select p as Valve).ToList();

			valveBoxes.ForEach(vb => vb.AssociatedValves = null);
			valves.ForEach(v => v.AssociatedValveBox = null);

			foreach (ValveBox vb in valveBoxes)
			{
				foreach (Valve v in valves)
				{
					if (vb.MouseOver(v.Position, Keys.None) != null)
					{
						v.AssociatedValveBox = vb;

						if (vb.AssociatedValves == null)
						{
							vb.AssociatedValves = new List<Valve>();
						}

						vb.AssociatedValves.Add(v);
					}
				}
			}

			// Set part quantities
			parts.ForEach(p => p.SetPartQuantity(inventoryParts));

			TNTSource source = parts.Find(p => p is TNTSource) as TNTSource;

			// Mainline drains
			if (source != null && source.Pipes.Count > 0)
			{
				inventoryParts["AUTOML"].Quantity += State.MainlineDrains;
				string pipeCode = m_PipeSizeList.SizeToCode(source.Pipes[0].PipeSize);
				inventoryParts[string.Format("FI{0}X050SSTTEE", pipeCode)].Quantity += State.MainlineDrains;
			}

			// Lateral drains
			inventoryParts["AUTOLD"].Quantity += valves.Count * State.LateralDrains;
			inventoryParts["FI075X050SSTTEE"].Quantity += valves.Count * State.LateralDrains;

			// Add static parts
			foreach (Part part in State.StaticParts)
			{
				if (inventoryParts.ContainsKey(part.Code))
				{
					inventoryParts[part.Code].Quantity += part.Quantity;
				}
			}

			// Keep only those parts that have a quantity
			List<Part> neededParts = (from p in inventoryParts where p.Value.Quantity > 0 select p.Value).ToList();

			if (State.RoundPipe)
			{
				// Round pipe to nearest 20' length
				neededParts.ForEach(p => { if ((Regex.IsMatch(p.Code, "^PI") && !Regex.IsMatch(p.Code, "200C")) || Regex.IsMatch(p.Code, "FUNNYPIPE")) { p.Quantity = (p.Quantity / 20 + (p.Quantity % 20 > 0 ? 1 : 0)) * 20; } });
			}

			if (State.ShowExternalCodes)
			{
				// Put the external code and description into the internal code and description if exists so that they can be
				// accessed by the parts list.
				neededParts.ForEach(p =>
				{
					if (p.ExternalPart != null && !string.IsNullOrEmpty(p.ExternalPart.Code))
					{
						p.Code = p.ExternalPart.Code;
						p.Description = p.ExternalPart.Description;
					}
				});
			}

			return neededParts;
		}

		public double GetArea()
		{
			List<IShape> shapes = (from s in Selected<TNTObject>() where s is IShape select s as IShape).ToList();
			double area = 0;

			shapes.ForEach(p => area += p.GetArea());

			return area;
		}

		public double GetLength()
		{
			List<IMeasurable> measurables = (from s in Selected<TNTObject>() where s is IMeasurable select s as IMeasurable).ToList();
			double length = 0;

			measurables.ForEach(p => length += p.GetLength());

			return length;
		}

		public bool Save(bool showSaveFileDialog)
		{
			bool wasSaved = false;

			if (!string.IsNullOrEmpty(CurrentFileName))
			{
				m_SaveFileDialog.FileName = Path.GetFileNameWithoutExtension(CurrentFileName);
			}
			else
			{
				m_SaveFileDialog.FileName = State.Settings.ToString();
			}

			string content = Serialize();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(content);

			try
			{
				if ((showSaveFileDialog || string.IsNullOrEmpty(CurrentFileName)) && m_SaveFileDialog.ShowDialog() == DialogResult.OK)
				{
					CurrentFileName = m_SaveFileDialog.FileName;
				}

				if (!string.IsNullOrEmpty(CurrentFileName))
				{
					if (Path.GetExtension(CurrentFileName) == ".lsd")
					{
						doc.Save(CurrentFileName);
						wasSaved = true;
					}
					else if (Path.GetExtension(CurrentFileName) == ".lsdx")
					{
						using (ZipFile zipFile = new ZipFile(CurrentFileName))
						{
							if (zipFile["LSD.xml"] != null)
							{
								zipFile.RemoveEntry("LSD.xml");
							}
							zipFile.AddEntry("LSD.xml", doc.InnerXml);
							zipFile.Save();
						}
						wasSaved = true;
					}
					else
					{
						SaveAsImage(m_SaveFileDialog.FileName);
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				wasSaved = Save(true);
			}

			if (wasSaved)
			{
				ClearUndoQueue();
			}

			return wasSaved;
		}

		public void Open(string fileName)
		{
			if (!File.Exists(fileName))
			{
				MessageBox.Show("The file you are attempting to open no longer exists. Could it have been renamed or moved?", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			XmlDocument doc = new XmlDocument();

			CurrentFileName = fileName;

			if (Path.GetExtension(CurrentFileName) == ".lsd")
			{
				doc.Load(CurrentFileName);
			}
			else if (Path.GetExtension(CurrentFileName) == ".lsdx")
			{
				using (ZipFile zipFile = ZipFile.Read(CurrentFileName))
				{
					ZipEntry zipEntry = zipFile["LSD.xml"];

					using (var ms = new MemoryStream())
					using (var sr = new StreamReader(ms))
					{
						zipEntry.Extract(ms);
						ms.Position = 0;
						var myStr = sr.ReadToEnd();
						doc.LoadXml(myStr);
					}
				}
			}

			doc = ApplyTransformations(doc);
			Deserialize(doc.InnerXml);
		}

		#endregion
	}
}
