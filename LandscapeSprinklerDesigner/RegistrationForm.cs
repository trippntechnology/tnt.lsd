using LandscapeSprinklerDesigner.Service;
using LandscapeSprinklerDesigner.Utils;
using System.Text;
using TNT.Cryptography;
using TNT.Services.Models.Dto;

namespace LandscapeSprinklerDesigner;


public partial class RegistrationForm : Form
{
  public RegistrationForm()
  {
    InitializeComponent();
    System.Windows.Forms.Application.Idle += Application_Idle!;
    this.ActiveControl = panelDetail;
  }

  public new DialogResult ShowDialog(IWin32Window owner)
  {
    if (base.ShowDialog(owner) == System.Windows.Forms.DialogResult.OK)
    {
      try
      {
        FileUtil.SaveRegistrationKey(RegistrationKey.Lines.ToList());
        var licenseeInfo = TntService.GetLicenseeInfo();

        if (licenseeInfo != null)
        {
          FileUtil.SaveLicenseInfo(licenseeInfo);

          var msg = new StringBuilder();
          msg.AppendLine("Successfully Registered");
          msg.AppendLine($"To: {licenseeInfo.Name}");
          msg.AppendLine($"Until: {licenseeInfo.ValidUntil.ToLocalTime().DateTime.ToString()}");
          MessageBox.Show(owner, msg.ToString(), "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
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
    var lines = RegistrationKey.Lines?.Where(l => !String.IsNullOrEmpty(l)) ?? new List<string>();
    var firstLine = lines.FirstOrDefault() ?? string.Empty;
    var lastLine = lines.LastOrDefault() ?? string.Empty;
    RegisterButton.Enabled = firstLine == FormatUtils.BEGIN_TAG && lastLine == FormatUtils.END_TAG;
  }

  private void SetLicenseDetails(LicenseeInfoDto? licenseeInfo)
  {
    labelIssuedTo.BeginInvoke(delegate
    {
      labelIssuedTo.Text = licenseeInfo?.Name ?? string.Empty;
      labelValidUntil.Text = licenseeInfo?.ValidUntil.DateTime.ToLocalTime().ToString() ?? string.Empty;
    });
  }

  private void RegistrationForm_Load(object sender, EventArgs e)
  {
    TntService.GetLicenseeInfoFlow().collect(SetLicenseDetails);
  }
}
