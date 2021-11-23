﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Entities
{
    public class AuctionUser : IdentityUser
    {

        public int Age { get; set; }
        public string Mobile { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNet.Identity.UserManager<AuctionUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
