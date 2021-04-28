using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.API.ConfirmationMail
{
   public interface IEmailSender
    {
        void SendEmail(Message message);
        void SendEmailString(string message);
    }
}
