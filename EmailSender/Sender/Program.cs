using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailSender.BLL.EmailSender  emailSender=new EmailSender.BLL.EmailSender();
            emailSender.SendEmail();
        }
    }
}
