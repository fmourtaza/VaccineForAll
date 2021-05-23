using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class Utilities
    {
        public static void HandleException(Exception ex)
        {
            Console.WriteLine("VaccineForAll Exception: " + ex.Message + ex.StackTrace);
            SendMail("fmourtaza@gmail.com", "VaccineForAll Exception", ex.Message + ex.StackTrace);
        }

        public static void SendWelcomeMail(String citizenEmail, String emailSubject)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<p>Dear {0},</p>", citizenEmail));
            builder.Append(string.Format("<p>Thanks for registering with us!</p>"));
            builder.Append(string.Format("<p>This application's ultimate goal is to provide valuable information which helps fellow citizens to get vaccinated in their respective district, and for your information, the use of this application is free of cost.</p>"));
            builder.Append(string.Format("<p>«<b><i>We believe that it is everyone's social responsibility to keep each other safe! </i></b>»</p>"));
            builder.Append(string.Format("<p>Please note that this web application <b>does NOT book any slot on your behalf whatsoever </b>- it only provides valuable information to help the citizen to select the available center at that point in time.</p>"));
            builder.Append(string.Format("<p><b><i><u>How it works:</u></i></b></p>"));
            builder.Append(string.Format("<p>The program run in an interval from 6 AM to 8 PM to query the provided Co-WIN Public APIs to look for an available center in your respective District, taking into consideration the age and available dose, for more details about the API, <a href='https://apisetu.gov.in/public/marketplace/api/cowin' target='_blank'>click here.</a></p>"));
            builder.Append(string.Format("<p>Once the program finds an available center, an email will be sent at the registered email address a complete report with all details which shows all the available centers along with the available dose at that point in time. </p>"));
            builder.Append(string.Format("<p>It is important to mention that upon receiving the report, it is highly recommended to book the slots on the <a href='https://www.cowin.gov.in/home' target='_blank'>cowin.gov.in</a> website or using the Aarogya Setu mobile app.</p>"));
            builder.Append(string.Format("<p><b><i><u>Direct Report Viewer</u></i></b></p>"));
            builder.Append(string.Format("<p>Additionally, you can view the data for a particular district at the <a href='https://vaccineforall.co.in/reportviewer.aspx' target='_blank'>Report Viewer</a> page</p>"));
            builder.Append(string.Format("<p>Best regards.</p>"));
            builder.Append(string.Format("<p><b>VaccineForAll Team</b></p>"));

            SendMail(citizenEmail, emailSubject, builder.ToString());
        }

        public static void SendNoDataFoundYet(String citizenEmail, String emailSubject, String district_Name, int citizenAge, String citizenDoseChoice)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<p>Hello {0},</p>", citizenEmail));
            builder.Append(string.Format("<p>The program has not yet found any data for the following critera - [District Name: {0} - Age: {1} - Dose: {2}].</p>", district_Name, citizenAge, citizenDoseChoice));
            builder.Append(string.Format("<p>The program runs in an intervals and once the available slots are found, we will inform you accordingly</p>"));
            builder.Append(string.Format("<p>Additionally, you can view the data for a particular district at the <a href='https://vaccineforall.co.in/reportviewer.aspx' target='_blank'>Report Viewer</a> page</p>"));
            builder.Append(string.Format("<p>Best regards.</p>"));
            builder.Append(string.Format("<p><b>VaccineForAll</b></p>"));

            SendMail(citizenEmail, emailSubject, builder.ToString());
        }

        public static bool SendMail(String emailAddress, String emailSubject, String emailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Credentials.MailUserName);
                mail.To.Add(emailAddress);
                mail.Subject = emailSubject;
                mail.Body = emailBody;
                mail.IsBodyHtml = true;

                var client = new SmtpClient(Credentials.MailSmtp, Credentials.MailPort)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(Credentials.MailUserName, Credentials.MailPassword),
                };
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("VaccineForAll Exception: " + ex.Message + ex.StackTrace);
            }
            return true;
        }

        public static void SendTeamUpdatesMail(String citizenEmail, String emailSubject)
        {
            var istDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<p>Hello {0},</p>", citizenEmail));
            builder.Append(string.Format("<p>Good news! Vaccines are back!!!"));
            builder.Append(string.Format("<p>Therefore this is to inform you that we are changing the frequency of the program to run every <b>2 hours</b> from 6 AM to 8 PM effective {0}.", istDateTime.AddDays(1).ToString("dddd, dd MMMM yyyy")));
            builder.Append(string.Format("<p>Additionally, you can view the data for a particular district at the <a href='https://vaccineforall.co.in/reportviewer.aspx' target='_blank'>Report Viewer</a> page</p>"));
            builder.Append(string.Format("<p>Thanks for being with us and Stay Safe!</ p>"));
            builder.Append(string.Format("<p>Best regards.</p>"));
            builder.Append(string.Format("<p><b>VaccineForAll Team</b></p>"));
            SendTeamMail(citizenEmail, emailSubject, builder.ToString());
        }

        public static bool SendTeamMail(String emailAddress, String emailSubject, String emailBody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Credentials.MailTeamUserName);
                mail.To.Add(emailAddress);
                mail.Subject = emailSubject;
                mail.Body = emailBody;
                mail.IsBodyHtml = true;

                var client = new SmtpClient(Credentials.MailSmtp, Credentials.MailPort)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(Credentials.MailTeamUserName, Credentials.MailTeamPassword),
                };
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("VaccineForAll Exception: " + ex.Message + ex.StackTrace);
            }
            return true;
        }

        public static string toHTML_Table(DataTable dt, String citizenEmail, int citizenAge, String district_name, String citizenDoseChoice)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("Dear {0}<br><br>", citizenEmail));
            builder.Append(string.Format("<p>Your district name: {0} - Your age: {1} - Your dose choice: {2}.</p>", district_name, citizenAge, citizenDoseChoice));
            builder.Append(string.Format("<p>It is important to mention that upon receiving this report, it is highly recommended to book the slots on the <a href='https://www.cowin.gov.in/home' target='_blank'>cowin.gov.in</a> website or using the Aarogya Setu mobile app.</p>"));
            builder.Append("<font>The following are the slots availability: </font><br><br>");
            builder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            builder.Append("<tr align='left' valign='top'>");

            try
            {
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'><b>");
                    builder.Append(c.ColumnName);
                    builder.Append("</b></td>");
                }
                builder.Append("</tr>");
                foreach (DataRow r in dt.Rows)
                {
                    builder.Append("<tr align='left' valign='top'>");
                    foreach (DataColumn c in dt.Columns)
                    {
                        builder.Append("<td align='left' valign='top'>");
                        builder.Append(r[c.ColumnName]);
                        builder.Append("</td>");
                    }
                    builder.Append("</tr>");
                }
                builder.Append("</table>");
                builder.Append("<br><br>Regards.");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return builder.ToString();
        }
    }
}
