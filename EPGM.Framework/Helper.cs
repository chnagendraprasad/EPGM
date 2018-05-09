using EPGM.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace EPGM.Framework
{
    public class Helper
    {

        public static string GetIPAddress()
        {
            var strHostName = HttpContext.Current.Request.UserHostAddress;

            if (strHostName != null)
            {
                var ipAddress = Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                if (ipAddress == "::1")
                {
                    ipAddress = "127.0.0.1";
                }
                return ipAddress;
            }
            return "";
        }
        public static string StateCode
        {
            get
            {
                return (string)System.Web.HttpContext.Current.Session["StateCode"];
            }
        }

        public static string GetGrades(string grade)
        {
            if (grade == "0")
                return "NOR";
            if (grade == "1")
                return "MUW";
            if (grade == "2")
                return "SUW";
            return grade;
        }

        public static string GetGradeCode(string gradeName)
        {
            if (gradeName == "NOR" || gradeName == "0")
                return "0";
            if (gradeName == "MUW" || gradeName == "1")
                return "1";
            if (gradeName == "SUW" || gradeName == "2")
                return "2";
            if (gradeName == "ABS")
                return "3";
            if (gradeName == "Total")
                return "Total";
            return "";
        }

        public static string GetGradeNames(string whoType = "")
        {
            if (whoType == "NOR")
                return " - Normal Weight.";
            if (whoType == "MUW")
                return " - Moderately Under Weight.";
            if (whoType == "SUW")
                return " - Severely Under Weight.";
            if (whoType == "ABS")
                return " - Absent.";
            return " - Total Beneficiaries.";
        }

        public static string GetGender(string Gender)
        {
            if (Gender == "M")
            {
                return "Male";
            }
            else if (Gender == "F")
            {
                return "FeMale";
            }
            else
            {
                return Gender;
            }
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "R@m!nf0@!@#";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x52, 0x40, 0x57, 0x46, 0x30, 0x52, 0x57, 0x41, 0x74, 0x6c, 0x54, 0x64 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Reads setting from config file
        /// </summary>
        /// <param name="setting">Setting to read</param>
        /// <param name="val">Default value if setting not found</param>
        /// <returns>Setting value</returns>
        public static string GetSetting(string setting, string val = "")
        {
            if (WebConfigurationManager.AppSettings[setting] == null)
                return val;
            else
                return WebConfigurationManager.AppSettings[setting];
        }

        /// <summary>
        /// Reads setting from config file
        /// </summary>
        /// <param name="setting">Setting to read</param>
        /// <param name="val">Default value if setting not found</param>
        /// <returns>Setting value</returns>
        public static int GetSetting(string setting, int val)
        {
            if (WebConfigurationManager.AppSettings[setting] == null)
                return val;
            else
                return int.Parse(WebConfigurationManager.AppSettings[setting]);
        }

        /// <summary>
        /// Reads setting from config file
        /// </summary>
        /// <param name="setting">Setting to read</param>
        /// <param name="val">Default value if setting not found</param>
        /// <returns>Setting value</returns>
        public static bool GetSetting(string setting, bool val)
        {
            if (WebConfigurationManager.AppSettings[setting] == null)
                return val;
            else
                return bool.Parse(WebConfigurationManager.AppSettings[setting]);
        }

        public static void FileLogging(string message)
        {
            string filename = @"\Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            string value = System.Web.Hosting.HostingEnvironment.MapPath(@"~\") + "\\Logs\\";

            if (!System.IO.Directory.Exists(value))
                System.IO.Directory.CreateDirectory(value);

            var filepath = value + filename;
            if (!File.Exists(filepath))
            {
                var writer = File.CreateText(filepath);
                writer.Close();
            }

            using (var writer = new StreamWriter(filepath, true))
                writer.WriteLine(message);
        }

        public static void Export(string type, HttpResponseBase Response, List<object> data, string filename)
        {
            Response.ClearContent();
            Response.Buffer = true;

            var grid = new GridView();
            grid.DataSource = data;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            switch (type)
            {
                case "EXCEL":
                    Response.AddHeader("content-disposition", "attachment; filename=Export.xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Output.Write("<table><tr><td colspan='13' align='center' style='font-size:20px; color: black'><b>GOVERNMENT OF TELANGANA</b></td></tr><tr><td colspan='13' align='center' style='font-size:20px'><b>Growth Monitoring System</b></td></tr><tr><td colspan='13' align='center'><b>" + filename + "</b></td></tr><tr><td colspan='7'></td><td colspan='6' align='right'>Report Generated Time: " + System.DateTime.Now.Day + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Year + " " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + "</td></tr></table>" + sw.ToString());
                    //Response.Output.Write(sw.ToString());
                    break;
                case "PDF":
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(FileExport.CreatePDF(data, filename));
                    Response.Flush();
                    Response.End();
                    break;
            }
            Response.Flush();
            Response.End();
        }


    }

    public class EPGMFTPClass
    {
        private static string host = Helper.GetSetting("FTPAddress");
        private static string user = Helper.GetSetting("FTPUsername");
        private static string pass = Helper.GetSetting("FTPPassword");
        private static FtpWebRequest ftpRequest = null;
        private static FtpWebResponse ftpResponse = null;
        private static Stream ftpStream = null;
        private static int bufferSize = 2048;

        public static string InsertIntoLocalFile(string center, string beneData)
        {
            try
            {
                var filename = @"" + Helper.GetSetting("UploadFileName");
                var value = System.Web.Hosting.HostingEnvironment.MapPath(@"~/NewBeneDetails/A") + center;
                if (!System.IO.Directory.Exists(value))
                {
                    System.IO.Directory.CreateDirectory(value);
                }

                var filepath = value + "\\" + filename;
                if (!File.Exists(filepath))
                {
                    var writer = File.CreateText(filepath);
                    writer.Close();
                }
                else
                {
                    File.Delete(filepath);
                    var writer = File.CreateText(filepath);
                    writer.Close();
                }
                using (var writer = new StreamWriter(filepath, true))
                {
                    var sb = new StringBuilder();
                    sb.Append(beneData);
                    writer.WriteLine(sb.ToString());
                }
                return "000";
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return ex.Message;
                else
                    return ex.InnerException.Message;
            }
        }

        public static string InsertIntoFTPFile(string center)
        {
            try
            {
                string fileName = Helper.GetSetting("UploadFileName");
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/A" + center + "/" + fileName);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                // Copy the contents of the file to the request stream.
                StreamReader sourceStream = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/NewBeneDetails/A" + center + "/UP_CART.txt"));
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                ftpRequest.ContentLength = fileContents.Length;

                Stream requestStream = ftpRequest.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                Helper.FileLogging("Upload Data Completed, status {0}" + response.StatusDescription);
                Helper.FileLogging("Upload Data Completed, status {0} : " + response.StatusCode);
                response.Close();
                return "000";
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return ex.Message;
                else
                    return ex.InnerException.Message;
            }
        }
    }
    public class SmartAttendance
    {
        public static string AddUser_Aindra(string uniqueid, string firstname, string lastname)
        {
            try
            {

                string request = "<?xml version='1.0' encoding='UTF-8'?><organizations> <task value='recognize' /><organization name='RAMINFO'>" +
                                "<states><state name='AP'><users><user><username>" + uniqueid + "</username>" +
                                "<unique_id>" + uniqueid + "</unique_id><first_name>" + firstname + "</first_name><last_name>" + lastname + "</last_name>" +
                               "<email /><mobile /></user></users></state></states></organization></organizations>";

                var httpClient = new System.Net.Http.HttpClient
                {
                    BaseAddress = new Uri(Helper.GetSetting("SmartAttendanceURL"))
                };

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Helper.GetSetting("AttendanceKey"));

                var httpContent = new StringContent(request, Encoding.UTF8, "application/xml");
                var response = httpClient.PostAsync("v1/adduser", httpContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    //var reader1 = DoSerialize.DeserializeFromXml<Organizations>(response.Content.ReadAsStringAsync().Result);

                    var serializer = new XmlSerializer(typeof(Organizations));
                    var reader = new StringReader(response.Content.ReadAsStringAsync().Result);
                    var XMlResponse = (Organizations)serializer.Deserialize(reader);

                    return XMlResponse.Organization.States.State.Users.User.Ack.ToString();
                }
                else
                {
                    return "Timeout. Operation is aborted";
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                    return ex.Message;
                else
                    return ex.InnerException.Message;
            }

        }
    }
}
