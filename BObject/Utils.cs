using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class Utils
    {
        static string api = ConfigurationManager.AppSettings["SmsApi"].Replace("Uttaran", "&");
        static string emailFrom = ConfigurationManager.AppSettings["EmailFrom"];
        static string emailHost = ConfigurationManager.AppSettings["EmailHost"];
        static string emailUID = ConfigurationManager.AppSettings["EmailUID"];
        static string emailPWD = ConfigurationManager.AppSettings["EmailPWD"];
        static string emailCC = ConfigurationManager.AppSettings["EmailCC"];
        static string emailBCC = ConfigurationManager.AppSettings["EmailBCC"];

        public static bool sendEmail(string emailTo, string subject, string message, bool sendCC, bool sendBCC)
        {
            try
            {
                MailMessage mailmessage = new MailMessage(emailFrom, emailTo) { Subject = subject, Body = message };
                if (sendCC && emailCC != null && !string.IsNullOrWhiteSpace(emailCC)) mailmessage.CC.Add(new MailAddress(emailCC));
                if (sendBCC && emailBCC != null && !string.IsNullOrWhiteSpace(emailBCC)) mailmessage.Bcc.Add(new MailAddress(emailBCC));

                new SmtpClient()
                {
                    Host = emailHost,
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(emailUID, emailPWD),
                }.Send(mailmessage);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }

        }
        public static SMSBO sendSMS(string mobileno, string msgtxt)
        {
            var url = string.Format(api, mobileno.Trim(), msgtxt.Trim());
            try
            {
                WebRequest request = HttpWebRequest.Create(url.Trim());
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream s = (Stream)response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(s))
                        {
                            string smsResponse = readStream.ReadToEnd();
                            return new SMSBO() { mobileNo = mobileno.Trim(), trackingNo = smsResponse.Substring(2, 6), remarks = "Success", msg = msgtxt };
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new SMSBO() { mobileNo = mobileno.Trim(), remarks = "Fail", msg = msgtxt };
            }
        }


        #region MVCUtils

        public static string FinancialCalender(Int32 MonthId, string Session)
        {
            string[] Sessions = Session.Split('-');
            var startSession = Convert.ToInt32(Sessions[0]);
            var year = startSession;
            MonthId = MonthId <= 9 ? MonthId + 3 : MonthId - 9;

            if (MonthId < 4) year = year + 1;
            var lastDayOfMonth = DateTime.DaysInMonth(year, MonthId);
            var IndMonth = MonthId.ToString();
            if (MonthId < 10)
            {
                IndMonth = "0" + MonthId;
            }

            return lastDayOfMonth + "/" + IndMonth + "/" + year;
        }

        #endregion  MVCUtils


    }
}
