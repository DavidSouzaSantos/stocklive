using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApiStockLive.Models
{
    public class Role : IdentityRole<int>
    {
        public List<UserRoles> UserRoles { get; set; }
    }
}
