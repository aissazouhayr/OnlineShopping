using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "use your Eamil";
        public string MailFromAddress = "use your Eamil";
        public bool UseSsl = true;
        public string Username = "use your Eamil";
        public string Password = " use your password";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;


    }
}
