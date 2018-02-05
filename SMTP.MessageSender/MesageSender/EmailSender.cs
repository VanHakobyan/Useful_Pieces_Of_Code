using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace MesageSender
{
    public class EmailSender
    {
        public void SendEmail(Contact contact, string content)
        {

            using (var msg = new MailMessage())
            {

                msg.To.Add(contact.Email);
                msg.Subject = $"Dear member - {contact.FirstName} {contact.LastName}";
                //msg.IsBodyHtml = true;
                msg.Body = content;

                var client = new SmtpClient();
                try
                {
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    File.AppendAllText(@"D:\emailSender.log",$"Message: {ex.Message} {Environment.NewLine}Inner: {ex.InnerException}{Environment.NewLine}");
                }

            }

        }

        public void SendEmailList(List<Contact> contacts, string content)
        {
            foreach (var contact in contacts)
                SendEmail(contact, content);
        }
    }
}
