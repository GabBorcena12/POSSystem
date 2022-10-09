using POSSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.ViewModel
{
    public class ViewFormViewModel
    {
        public UserAuthentication userAuthentication { get; set; }
        public TransactionInquiry transactionInquiry { get; set; }
        public UserRegistration userRegistration { get; set; }
        public EmployeeDetails employeeDetails { get; set; }
    }
}