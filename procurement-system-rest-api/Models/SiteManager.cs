﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class SiteManager: CompanyStaff
    {
        public List<Site> SiteList { get; set; }
    }
}
