using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Text.RegularExpressions;
//using Humanizer;

namespace EPGM.Framework
{
    class FileExport
    {
        static iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 8f);
        static iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 8f);
        static iTextSharp.text.Font baseFontBold = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED), 8f);
        static iTextSharp.text.Font baseFontHeading = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 12f);
        public string filkanam;

        public static byte[] CreatePDF(List<object> data, string filename)
        {
            string fileName = string.Empty;
            DateTime fileCreationDatetime = DateTime.Now;
            MemoryStream msReport = new MemoryStream();
            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 30f);
            Document pdfDoc = new Document(new Rectangle(288f, 144f), 10, 10, 10, 10);
            pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                var evt = new ITextEvents();
                evt.Header = filename;
                evt.fileName = filename;
                evt.data = data;
                pdfWriter.PageEvent = evt;
                pdfDoc.Open();
                var count = data[0].GetType().GetProperties().Count();
                var pdfPTable = new PdfPTable(count);
                var myType = data[0].GetType();
                IList<PropertyInfo> properties = new List<PropertyInfo>(myType.GetProperties());
                List<PdfPCell> cells = new List<PdfPCell>();
                PdfPRow row = new PdfPRow(cells.ToArray<PdfPCell>());
                foreach (var record in data)
                {
                    int i = 0;
                    cells = new List<PdfPCell>();
                    myType = record.GetType();
                    properties = new List<PropertyInfo>(myType.GetProperties());
                    foreach (PropertyInfo property in properties)
                    {
                        var val = property.GetValue(record, null);
                        PdfPCell cell = new PdfPCell();
                        string propValue = (val == null) ? "" : val.ToString();
                        if (propValue == "")
                        {
                            i++;
                        }
                        int errorcount = Regex.Matches(propValue, @"[a-zA-Z]").Count;
                        if (propValue == "Total:" || propValue == "Grand Total:")
                        {
                            cell = new PdfPCell(new Phrase(6, propValue, baseFontBold));
                        }
                        cell = new PdfPCell(new Phrase(6, propValue, baseFontNormal));
                        if (errorcount > 0)
                            cell.HorizontalAlignment = 0;
                        else
                            cell.HorizontalAlignment = 2;
                        cells.Add(cell);
                    }
                    row = new PdfPRow(cells.ToArray<PdfPCell>());
                    pdfPTable.Rows.Add(row);
                }
                pdfDoc.Add(pdfPTable);
                pdfDoc.Close();
            }
            catch (Exception ex) { }
            finally { }
            return msReport.ToArray();
        }
    }

    public class ITextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        #endregion

        public string fileName { get; set; }
        public List<PdfPCell> colheads { get; set; }
        public List<object> data { get; set; }
        iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 8f);
        iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 8f);
        iTextSharp.text.Font baseFontBold = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 10f);
        iTextSharp.text.Font baseFontHeading = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 14f);
        iTextSharp.text.Font baseFontHeading1 = new iTextSharp.text.Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.EMBEDDED), 12f);

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                footerTemplate = cb.CreateTemplate(100, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnStartPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            //Stream st = new Stream();
            var headerTable = new PdfPTable(3);
            headerTable.SpacingBefore = 0;
            var cnt = data[0].GetType().GetProperties().Count();
            var pdfHTable = new PdfPTable(cnt);
            headerTable.HorizontalAlignment = Element.ALIGN_CENTER;
            //byte[] data21 = File.ReadAllBytes();
            //MemoryStream ms = new MemoryStream(data21);
            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/EPGM-LOGO.png"));
            //logo.SetAbsolutePosition(120, document.PageSize.Height - 90);
            //logo.ScalePercent(7f);
            //PdfPCell logoCell = new PdfPCell();
            //logoCell.AddElement(logo);
            //logoCell.Border = 0;
            //logoCell.Colspan = 2;
            //logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell emptycel = new PdfPCell();
            emptycel.Border = 0;
            headerTable.AddCell(emptycel);
            //headerTable.AddCell("hai");
            //headerTable.AddCell("cell");
            //headerTable.AddCell(" ");
            var count = data[0].GetType().GetProperties().Count();
            List<object> headtext = new List<object>();
            headtext.Add(fileName);
            //if ((disname != ""))
            //    headtext.Add(disname);
            //headtext.Add("Report Generated for the Period of: " + fromdate + " To " + todate);
            var pdfheadertb = new PdfPTable(1);
            //headerTable.AddCell(logoCell);
            //headerTable.AddCell(" ");
            var celldat2 = new PdfPCell(new Phrase(12, "Growth Monitoring System", baseFontHeading));
            celldat2.HorizontalAlignment = Element.ALIGN_CENTER;
            celldat2.Border = 0;
            pdfheadertb.AddCell(celldat2);
            var celldat = new PdfPCell(new Phrase(14, "Government of Telangana", baseFontHeading1));
            celldat.HorizontalAlignment = Element.ALIGN_CENTER;
            celldat.Border = 0;
            pdfheadertb.AddCell(celldat);
            foreach (object colhead in headtext)
            {
                var celldata = new PdfPCell(new Phrase(12, colhead.ToString(), baseFontBold));
                celldata.HorizontalAlignment = Element.ALIGN_CENTER;
                celldata.Border = 0;
                PdfPCell[] coldata = new PdfPCell[] { celldata };
                PdfPRow rowdata = new PdfPRow(coldata);
                pdfheadertb.Rows.Add(rowdata);
            }
            pdfheadertb.HorizontalAlignment = Element.ALIGN_CENTER;
            var celldat1 = new PdfPCell(new Phrase(12, "Report Generated Time:" + System.DateTime.Now.Day + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Year + " " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute, baseFontBig));
            celldat1.HorizontalAlignment = Element.ALIGN_LEFT;
            celldat1.Border = 0;
            //document.Add(logo);
            var myType = data[0].GetType();

            IList<PropertyInfo> properties = new List<PropertyInfo>(myType.GetProperties());
            List<PdfPCell> cells = new List<PdfPCell>();
            foreach (PropertyInfo prop in properties)
            {
                string propValue = prop.Name;   //.Humanize(LetterCasing.Title);
                var cell = new PdfPCell(new Phrase(12, propValue, baseFontBold));
                cells.Add(cell);
            }
            PdfPRow row = new PdfPRow(cells.ToArray<PdfPCell>());
            pdfHTable.Rows.Add(row);

            PdfPTable reporttb = new PdfPTable(9);
            var col1 = new PdfPCell(new Phrase(12, " ", baseFontBig));
            col1.Colspan = 5;
            col1.Border = 0;
            celldat1.Colspan = 4;
            reporttb.AddCell(col1);
            reporttb.AddCell(celldat1);
            if (document.PageNumber == 1)
            {
                document.Add(headerTable);
                document.Add(pdfheadertb);
                document.Add(reporttb);
            }

            document.Add(pdfHTable);
            PdfPTable footerTable = new PdfPTable(1);

            //byte[] data12 = File.ReadAllBytes();
            //MemoryStream ms1 = new MemoryStream(data12);
            //iTextSharp.text.Image logo1 = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/EPGM-LOGO.png"));
            //logo1.SetAbsolutePosition(10, 100);
            //logo1.ScalePercent(40f);
            //PdfPCell addressCell = new PdfPCell();
            //addressCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //addressCell.AddElement(logo1);
            //addressCell.Border = 0;
            //footerTable.AddCell(addressCell);
            footerTable.TotalWidth = document.PageSize.Width - 80f;
            footerTable.WidthPercentage = 100;
            footerTable.WriteSelectedRows(0, -1, 40, document.PageSize.GetBottom(500), writer.DirectContent);
        }
        
        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);
        }
    }
}
