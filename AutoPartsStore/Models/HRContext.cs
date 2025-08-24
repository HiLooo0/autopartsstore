using System.Data.Entity;
using AutoPartsStore.Models;

namespace AutoPartsStore.Data
{
    public class HRContext : DbContext
    {
        public HRContext() : base("HRContext") { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
