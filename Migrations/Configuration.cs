namespace HrsVerification.Migrations
{
    using HrsVerification.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;


    internal sealed class Configuration : DbMigrationsConfiguration<HrsVerification.Models.TimeVerificationDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false; // true;
        }

        protected override void Seed(HrsVerification.Models.TimeVerificationDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // seed table Branch
            //var branches = new List<Branch> {
            var branch = new Branch { Name = "ALB", OrgId = "360210"};
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "CSV", OrgId = "360220"};
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "DUB", OrgId = "360240" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "NWK", OrgId = "360280" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "SLZ", OrgId = "360300" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "UCY", OrgId = "360310" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "FRM", OrgId = "360260" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "FRM J", OrgId = "360262" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "CTV", OrgId = "360230" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "IRV", OrgId = "360270" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "NLS", OrgId = "360290" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "EXT-CORE", OrgId = "360250" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "BKM", OrgId = "360251" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "JAILS", OrgId = "360252" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "SR OUTREACH", OrgId = "360253" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "COMM REL", OrgId = "360115" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "ADULT PROG", OrgId = "360115" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "CHILD SERV", OrgId = "360115" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "COLL DEV", OrgId = "360116" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            branch = new Branch { Name = "OTHER", OrgId = "" };
            context.BranchList.AddOrUpdate(b => b.Name, branch);

            context.SaveChanges();

            SeedMembership();
        }

        private void SeedMembership()
        {
        //    if (!WebSecurity.Initialized)
        //    {
        //        WebSecurity.InitializeDatabaseConnection("DefaultConnection",
        //            "UserProfile", "UserId", "UserName", autoCreateTables: true);
        //    }

        //    var roles = (SimpleRoleProvider)Roles.Provider;
        //    var membership = (SimpleMembershipProvider)Membership.Provider;

        //    if (!roles.RoleExists("Admin"))
        //    {
        //        roles.CreateRole("Admin");
        //    }
        //    if (membership.GetUser("sallen", false) == null)
        //    {
        //        membership.CreateUserAndAccount("sallen", "imalittleteapot");
        //    }
        //    if (!roles.GetRolesForUser("sallen").Contains("Admin"))
        //    {
        //        roles.AddUsersToRoles(new[] { "sallen" }, new[] { "admin" });
        //    }
        }
    }
}
