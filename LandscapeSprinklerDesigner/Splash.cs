using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LandscapeSprinklerDesigner
{
	public partial class Splash : Form
	{
		private string m_Version = string.Empty;
		private string m_Status = string.Empty;

		public string Status { get { return m_Status; } set { m_Status = value; UpdateFormDisplay(BackgroundImage); } }

		public Splash()
		{
			InitializeComponent();

			using (Stream s = this.GetType().Assembly.GetManifestResourceStream("LandscapeSprinklerDesigner.splash.png"))
			{
				BackgroundImage = new Bitmap(s);
			}

			Width = BackgroundImage.Width;
			Height = BackgroundImage.Height;

			Assembly asm = Assembly.GetEntryAssembly();
			m_Version = asm.GetName().Version.ToString();
		}

		public void ShowAsSplash()
		{
			timer.Enabled = true;
			Show();
		}

		public new DialogResult ShowDialog()
		{
			return base.ShowDialog();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			timer.Enabled = false;
			this.Click += new EventHandler(Splash_Click);
			Hide();
		}

		private void Splash_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			Hide();
		}

		#region CUSTOM PAINT METHODS

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00080000; // Required: set WS_EX_LAYERED extended style
				return cp;
			}
		}

		//Updates the Form's display using API calls
		public void UpdateFormDisplay(Image backgroundImage)
		{
			Graphics g = Graphics.FromImage(backgroundImage);
			Rectangle versionRect = new Rectangle(275, 150, 150, 30);
			Rectangle statusRect = new Rectangle(25, 328, 256, 30);
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;
			g.DrawString(m_Version, Font, new SolidBrush(Color.FromArgb(50, Color.Black)), new Rectangle(versionRect.Left + 4, versionRect.Top + 4, versionRect.Width, versionRect.Height), format);
			g.DrawString(m_Version, Font, new SolidBrush(ForeColor), versionRect, format);

			IntPtr screenDc = API.GetDC(IntPtr.Zero);
			IntPtr memDc = API.CreateCompatibleDC(screenDc);
			IntPtr hBitmap = IntPtr.Zero;
			IntPtr oldBitmap = IntPtr.Zero;

			try
			{
				//Display-image
				Bitmap bmp = new Bitmap(backgroundImage);
				hBitmap = bmp.GetHbitmap(Color.FromArgb(0));  //Set the fact that background is transparent
				oldBitmap = API.SelectObject(memDc, hBitmap);

				//Display-rectangle
				Size size = bmp.Size;
				Point pointSource = new Point(0, 0);
				Point topPos = new Point(this.Left, this.Top);

				//Set up blending options
				API.BLENDFUNCTION blend = new API.BLENDFUNCTION();
				blend.BlendOp = API.AC_SRC_OVER;
				blend.BlendFlags = 0;
				blend.SourceConstantAlpha = 255;
				blend.AlphaFormat = API.AC_SRC_ALPHA;

				API.UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, API.ULW_ALPHA);

				//Clean-up
				bmp.Dispose();
				API.ReleaseDC(IntPtr.Zero, screenDc);
				if (hBitmap != IntPtr.Zero)
				{
					API.SelectObject(memDc, oldBitmap);
					API.DeleteObject(hBitmap);
				}
				API.DeleteDC(memDc);
			}
			catch (Exception)
			{
			}
		}

		#endregion

		private void Splash_Load(object sender, EventArgs e)
		{
			UpdateFormDisplay(this.BackgroundImage);
		}

		private void Splash_Paint(object sender, PaintEventArgs e)
		{
			//Call our drawing function
			UpdateFormDisplay(this.BackgroundImage);
		}
	}

	internal class API
	{
		public const byte AC_SRC_OVER = 0x00;
		public const byte AC_SRC_ALPHA = 0x01;
		public const Int32 ULW_ALPHA = 0x00000002;

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BLENDFUNCTION
		{
			public byte BlendOp;
			public byte BlendFlags;
			public byte SourceConstantAlpha;
			public byte AlphaFormat;
		}

		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);


		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteDC(IntPtr hdc);


		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteObject(IntPtr hObject);
	}
}