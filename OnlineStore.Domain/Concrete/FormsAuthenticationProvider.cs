using OnlineStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Concrete
{
    public class FormsAuthenticationProvider : IAuthentication
    {
        private readonly EFDbContext Context = new EFDbContext();
        public bool Authenticate(string username, string password)
        {
            var res = Context.Users.FirstOrDefault(u =>u.Username== username && u.Password== password);
            if (res==null)
            {
                return false;
            }
            else { return true; }
        }

        public bool Logout()
        {
            return false;
        }
    }
}
