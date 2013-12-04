using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HrsVerification.Models
{
    public class TimeVerificationDb: DbContext   
    {
        public TimeVerificationDb() : base("name=DefaultConnection")
        {
        }

        public DbSet<Branch> BranchList         { get; set; }
        public DbSet<TimeVerificationForm> TimeVerificationFormList { get; set; }
        public DbSet<WorkedTime> WorkedTimeList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}