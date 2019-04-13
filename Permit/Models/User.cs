using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class User
    {
        int UserId { get; set; }
        Password Password { get; set; }//done
        Name Name { get; set; }
        EmergencyContract EmergencyContract { get; set; }
        CreditCard CreditCard;

        public User(int UserId, Password Password,Name Name,EmergencyContract EmergencyContract,CreditCard CreditCard)
        {
            this.UserId = UserId;
            this.Password = Password;
            this.Name = Name;
            this.EmergencyContract = EmergencyContract;
            this.CreditCard = CreditCard;
        }

    }
}
