using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EPGM.Business
{
    public class NotificationService
    {

        private static NotificationService _instance;

        /// <summary>
        /// Email ID from which the mails to be sent (from Config setting)
        /// </summary>
        MailAddress FromEMAIL;

        /// <summary>
        /// Password of Email from which the mails to be sent (from Config setting)
        /// </summary>
        string FromPASS;

        /// <summary>
        /// SMTP Port number to use to send Email (from Config setting)
        /// </summary>
        int SMTPPort;

        /// <summary>
        /// SMTP server to use to send Email (from Config setting)
        /// </summary>
        string SMTPServer;

        /// <summary>
        /// SMS Token to use to send SMS (from Config setting)
        /// </summary>
        string SMSTOKEN;

        /// <summary>
        /// SMS Account ID to use to send SMS (from Config setting)
        /// </summary>
        string SMSACTID;

        /// <summary>
        /// Number from which SMS to use to send (from Config setting)
        /// </summary>
        string SMSFrom;

        /// <summary>
        /// Returns the Singleton instance
        /// </summary>
        public static NotificationService Instance
        {
            get
            {
                //  check if notification service object is created or not.
                if (_instance == null)
                    _instance = new NotificationService();
                return _instance;
            }
        }

        /// <summary>
        /// Singleton constructor that reads config settings
        /// </summary>
        public NotificationService()
        {
            FromEMAIL = new MailAddress(ConfigurationManager.AppSettings["EmailSender"]);
            FromPASS = ConfigurationManager.AppSettings["EmailPassword"];
            SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            SMTPServer = ConfigurationManager.AppSettings["SMTPServer"];
            //SMSTOKEN = Helper.GetSetting("SMSTOKEN");
            //SMSACTID = Helper.GetSetting("SMSACTID");
            //SMSFrom = Helper.GetSetting("SMSFrom");
        }


        /// <summary>
        /// Sends an Email in a new thread
        /// </summary>
        /// <param name="To">To address to send the email</param>
        /// <param name="Subject">Subject of email</param>
        /// <param name="Message">message/body of email</param>
        public void SendMail(String To, string Subject, string Message)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(To.ToString());
                msg.Subject = Subject;
                msg.Body = Message;
                msg.IsBodyHtml = true;
                Thread newThread = new Thread(SendMail);
                newThread.Start(msg);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        public void MultipleSendMail(List<String> To, string Subject, string Message)
        {
            try
            {
                MailMessage msg = new MailMessage();
                foreach (var item in To)
                {
                    if (item != null && item != "")
                    {
                        if (item.Contains(".com"))
                        {
                            msg.To.Add(item.ToString());
                        }
                    }
                }
                msg.Subject = Subject;
                msg.Body = Message;
                msg.IsBodyHtml = true;
                Thread newThread = new Thread(SendMail);
                newThread.Start(msg);
            }
            catch (Exception ex)
            {
                //    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }


        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="msg">message to send</param>
        void SendMail(object msg)
        {
            try
            {
                MailMessage mail = (MailMessage)msg;
                mail.From = FromEMAIL;
                mail.IsBodyHtml = true;

                //  configure email settings
                SmtpClient smtp = new SmtpClient(SMTPServer, SMTPPort);
                smtp.EnableSsl = true;
                NetworkCredential netCre = new NetworkCredential(FromEMAIL.Address, FromPASS);
                smtp.Credentials = netCre;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                //  Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        public void SendMailWithMultipleAttachement(List<String> To, string Subject, string Message, List<string> FilePath)
        {
            try
            {
                MailMessage msg = new MailMessage();
                foreach (var i in FilePath)
                    if (i != "")
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(i.ToString());
                        msg.Attachments.Add(attachment);
                    }
                foreach (var i in To)
                {
                    if (i != "")
                    {
                        msg.To.Add(i.ToString());
                    }
                }
                msg.Subject = Subject;
                msg.Body = Message;
                msg.IsBodyHtml = true;
                Thread newThread = new Thread(SendMail);
                newThread.Start(msg);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }



    }
}

