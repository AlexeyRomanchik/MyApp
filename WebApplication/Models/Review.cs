using System;

namespace WebApplication.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool ReviewVerified { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
