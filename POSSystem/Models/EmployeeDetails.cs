using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class EmployeeDetails
    {
        public string Id { get; set; }
        [DataType(DataType.Text, ErrorMessage = "First Name is not valid")]
        public string FirstNm { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Middle Name is not valid")]
        public string MiddleNm { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Last Name is not valid")]
        public string LastNm { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Full Name is not valid")]
        public string FullNm { get; set; }
        [DataType(DataType.Text, ErrorMessage = "E-mail is not valid")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Role { get; set; }
        public string IsActive { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Contact Number is not valid")]
        public string PhoneNumber { get; set; }
         
    }
}