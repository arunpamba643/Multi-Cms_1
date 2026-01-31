namespace WebsiteDemo.Models
{
    public class Honoree
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Role { get; set; }
        public string AvatarImageUrl { get; set; }
        public string PhoneNumber { set; get; }
        public string EmailAddress { set; get; }
    }
}
