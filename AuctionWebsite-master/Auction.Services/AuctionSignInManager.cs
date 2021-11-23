using Auction.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Services
{
    public class AuctionSignInManager : SignInManager<AuctionUser, string>
    {
        public AuctionSignInManager(AuctionUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AuctionUser user)
        {
            return user.GenerateUserIdentityAsync((AuctionUserManager)UserManager);
        }

        public static AuctionSignInManager Create(IdentityFactoryOptions<AuctionSignInManager> options, IOwinContext context)
        {
            return new AuctionSignInManager(context.GetUserManager<AuctionUserManager>(), context.Authentication);
        }
    }
}
