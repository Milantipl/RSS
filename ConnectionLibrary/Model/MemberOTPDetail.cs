using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionLibrary.Model
{
   public class MemberOTPDetail
    {
        public int OTP { get; set; }
        public int ID { get; set; }
        public string MobileNo { get; set; }
        public string IPAddress { get; set; }
        public string OTPGDateTime { get; set; }
        public bool IsSend { get; set; }
        public bool IsVerified { get; set; }
        public int LoginId { get; set; }
        public int Roleid { get; set; }
        public string Username { get; set; }

    }
}
