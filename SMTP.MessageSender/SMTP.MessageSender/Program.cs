using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MesageSender;

namespace SMTP.MessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var emailSender = new EmailSender();
            emailSender.SendEmail(new Contact { FirstName = "yourName", Email = "toSendEmail", LastName = "Hakobyan" }, "Message");
        }
    }
}
