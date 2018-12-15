using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace LSDComponents.DrawingModes
{
	public abstract class PipeMode<T> : DrawingMode where T : Pipe, new()
	{
		protected List<UndoAction> m_UndoActionList = null;
		protected Pipe m_PipeSegment = null;

		protected Cursor PipeCursor { get { return typeof(T) == typeof(LateralPipe) ? TNTCursors.LatPipe : TNTCursors.MainPipe; } }
		protected Cursor NoPipeCursor { get { return typeof(T) == typeof(LateralPipe) ? TNTCursors.NoLatPipe : TNTCursors.NoMainPipe; } }

		public PipeMode()
			: base()
		{
			DefaultObject = new T();
			DefaultObjectType = DefaultObject.GetType().ToString();
		}

		public PipeMode(PipeMode<T> obj)
			: base(obj)
		{
		}

		#region Overridden methods

		public override void OnMouseMove(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
		{
			base.OnMouseMove(cad, e, modifierKeys);

			Point currentPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), false);

			if (m_PipeSegment != null)
			{
				currentPos = currentPos.SnapToGrid(cad.SnapToGrid);
				m_PipeSegment.Part2.MoveTo(currentPos.X, currentPos.Y);
				cad.Refresh();
			}

			BasePart objUnderMouse = GetObjectUnderMouse(cad, e, modifierKeys) as BasePart;
			string reason = string.Empty;
			bool canAddPipe = false;

			if (objUnderMouse != null)
			{
				canAddPipe = objUnderMouse.CanAddPipe(typeof(T), out reason);
			}

			if (m_PipeSegment !=null && objUnderMouse != null && m_PipeSegment.Part1 == objUnderMouse)
			{
				cad.Cursor = NoPipeCursor;
				cad.Text = "Cannot connect head to itself";
			}
			else if (m_PipeSegment != null && objUnderMouse != null && canAddPipe)
			{
				cad.Cursor = PipeCursor;
				cad.Text = "Left click to connect pipe segment. Right click to remove current pipe segment.";
			}
			else if (objUnderMouse != null)
			{
				if (objUnderMouse is TNTPart && canAddPipe)
				{
					cad.Cursor = PipeCursor;
					cad.Text = "Click to begin pipe segment";
				}
				else if (!string.IsNullOrEmpty(reason))
				{
					cad.Cursor = NoPipeCursor;
					cad.Text = reason;
				}
				else
				{
					cad.Cursor = NoPipeCursor;
					cad.Text = "Click on part or fitting to begin pipe segment";//Start pipe segment at part or fitting to connect to existing pipe";
				}
			}
			else if (m_PipeSegment != null && objUnderMouse == null)
			{
				cad.Cursor = PipeCursor;
				cad.Text = "Left click to add fitting. Right click to remove current pipe segment.";
			}
			else
			{
				cad.Cursor = NoPipeCursor;
				cad.Text = "Click on part or fitting to begin pipe segment";
			}
		}

		public override void OnMouseClick(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
		{
			base.OnMouseDown(cad, e, modifierKeys);

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				Point mousePosition = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), false);
				BasePart objUnderMouse = GetObjectUnderMouse(cad, e, modifierKeys) as BasePart;
				string reason = string.Empty;
				bool canAddPipe = false;

				if (objUnderMouse != null)
				{
					canAddPipe = objUnderMouse.CanAddPipe(typeof(T), out reason);

					if (!canAddPipe)
					{
						// Do nothing
					}
					else if (objUnderMouse is TNTPart && m_PipeSegment == null)
					{
						TNTPart partUnderMouse = objUnderMouse as TNTPart;

						// Create a new Pipe segment and added it to layer to be drawn
						m_PipeSegment = new T();
						m_PipeSegment.Assign(DefaultObject);

						m_PipeSegment.Selected = true;
						cad.ActiveObjects.Add(m_PipeSegment);

						// Create a fitting for the mouse location
						Fitting newFitting = new Fitting(mousePosition);

						m_PipeSegment.CreatePartsPipeRelationship(partUnderMouse, newFitting);
					}
					else if (m_PipeSegment != null)
					{
						// Get Part1
						TNTPart part1 = m_PipeSegment.Part1;

						// Get Part2 (the fitting)
						Fitting fitting = m_PipeSegment.Part2 as Fitting;

						// Find all parts that are along the pipe path
						List<TNTObject> partsAlongPath = cad.ActiveObjects.FindAll(o =>
						{
							return o is LateralPart && o != objUnderMouse && o != part1 && m_PipeSegment.MouseOver(o.ControlPoints.First().Position, new Keys()) == m_PipeSegment;
						});

						// Order closest to farthest from part1
						partsAlongPath = (from cp in partsAlongPath orderby (cp as TNTPart).Position.Distance(part1.Position) select cp).ToList();

						// This is done to restore the parts to the previous state so that an uaModify event can be created
						m_PipeSegment.RemovePartsPipeRelationship();

						m_UndoActionList = new List<UndoAction>();

						m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, part1, cad.ActiveObjects));

						if (objUnderMouse is TNTPart)
						{
							#region Connect to part

							part1 = AddPartsToPath(cad, part1, partsAlongPath);

							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, objUnderMouse, cad.ActiveObjects));
							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaDelete, m_PipeSegment, cad.ActiveObjects));

							m_PipeSegment.Selected = false;

							m_PipeSegment.CreatePartsPipeRelationship(part1, objUnderMouse as TNTPart);

							if (objUnderMouse.TerminatePipe())
							{
								m_PipeSegment = null;
							}
							else
							{
								m_PipeSegment = new T();
								m_PipeSegment.Assign(DefaultObject);

								m_PipeSegment.Selected = true;

								m_PipeSegment.CreatePartsPipeRelationship(objUnderMouse as TNTPart, fitting);

								cad.ActiveObjects.Add(m_PipeSegment);
							}

							#endregion
						}
						else if (objUnderMouse is Pipe)
						{
							#region Connect to pipe

							Pipe pipe = objUnderMouse as Pipe;
							TNTPart pipePart1 = pipe.Part1;
							TNTPart pipePart2 = pipe.Part2;

							part1 = AddPartsToPath(cad, part1, partsAlongPath);

							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, pipe, cad.ActiveObjects));
							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, pipePart1, cad.ActiveObjects));
							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, pipePart2, cad.ActiveObjects));
							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaDelete, m_PipeSegment, cad.ActiveObjects));

							pipe.RemovePartsPipeRelationship();

							m_PipeSegment.CreatePartsPipeRelationship(part1, fitting);
							pipe.CreatePartsPipeRelationship(pipePart1, fitting);

							Pipe newPipe = new T();
							newPipe.Assign(DefaultObject);

							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaDelete, newPipe, cad.ActiveObjects));
							cad.ActiveObjects.Add(newPipe);
							newPipe.CreatePartsPipeRelationship(fitting, pipePart2);

							m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaDelete, fitting, cad.ActiveObjects));
							cad.ActiveObjects.Insert(0, fitting);

							m_PipeSegment.Selected = false;

							m_PipeSegment = null;

							#endregion
						}

						cad.UndoActions.Push(m_UndoActionList);
					}
				}
				else if (m_PipeSegment != null)
				{
					m_UndoActionList = new List<UndoAction>();

					// Get Part1
					TNTPart part1 = m_PipeSegment.Part1;

					// Get Part2 (the fitting)
					Fitting fitting = m_PipeSegment.Part2 as Fitting;

					// Find all parts that are along the pipe path
					List<TNTObject> partsAlongPath = cad.ActiveObjects.FindAll(o =>
					{
						return o is LateralPart && o != objUnderMouse && o != part1 && m_PipeSegment.MouseOver(o.ControlPoints.First().Position, new Keys()) == m_PipeSegment;
					});

					// Order closest to farthest from part1
					partsAlongPath = (from cp in partsAlongPath orderby (cp as TNTPart).Position.Distance(part1.Position) select cp).ToList();

					// This is done to restore the parts to the previous state so that an uaModify event can be created
					m_PipeSegment.RemovePartsPipeRelationship();

					m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, part1, cad.ActiveObjects));

					part1 = AddPartsToPath(cad, part1, partsAlongPath);

					m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaDelete, m_PipeSegment, cad.ActiveObjects));

					m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaDelete, fitting, cad.ActiveObjects));
					cad.ActiveObjects.Insert(0, fitting);

					m_PipeSegment.CreatePartsPipeRelationship(part1, fitting);

					m_PipeSegment.Selected = false;

					m_PipeSegment = new T();
					m_PipeSegment.Assign(DefaultObject);
					m_PipeSegment.Selected = true;

					m_PipeSegment.CreatePartsPipeRelationship(fitting, new Fitting(mousePosition));

					cad.ActiveObjects.Add(m_PipeSegment);

					cad.UndoActions.Push(m_UndoActionList);
				}
			}
			else if (e.Button == System.Windows.Forms.MouseButtons.Right && m_PipeSegment != null)
			{
				m_PipeSegment.RemovePartsPipeRelationship();
				cad.ActiveObjects.Remove(m_PipeSegment);

				m_PipeSegment = null;
			}

			UndoEnabled = m_PipeSegment == null;

			cad.DrawLayers(2);
		}

		private TNTPart AddPartsToPath(TNTCAD cad, TNTPart part1, List<TNTObject> partsAlongPath)
		{
			// Add all the parts along the path
			foreach (LateralPart part2 in partsAlongPath)
			{
				m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaModify, part2, cad.ActiveObjects));
				m_UndoActionList.Add(new UndoAction(UndoAction.UndoActionType.uaDelete, m_PipeSegment, cad.ActiveObjects));

				m_PipeSegment.Selected = false;
				m_PipeSegment.CreatePartsPipeRelationship(part1, part2);

				m_PipeSegment = new T();
				m_PipeSegment.Assign(DefaultObject);
				cad.ActiveObjects.Add(m_PipeSegment);

				part1 = part2;
			}

			return part1;
		}

		public override void OnMouseDoubleClick(TNTCAD cad, System.Windows.Forms.MouseEventArgs e)
		{
			if (m_PipeSegment != null)
			{
				m_PipeSegment.RemovePartsPipeRelationship();
				cad.ActiveObjects.Remove(m_PipeSegment);
			}

			m_PipeSegment = null;
		}

		public override void Reset(TNTCAD cad)
		{
			base.Reset(cad);

			if (m_PipeSegment != null)
			{
				cad.ActiveObjects.Remove(m_PipeSegment);
				cad.Undo();
				m_PipeSegment = null;
			}
		}

		/// <summary>
		/// Added to handle the case when the s key is pressed
		/// </summary>
		/// <param name="cad">CAD</param>
		/// <param name="e">Key event argss</param>
		public override void OnKeyDown(TNTCAD cad, KeyEventArgs e)
		{
			base.OnKeyDown(cad, e);

			// This is added for the case when the s key is pressed and the mode changes to select mode. Without it,
			// a the previously added pipe segment is lost.
			if (e.KeyCode == Keys.S)
			{
				OnMouseClick(cad, new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0), new Keys());
			}
		}

		#endregion

		/// <summary>
		/// Returns the object under the mouse position
		/// </summary>
		/// <param name="cad">TNTCAD object</param>
		/// <param name="e">Mouse event arguements</param>
		/// <param name="modifierKeys">Modifier keys</param>
		/// <returns>Object under the mouse if exist, null otherwise</returns>
		virtual protected TNTObject GetObjectUnderMouse(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
		{
			Point currentPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), false);

			List<TNTObject> orderedList = new List<TNTObject>();

			// Selected objects that aren't pipe
			orderedList.AddRange((from s in cad.Selected<TNTObject>() where !(s is Pipe) && s.Selected select s).ToList());

			// Non-selected objects that aren't pipe
			orderedList.AddRange((from o in cad.ActiveObjects where !(o is Pipe) && !o.Selected select o).ToList());

			// Pipe objects
			orderedList.AddRange((from o in cad.ActiveObjects where o is Pipe && !o.Equals(m_PipeSegment) select o).ToList());

			foreach (TNTObject obj in orderedList)
			{
				if (obj.MouseOver(currentPos, modifierKeys) != null)
				{
					return obj;
				}
			}

			return null;
		}
	}
}
