using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api
{
    public class ProcurementDbContext : DbContext
    {
        public ProcurementDbContext(DbContextOptions<ProcurementDbContext> options) : base(options) { }

        public DbSet<SiteManager> SiteManagers {get; set;}
    }
}
