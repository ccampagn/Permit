using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Address
    {
        int AddressId { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Country { get; set; }
        string ZipCode { get; set; }
        string PhoneNumber { get; set; }
        public Address(int AddressId, string Address1, string Address2, string City, string State,string Country,string ZipCode,string PhoneNumber)
        {
            this.AddressId = AddressId;
            this.Address1 = Address1;
            this.Address2 = Address2;
            this.City = City;
            this.State = State;
            this.Country = Country;
            this.ZipCode = ZipCode;
            this.PhoneNumber = PhoneNumber;

        }
    }
}
