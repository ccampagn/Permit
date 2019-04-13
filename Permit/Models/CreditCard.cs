using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class CreditCard
    {
        int CreditCardId { get; set; }
        int Number { get; set; }

        public CreditCard(int CreditCardId,int Number)
        {
            this.CreditCardId = CreditCardId;
            this.Number = Number;
        }

    }
}
