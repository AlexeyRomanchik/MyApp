using System;

namespace Models.User
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseOrFlat { get; set; }
    }
}
