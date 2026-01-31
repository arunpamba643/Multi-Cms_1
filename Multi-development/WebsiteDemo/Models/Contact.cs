namespace WebsiteDemo.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string AvatarImageUrl { get; set; }
        public string Description { get; set; } = "";

    }
}
