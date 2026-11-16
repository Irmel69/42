using Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Classes
{
    public class CurrentUser
    {
        public string UserID { get; set; } = string.Empty;
        
        public string UserName { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public bool IsAuthenticated { get; set; } = false;

        public RoleType Role { get; set; } = RoleType.Guest;
    }
}
