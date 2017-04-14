using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner
{
	partial class AboutBox : Form
	{
		public AboutBox()
		{
			InitializeComponent();
			this.Text = String.Format("About {0}", AssemblyTitle);
			this.VersionLabel.Text = String.Format("Version {0}", AssemblyVersion);
			this.CopyrightLabel.Text = AssemblyCopyright;
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle { get { return Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly()).Title; } }
		public string AssemblyVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
		public string AssemblyDescription { get { return Utilities.GetAssemblyAttribute<AssemblyDescriptionAttribute>(Assembly.GetExecutingAssembly()).Description; } }
		public string AssemblyProduct { get { return Utilities.GetAssemblyAttribute<AssemblyProductAttribute>(Assembly.GetExecutingAssembly()).Product; } }
		public string AssemblyCopyright { get { return Utilities.GetAssemblyAttribute<AssemblyCopyrightAttribute>(Assembly.GetExecutingAssembly()).Copyright; } }
		public string AssemblyCompany { get { return Utilities.GetAssemblyAttribute<AssemblyCompanyAttribute>(Assembly.GetExecutingAssembly()).Company; } }

		#endregion

		private void AboutBox_Load(object sender, EventArgs e)
		{
			string[] files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll", SearchOption.AllDirectories);
			Assembly asm = null;

			foreach (string file in files)
			{
				try
				{
					asm = Assembly.LoadFile(file);

					ListViewItem item = listView1.Items.Add(Path.GetFileName(file));

					if (asm != null)
					{
						item.SubItems.Add(asm.GetName().Version.ToString());

						AssemblyCopyrightAttribute assCopyAttr = Utilities.GetAssemblyAttribute<AssemblyCopyrightAttribute>(asm);
						if (assCopyAttr != null)
						{
							item.SubItems.Add(assCopyAttr.Copyright);
						}
						else
						{
							item.SubItems.Add(string.Empty);
						}
					}
				}
				catch { }
			}
		}
	}
}
