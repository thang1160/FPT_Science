﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES.CustomModels.ScienceManagement.Researcher
{
    public class ResearcherList
    {
        public int peopleId { get; set; }
        public String name { get; set; }
        public String title { get; set; }
        public String position { get; set; }
        public String workplace { get; set; }
        public String interest { get; set; }
        public String googleScholar { get; set; }
    }
}