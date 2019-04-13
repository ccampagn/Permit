using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        public string Name { get; set; }
        public Park(int ParkId, string Name)
        {
            this.ParkId = ParkId;
            this.Name = Name;        
        }
    }
}
