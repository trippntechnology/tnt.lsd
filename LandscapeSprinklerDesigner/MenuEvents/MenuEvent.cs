using System.Reflection;
using TNT.LSD.Components;
using TNT.ToolStripItemManager;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

abstract class MenuEvent(string text, string? toolTipText = null, bool checkOnClick = false) : ToolStripItemGroup(text, toolTipText, checkOnClick)
{
    virtual protected string AssemblyTitle
    {
        get
        {
            AssemblyTitleAttribute? ata = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly());
            return ata != null ? ata.Title : string.Empty;
        }
    }

    virtual protected bool HandleUnsavedChanges(TNTCAD cad)
    {
        bool handled = true;

        cad.Repaint();

        if (cad.HasUnsavedChanges)
        {
            string msg = string.Format("The layout \"{0}\" has been modified.\nDo you want to save your changes?", string.IsNullOrEmpty(cad.CurrentFileName) ? "Untitled" : Path.GetFileName(cad.CurrentFileName));
            DialogResult dr = MessageBox.Show(msg, AssemblyTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (dr)
            {
                case DialogResult.Yes:
                    handled = cad.Save(false);
                    break;
                case DialogResult.No:
                    break;
                default:
                    handled = false;
                    break;
            }
        }

        return handled;
    }

    public virtual void OnApplicationIdle(TNTCAD cad) { }

    public virtual void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings) { }
}
