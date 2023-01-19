namespace MessageBoard.Models
{
    public class AppUser
    {
        public int AppUserId {get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}