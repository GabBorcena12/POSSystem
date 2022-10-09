using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class UserRegistration
    {
        [Required]
        [DataType(DataType.Text, ErrorMessage = "Username is not valid")]
        [Display(Name = "Username")]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Confirm E-mail is not valid")]
        [Display(Name = "Confirm Email Address")]
        public string ConfirmEmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Confirm Password is Required")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Text, ErrorMessage = "First Name is not valid")]
        [Display(Name = "First Name")]
        public string FirstNm { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Last Name is not valid")]
        [Display(Name = "Last Name")]
        public string LastNm  { get; set; }
    }
}