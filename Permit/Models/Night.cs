using Permit.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Night
    {
        public int NightId { get; set; }
        public DateTime Date { get; set; }
        public Campsite Campsite { get; set; }
        public Night(int NightId, DateTime Date, Campsite Campsite){
            this.NightId = NightId;
            this.Date = Date;
            this.Campsite = Campsite;
       }

        public bool CheckAvailable(Application a)
        {
            Db db = new Db();
            
            if (db.sitespots(this.Campsite)- db.sitetaken(this)- this.Campsite.getcampsiteneeded(a.GroupSize, a.Tents)>= 0 && db.stockleft(a,this))
            {

                return true;
            }
            return false;
        }
    }
}
