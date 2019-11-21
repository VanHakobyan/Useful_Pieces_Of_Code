using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace EmailSender.BLL
{
    public class EmailSender
    {
        private List<ContactResponseModel> contactResponseModels;

        public EmailSender()
        {
            //var json = File.ReadAllText($"{Directory.GetCurrentDirectory()}/contacts.json");
            //var contacts = JsonConvert.DeserializeObject<List<Profile>>(json);
            //FactoryModel(contacts);
        }

        private void FactoryModel(List<Profile> contacts)
        {
            contactResponseModels = new List<ContactResponseModel>();
            foreach (var contact in contacts)
            {
                contactResponseModels.Add(new ContactResponseModel { Email = contact.eMail, FullName = contact.Nickname });
            }
        }

        private string GetMessageText(ContactResponseModel contact)
        {
            try
            {
                var templateText = File.ReadAllText("");
                return
                    templateText.Replace("{FullName}", contact.FullName)
                     .Replace("{Email}", contact.Email)
                        .Replace("{DateTimeNow}", DateTime.UtcNow.ToString());
            }
            catch
            {
                return "";
            }
        }

        public void SendEmail(ContactResponseModel contact)
        {

            using (var msg = new MailMessage())
            {

                msg.From=new MailAddress("van19962009@mail.ru");

                msg.To.Add(contact.Email);
                //msg.To.Add("vanhakobyan1996@gmail.com");
                msg.Subject = "VH Development";
                msg.IsBodyHtml = true;
                msg.Body = GetMessageText(contact);

                var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("newsvh2018@gmail.com", "*")
                };

                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }

            }

        }

        public void SendEmail()
        {

            using (var msg = new MailMessage())
            {

                msg.From = new MailAddress("newsvh2018@gmail.com");

                msg.To.Add("*@gmail.com");
                //msg.To.Add("vanhakobyan1996@gmail.com");
                msg.Subject = "VH Development";
                msg.IsBodyHtml = true;
                msg.Body = File.ReadAllText(@"*");
                msg.BodyTransferEncoding = TransferEncoding.QuotedPrintable;
                msg.Headers.Add("Content-Type", "text/html; charset='iso-8859-1'");
                var client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("newsvh2018@gmail.com", "*")
                };

                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }

            }

        }

        public void SendEmailList(int count)
        {
            for (int i = 0; i < count; i++)
                SendEmail(contactResponseModels[i]);
        }

    }
}
