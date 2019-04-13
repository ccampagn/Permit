using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Campsite
    {
        public int CampsiteId { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int TotalSite { get; set; }
        public int AdvanceSite { get; set; }
        public int Stock { get; set; }
        public int Tents { get; set; }
        public int GroupSize { get; set; }
        public Campsite(int CampsiteId,string Name,DateTime OpenDate,DateTime CloseDate,int TotalSite,int AdvanceSite,int Stock,int Tents,int GroupSize)
        {
            this.CampsiteId = CampsiteId;
            this.Name = Name;
            this.OpenDate = OpenDate;
            this.CloseDate = CloseDate;
            this.TotalSite = TotalSite;
            this.AdvanceSite = AdvanceSite;
            this.Stock = Stock;
            this.Tents = Tents;
            this.GroupSize = GroupSize;
        }
        public int getcampsiteneeded(int groupsize,int tent)
        {
            int groupsite = (int)Math.Ceiling((double)groupsize /this.GroupSize);
            int tentsite = (int)Math.Ceiling((double)tent / this.GroupSize);
            return Math.Max(groupsite,tentsite);
        }
    }
}
