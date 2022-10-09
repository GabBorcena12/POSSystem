using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class UserAuthentication:BaseMessage
    {
        public string EmployeeName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string isActive { get; set; }
        public string LoginFailedCount { get; set; }
        public string isAuthenticated { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}