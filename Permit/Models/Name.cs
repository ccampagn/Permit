using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Name
    {
        int NameId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Suffix { get; set; }
        Address Address { get; set; }
        public Name(int NameId, string FirstName,string MiddleName, string LastName,string Suffix,Address Address)
        {
            this.NameId = NameId;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Suffix = Suffix;
            this.Address = Address;
        }
    }
}
