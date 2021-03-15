using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiStockLive.Enums;

namespace WebApiStockLive.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UserRoles> UserRoles { get; set; }
        public StatusEnum Status { get; set; }
    }
}
