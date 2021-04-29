﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES.CustomModels.ScienceManagement.Report
{
    public class ReportByAuthorAward
    {
        //public int rowNum { get; set; }
        public string msnv_mssv { get; set; }
        //public int office_id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string office { get; set; }
        public List<int> paperAward { get; set; }
        public List<int> inventionAmount { get; set; }
        public int CitationAward { get; set; }
        //public string PublicYear { get; set; }
    }
}
