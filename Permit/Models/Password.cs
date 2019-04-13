using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Password
    {
        int PasswordId { get; set; }
        string Email { get; set; }
        string Hashvalue { get; set; }

        public Password(int PasswordId, string Email, string Hashvalue)
        {
            this.PasswordId = PasswordId;
            this.Email = Email;
            this.Hashvalue = Hashvalue;
        }
    }
}
