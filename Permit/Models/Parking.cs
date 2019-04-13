using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Parking
    {
        int ParkingId { get; set; }
        string LotName { get; set; }
        string PlateNumber { get; set; }
        string State { get; set; }
        bool Rental { get; set; }
    }
}
