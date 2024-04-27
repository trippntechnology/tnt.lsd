using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using TNT.LSD.Inventory;
using TNT.LSD.Inventory.DAL;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace TNT.LSD.Components.DrawingModes;

public class SelectMode : DrawingMode
{
  private bool m_AllowDrag = false;
  private bool m_PrepareUndo = false;
  private Rectangle m_SelectionRectangle = Rectangle.Empty;
  private Point m_SelectionBandAnchor = Point.Empty;
  private TNTObject m_UnderlaidObject = null;
  private ToolTip m_ToolTip = new ToolTip();
  private bool m_ShowToolTip = true;

  private Point m_LastMousePosition = Point.Empty;

  public SelectMode()
    : base()
  {
    //m_ToolTip.AutomaticDelay = 5000;  // 500
    //m_ToolTip.AutoPopDelay = 50000;  // 5000
    //m_ToolTip.InitialDelay = 5000;  // 500
    m_ToolTip.IsBalloon = true;  // false
                                 //m_ToolTip.ReshowDelay = 1000;  // 100
                                 //m_ToolTip.ShowAlways = true; // false
                                 //m_ToolTip.ToolTipIcon = ToolTipIcon.Info; // None
                                 //m_ToolTip.ToolTipTitle = "ToolTipTitle"; // string.Empty
                                 //m_ToolTip.UseAnimation = false; // true
                                 //m_ToolTip.UseFading = false;  // true
  }

  public SelectMode(SelectMode obj)
    : base(obj)
  {
    m_ToolTip.IsBalloon = obj.m_ToolTip.IsBalloon;
  }

  public override DrawingMode Clone()
  {
    return new SelectMode(this);
  }

  public override void OnMouseMove(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
  {
    m_UnderlaidObject = null;
    Point currentPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), false);

    if (m_AllowDrag && cad.SelectedObjects.Count > 0 && e.Button == MouseButtons.Left)
    {
      #region Move selected objects

      // Make sure the tool tip is hidden when moving
      m_ToolTip.Hide(cad);
      m_ShowToolTip = true;

      if (m_AllowDrag && m_PrepareUndo)
      {
        cad.PrepareToUndoSelected();
      }

      m_PrepareUndo = false;

      foreach (TNTObject obj in cad.SelectedObjects)
      {
        obj.Move(currentPos, m_LastMousePosition, cad.SnapToGrid, false, modifierKeys);
      }

      cad.Refresh();

      #endregion
    }
    else if ((cad.SelectedObjects.Count == 0 || (modifierKeys & Keys.Control) == Keys.Control) && e.Button == MouseButtons.Left)
    {
      #region Draw Selection Rectangle

      Graphics g = cad.CreateGraphics();

      g.SmoothingMode = SmoothingMode.AntiAlias;
      g.ResetTransform();
      g.ScaleTransform((float)(cad.DisplayScale / 100.0), (float)(cad.DisplayScale / 100.0));

      cad.Refresh();

      SolidBrush b = new SolidBrush(Color.FromArgb(50, Color.Turquoise));
      Pen p = new Pen(Color.DarkTurquoise);
      p.Width = 2;

      m_SelectionRectangle = cad.CreateRectangle(m_SelectionBandAnchor, currentPos);
      g.FillRectangle(b, m_SelectionRectangle);
      g.DrawRectangle(p, m_SelectionRectangle);

      #endregion
    }
    else
    {
      List<TNTObject> orderedList = new List<TNTObject>();

      // Selected objects that aren't pipe
      orderedList.AddRange((from s in cad.Selected<TNTObject>() where !(s is Pipe) && s.Selected select s).ToList());

      // Non-selected objects that aren't pipe
      orderedList.AddRange((from o in cad.ActiveObjects where !(o is Pipe) && !o.Selected select o).ToList());

      // Pipe objects
      orderedList.AddRange((from o in cad.ActiveObjects where o is Pipe select o).ToList());

      foreach (TNTObject obj in orderedList)
      {
        if (obj.MouseOver(currentPos, modifierKeys) != null)
        {
          m_UnderlaidObject = obj;
          break;
        }
      }

      if (m_UnderlaidObject != null)
      {
        cad.SetStateInfo(m_UnderlaidObject.MouseOver(currentPos, modifierKeys).GetStateInfo(currentPos, modifierKeys));

        if (!cad.ShowPartsToolTip || !m_UnderlaidObject.Selected)
        {
          m_ToolTip.SetToolTip(cad, string.Empty);
        }
        else if (cad.ShowPartsToolTip && m_ShowToolTip && m_UnderlaidObject is BasePart)
        {
          var parts = DALPart.GetParts();
          (m_UnderlaidObject as BasePart).SetPartQuantity(parts, cad.Settings.SystemType);
          StringBuilder sb = new StringBuilder();
          List<Part> partsList = (from p in parts where p.Value.Quantity > 0 select p.Value).ToList();

          foreach (Part part in partsList)
          {
            sb.AppendFormat("({0}) {1}\n", part.Quantity, part.Description);
          }

          Point ttPosition = (m_UnderlaidObject as BasePart).Position.ToPageCoordinateSpace(cad.CreateGraphics(), true);

          // KLUGE: By adding the next to line it kept the width correct.
          m_ToolTip.SetToolTip(cad, sb.ToString());
          m_ShowToolTip = false;
          m_ToolTip.Show(sb.ToString(), cad, ttPosition);
        }
      }
      else
      {
        cad.Cursor = TNTCursors.Default;
        cad.Text = string.Empty;

        m_ToolTip.Hide(cad);
        m_ShowToolTip = true;
      }
    }

    m_LastMousePosition = currentPos;
  }

  public override void OnMouseUp(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
  {
    // Select anything with the selection band
    if (m_SelectionBandAnchor != Point.Empty)
    {
      m_SelectionBandAnchor = Point.Empty;

      foreach (TNTObject obj in cad.m_State.ObjectLayers[cad.ActiveLayer])
      {
        if (obj.InRectangle(m_SelectionRectangle))
        {
          obj.Selected = true;
        }
      }

      cad.DrawLayers(1);
    }

    // Find out which selected object mouse is over and call MouseUp event on object
    foreach (TNTObject obj in cad.SelectedObjects)
    {
      if (obj.MouseOver(m_LastMousePosition, modifierKeys) != null)
      {
        Point adjPoint = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);
        obj.MouseUp(adjPoint, (modifierKeys & Keys.Control) > 0, (modifierKeys & Keys.Shift) > 0);
        cad.SetStateInfo(obj.GetStateInfo(m_LastMousePosition, modifierKeys));
        break;
      }
    }

    cad.TriggerOnObjectsSelected();

    m_AllowDrag = false;
    m_PrepareUndo = false;
  }

  public override void OnMouseDown(TNTCAD cad, MouseEventArgs e, Keys modifierKeys)
  {
    m_SelectionRectangle = Rectangle.Empty;

    if (e.Button == MouseButtons.Left)
    {
      m_LastMousePosition = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), false);

      if (m_UnderlaidObject != null)
      {
        m_UnderlaidObject.MouseDown(m_LastMousePosition, modifierKeys);

        if (modifierKeys == Keys.Control)
        {
          m_UnderlaidObject.Selected = !m_UnderlaidObject.Selected;
        }
        else if (modifierKeys == Keys.Shift)
        {
          cad.Unselect(cad.SelectedObjects.FindAll(o => o.GetType() != m_UnderlaidObject.GetType()));
        }
        else if (m_UnderlaidObject.Selected)
        {
          if (m_UnderlaidObject.OverControlPoint)
          {
            cad.UnselectAll(m_UnderlaidObject);
          }
        }
        else
        {
          cad.UnselectAll();
          m_UnderlaidObject.Selected = true;
        }
      }
      else
      {
        if ((modifierKeys & Keys.Control) == Keys.None)
        {
          cad.UnselectAll();
        }

        // Start the selection band
        m_SelectionBandAnchor = m_LastMousePosition;
      }

      if (m_UnderlaidObject != null)
      {
        TNTObject obj = m_UnderlaidObject.MouseOver(m_LastMousePosition, modifierKeys);

        if (obj != null)
        {
          cad.SetStateInfo(obj.GetStateInfo(m_LastMousePosition, modifierKeys));
        }
      }

      // Draw all object layers
      cad.DrawLayers(1);
    }

    m_AllowDrag = m_UnderlaidObject != null ? m_UnderlaidObject.MouseOver(m_LastMousePosition, modifierKeys) != null : false;
    m_PrepareUndo = m_AllowDrag;
  }

  public override void OnKeyDown(TNTCAD cad, KeyEventArgs e)
  {
    if (m_UnderlaidObject != null)
    {
      cad.SetStateInfo(m_UnderlaidObject.GetStateInfo(m_LastMousePosition, e.Modifiers));
    }
  }

  public override void OnKeyUp(TNTCAD cad, KeyEventArgs e)
  {
    if (m_UnderlaidObject != null)
    {
      cad.SetStateInfo(m_UnderlaidObject.GetStateInfo(m_LastMousePosition, e.Modifiers));
    }
    else
    {
      cad.Cursor = Cursors.Arrow;
      cad.Text = string.Empty;
    }
  }
}
