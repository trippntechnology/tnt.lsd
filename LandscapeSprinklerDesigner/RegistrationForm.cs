using System.ComponentModel;
using System.Text;

namespace LandscapeSprinklerDesigner
{
  public partial class RegistrationForm : Form
  {
    private const string BEGIN = "--- BEGIN ---";
    private const string END = "--- END ---";

    public RegistrationForm()
    {
      InitializeComponent();
      System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
      this.ActiveControl = panelDetail;
    }

    public new DialogResult ShowDialog(IWin32Window owner)
    {
      var license = Global.GetLicense();
      SetLicenseDetails(license);

      DialogResult result = base.ShowDialog(owner);

      if (result == System.Windows.Forms.DialogResult.OK)
      {
        try
        {
          license = Global.SetLicense(LicenseText.Lines.ToList());

          var msg = new StringBuilder();
          msg.AppendLine("Successfully Registered");
          //msg.AppendLine($"To: {license.IssuedTo}");
          //msg.AppendLine($"Until: {license.ExpiresOn}");
          MessageBox.Show(owner, msg.ToString(), "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
          MessageBox.Show(owner, ex.Message, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }

      return DialogResult;
    }

    protected void Application_Idle(object sender, EventArgs e)
    {
      var lines = LicenseText.Lines?.Where(l => !String.IsNullOrEmpty(l)) ?? new List<string>();
      var firstLine = lines.FirstOrDefault() ?? string.Empty;
      var lastLine = lines.LastOrDefault() ?? string.Empty;

      RegisterButton.Enabled = firstLine == BEGIN && lastLine == END;
    }

    private void LicenseText_TextChanged(object sender, EventArgs e)
    {
      try
      {
        var license = Global.Decrypt(LicenseText.Lines.ToList());
        SetLicenseDetails(license);
      }
      catch { SetLicenseDetails(null); }
    }

    private void SetLicenseDetails(TNT.LSD.Settings.License license)
    {
      //if (license != null)
      //{
      //	labelIssuedTo.Text = license.IssuedTo;
      //	labelValidUntil.Text = license.ExpiresOn.ToString();
      //}
      //else
      //{
      //	labelIssuedTo.Text = string.Empty;
      //	labelValidUntil.Text = string.Empty;
      //}
    }

    private void propertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
    {
      var propertyGrid = sender as PropertyGrid;

      if (propertyGrid.SelectedObject != null)
      {
        TypeDescriptor.AddAttributes(propertyGrid.SelectedObject, new Attribute[] { new ReadOnlyAttribute(true) });
        propertyGrid.Refresh();
      }
    }
  }
}
