using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class EmergencyContract
    {
        int EmergencyContractId { get; set; }
        Name Name { get; set; }
        string Email { get; set; }
        public EmergencyContract(int EmergencyContractId,Name Name,string Email)
        {
            this.EmergencyContractId = EmergencyContractId;
            this.Name = Name;
            this.Email = Email;
        }
    }
}
