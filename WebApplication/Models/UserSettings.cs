namespace WebApplication.Models
{
    public class UserSettings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public bool ReceiveProductNotifications { get; set; }
        public string UserImageUrl { get; set; }
    }
}
