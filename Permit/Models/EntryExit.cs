﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permit.Models
{
    public class EntryExit
    {
        int EntryExitId { get; set; }
        string Name { get; set; }
        public EntryExit(int EntryExitId, string Name)
        {
            this.EntryExitId = EntryExitId;
            this.Name = Name;
        }
    }
}
