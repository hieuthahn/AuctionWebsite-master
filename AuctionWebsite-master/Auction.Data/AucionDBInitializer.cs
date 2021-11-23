using Auction.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Data
{
    public class AucionDBInitializer : CreateDatabaseIfNotExists<AuctionContext>
    {
        protected override void Seed(AuctionContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
        }

        public void SeedRoles (AuctionContext context)
        {
            List<IdentityRole> rolesInAuction = new List<IdentityRole>();
            rolesInAuction.Add(new IdentityRole() { Name = "Adminstrator" });
            rolesInAuction.Add(new IdentityRole() { Name = "Moderator" });
            rolesInAuction.Add(new IdentityRole() { Name = "User" });

            var rolesStore = new RoleStore<IdentityRole>(context);
            var rolesManager = new RoleManager<IdentityRole>(rolesStore);

            foreach (IdentityRole role in rolesInAuction)
            {
                if (!rolesManager.RoleExists(role.Name))
                {
                    var result = rolesManager.Create(role);
                    
                    if (result.Succeeded) continue;
                }
            }
        }

        public void SeedUsers(AuctionContext context)
        {
            var usersStore = new UserStore<AuctionUser>(context);
            var usersManager = new UserManager<AuctionUser>(usersStore);
            AuctionUser admin = new AuctionUser();
            admin.Email = "admin@gmail.com";
            admin.UserName = "admin";
            var password = "admin123";

            if (usersManager.FindByEmail(admin.Email) == null)
            {
                var result = usersManager.Create(admin, password);
                if(result.Succeeded)
                {
                    //add necessary roles to admin
                    usersManager.AddToRole(admin.Id, "Adminstrator");
                    usersManager.AddToRole(admin.Id, "Moderator");
                    usersManager.AddToRole(admin.Id, "User");
                }
            }
        }
    }

}
