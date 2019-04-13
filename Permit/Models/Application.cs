using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using Permit.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public Park Park { get; set; }
        public DateTime StartDate { get; set; }
        public EntryExit EntryTrail { get; set;}
        public EntryExit ExitTrail { get; set; }
        public string Status { get; set; }
        public int GroupSize { get; set; }
        public int Tents { get; set; }
        public List<Night> Nights { get; set; }
        public User user { get; set; }


        public Application(int ApplicationId,Park Park,DateTime StartDate,EntryExit EntryTrail,EntryExit ExitTrail,string Status,int GroupSize,int Tents,List<Night> nights,User user)
        {
            this.ApplicationId = ApplicationId;
            this.Park = Park;
            this.StartDate = StartDate;
            this.EntryTrail = EntryTrail;
            this.ExitTrail = ExitTrail;
            this.Status = Status;
            this.GroupSize = GroupSize;
            this.Tents = Tents;
            Nights = new List<Night>(nights);
            this.user = user;
        }

        public static void CheckApplication()
        {
            Db db = new Db();
            List<Application> application = db.getapplications();
            bool availity = true;
            foreach (Application a in application)
            {              
                foreach (Night n in a.Nights)
                {
                    if (n.CheckAvailable(a)==false)
                    {
                        availity = false;
                        break;
                    }
                }
                if (availity == true)
                {
                   db.updateapplication(a.ApplicationId,"Approve");
                }
                else
                {
                   db.updateapplication(a.ApplicationId, "Decline");
                }
            }
        }
    }
}
