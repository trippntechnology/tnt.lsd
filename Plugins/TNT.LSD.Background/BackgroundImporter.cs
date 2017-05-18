using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LSDComponents;
using TNT.Utilities;
using Microsoft.Win32;
using System.IO;

namespace TNT.LSD.Background
{
	public partial class BackgroundImporter : Form
	{
		protected ApplicationRegistry m_ApplicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "BackgroundPlugin");

		public BackgroundImporter()
		{
			InitializeComponent();

			Application.Idle += Application_Idle;
		}

		public DialogResult ShowDialog(IWin32Window owner, TNTCADState state)
		{
			FindFileButton_Click(null, null);
			DialogResult result = base.ShowDialog(owner);

			if (result == System.Windows.Forms.DialogResult.OK)
			{
				double pixelsPerFoot = Convert.ToDouble(PixelPerFootTextBox.Text);
				state.BackgroundImage = new Bitmap(FileNameTextBox.Text);
				state.WidthInFeet = (int)(state.BackgroundImage.Width / pixelsPerFoot);
				state.HeightInFeet = (int)(state.BackgroundImage.Height / pixelsPerFoot);
			}

			return result;
		}

		private void FindFileButton_Click(object sender, EventArgs e)
		{
			FindFileDialog.InitialDirectory = m_ApplicationRegistry.ReadString("InitialDirectory", string.Empty);

			if (FindFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Regex regex = new Regex("_(?<pixels_per_foot>[0-9]*.[0-9]*)");
				FileNameTextBox.Text = FindFileDialog.FileName;

				Match match = regex.Match(FindFileDialog.FileName);

				if (match.Success)
				{
					PixelPerFootTextBox.Text = match.Groups["pixels_per_foot"].ToString();
				}

				m_ApplicationRegistry.WriteString("InitialDirectory", Path.GetDirectoryName(FindFileDialog.FileName));
			}
		}

		private void Application_Idle(Object sender, EventArgs e)
		{
			bool isDouble = false;

			try
			{
				Convert.ToDouble(PixelPerFootTextBox.Text);
				isDouble = true;
			}
			catch
			{
			}

			ImportButton.Enabled = !string.IsNullOrEmpty(FileNameTextBox.Text) && isDouble;
		}
	}
}
