using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSSystem.Models
{
    public class Chat
    {
        public string Id { get; set; }
        public string ReceiverNm { get; set; }
        public string SenderNm { get; set; }
        public string SenderUserNm { get; set; }
        public string ReceiverUserNm { get; set; }
        public string Message { get; set; }
        public string Datetime { get; set; }
        public string ImagePath { get; set; }
        public string You { get; set; }
        public string Notification { get; set; }
    }
}