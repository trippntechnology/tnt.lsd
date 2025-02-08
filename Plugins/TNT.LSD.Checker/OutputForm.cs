using System.Drawing;
using TNT.LSD.Components;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.Checker;

public partial class OutputForm : DockContent
{
  public OutputForm()
  {
    InitializeComponent();
  }

  internal void Show(ApplicationData applicationData, DockState document)
  {
    Show(applicationData.DockPanel, document);

    appendText("Blue", Color.Blue);
    appendText("Yellow", Color.Yellow);
    appendLine("Default");
    appendLine("Red", Color.Red);
    appendLine("Default bold", fontStyle: FontStyle.Bold);

  }

  private void appendText(string text, Color? color = null, FontStyle fontStyle = FontStyle.Regular)
  {
    Color selectionColor = color ?? console.ForeColor;

    console.SelectionStart = console.TextLength;
    console.SelectionLength = 0;

    console.SelectionColor = selectionColor;
    console.SelectionFont = new Font(console.Font, fontStyle);
    console.AppendText(text);
    console.SelectionColor = console.ForeColor;
  }

  private void appendLine(string text, Color? color = null, FontStyle fontStyle = FontStyle.Regular)
  {
    appendText(text, color, fontStyle);
    appendText(Environment.NewLine);
  }
}
