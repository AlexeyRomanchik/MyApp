using System;

namespace WebApplication.Models
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public int Code { get; set; }
        public DateTime SendingDate { get; set; }
        public int AttemptsNumber { get; set; }
        public DateTime NextSendingDate { get; set; }
    }
}
