using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TNT.LSD.PDFGenerator
{
	/// <summary>
	/// Callback event signature for method that updates header before writing to page
	/// </summary>
	/// <param name="header">Header</param>
	/// <param name="document">Document</param>
	public delegate void OnBeforeWriteHeaderDelegate(PdfPTable header, Document document);

	/// <summary>
	/// Callback event signature for method that updates footer before writing to page
	/// </summary>
	/// <param name="footer">Footer</param>
	/// <param name="document">Document</param>
	public delegate void OnBeforeWriteFooterDelegate(PdfPTable footer, Document document);

	/// <summary>
	/// Page event helper
	/// </summary>
	public class PageEventHelper : PdfPageEventHelper
	{
		/// <summary>
		/// Callback event to updete header before writing to document
		/// </summary>
		public OnBeforeWriteHeaderDelegate OnBeforeWriteHeader { protected get; set; }

		/// <summary>
		/// Callback event to updete footer before writing to document
		/// </summary>
		public OnBeforeWriteFooterDelegate OnBeforeWriteFooter { protected get; set; }

		/// <summary>
		/// Header table
		/// </summary>
		protected PdfPTable m_Header = new PdfPTable(1);

		/// <summary>
		/// Footer table
		/// </summary>
		protected PdfPTable m_Footer = new PdfPTable(1);

		/// <summary>
		/// Initializes header and footer
		/// </summary>
		/// <param name="action">Action that setups up the header and footer</param>
		public PageEventHelper(Action<PdfPTable, PdfPTable> action)
		{
			if (action != null)
			{
				action(m_Header, m_Footer);
			}
		}

		/// <summary>
		/// Writes out the header when the page starts
		/// </summary>
		/// <param name="writer">Writer associated with the document</param>
		/// <param name="document">Document</param>
		public override void OnStartPage(PdfWriter writer, Document document)
		{
			PdfContentByte cb = writer.DirectContent;

			if (OnBeforeWriteHeader != null)
			{
				OnBeforeWriteHeader(m_Header, document);
			}

			//// This is used to see the margins
			//cb.MoveTo(document.Left, document.Top);
			//cb.LineTo(document.Right, document.Top);
			//cb.LineTo(document.Right, document.Bottom);
			//cb.LineTo(document.Left, document.Bottom);
			//cb.ClosePath();
			//cb.Stroke();

			m_Header.TotalWidth = document.Right - (document.RightMargin);
			m_Header.WriteSelectedRows(0, -1, document.LeftMargin, document.Top + (m_Header.TotalHeight + 5), cb);
		}

		/// <summary>
		/// Writes out the footer when the page ends
		/// </summary>
		/// <param name="writer">Writer associated with the document</param>
		/// <param name="document">Document</param>
		public override void OnEndPage(PdfWriter writer, Document document)
		{
			PdfContentByte cb = writer.DirectContent;

			if (OnBeforeWriteFooter != null)
			{
				OnBeforeWriteFooter(m_Footer, document);
			}

			m_Footer.TotalWidth = document.Right - (document.RightMargin);
			m_Footer.WriteSelectedRows(0, -1, document.LeftMargin, document.Bottom - 5, cb);
		}
	}
}