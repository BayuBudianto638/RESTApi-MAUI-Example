using Microsoft.EntityFrameworkCore;
using RESTWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Data.Databases
{
    public class HumanResourcesContext : DbContext
    {
        public DbSet<MstEmployee> Employees { get; set; }
        public HumanResourcesContext(DbContextOptions<HumanResourcesContext> options) : base(options)
        {

        }
    }
}
