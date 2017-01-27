using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TNT.Utilities;
using System.IO;

namespace LandscapeSprinklerDesigner
{
	partial class AboutBox : Form
	{
		public AboutBox()
		{
			InitializeComponent();
			this.Text = String.Format("About {0}", AssemblyTitle);
			//this.labelProductName.Text = AssemblyProduct;
			this.VersionLabel.Text = String.Format("Version {0}", AssemblyVersion);
			this.CopyrightLabel.Text = AssemblyCopyright;
			//this.labelCompanyName.Text = AssemblyCompany;
			//this.textBoxDescription.Text = AssemblyDescription;
		}

		#region Assembly Attribute Accessors

		public string AssemblyTitle { get { return Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly()).Title; } }
		public string AssemblyVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
		public string AssemblyDescription { get { return Utilities.GetAssemblyAttribute<AssemblyDescriptionAttribute>(Assembly.GetExecutingAssembly()).Description; } }
		public string AssemblyProduct { get { return Utilities.GetAssemblyAttribute<AssemblyProductAttribute>(Assembly.GetExecutingAssembly()).Product; } }
		public string AssemblyCopyright { get { return Utilities.GetAssemblyAttribute<AssemblyCopyrightAttribute>(Assembly.GetExecutingAssembly()).Copyright; } }
		public string AssemblyCompany { get { return Utilities.GetAssemblyAttribute<AssemblyCompanyAttribute>(Assembly.GetExecutingAssembly()).Company; } }

		#endregion

		private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
		{

		}

		private void AboutBox_Load(object sender, EventArgs e)
		{
			string[] files = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll");
			Assembly asm = null;

			foreach (string file in files)
			{
				asm = Assembly.LoadFile(file);

				ListViewItem item = listView1.Items.Add(Path.GetFileName(file));

				AssemblyFileVersionAttribute fileVersionAttr = Utilities.GetAssemblyAttribute<AssemblyFileVersionAttribute>(asm);
				if (fileVersionAttr != null)
				{
					item.SubItems.Add(fileVersionAttr != null ? fileVersionAttr.Version : asm.GetName().Version.ToString());
				}
				else
				{
					item.SubItems.Add(string.Empty);
				}

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
	}
}
