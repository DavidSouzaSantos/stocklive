using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<UserRoles> UserRoles { get; set; }
    }
}
