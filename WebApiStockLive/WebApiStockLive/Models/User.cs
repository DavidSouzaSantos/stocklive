using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebApiStockLive.Enums;

namespace WebApiStockLive.Models
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public List<UserRoles> UserRoles { get; set; }
        public StatusEnum Status { get; set; }
    }
}
