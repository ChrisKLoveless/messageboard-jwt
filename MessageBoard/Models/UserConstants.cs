namespace MessageBoard.Models
{
  public class UserConstants
  {
    public static List<AppUser> Users = new List<AppUser>()
    {
      new AppUser() {EmailAddress = "chris.admin@email.com", Username = "chris_admin", Password = "MyPass_w0rd", Role = "Administrator" },
      new AppUser() {EmailAddress = "david.seller@email.com", Username = "david", Password = "david123", Role = "Seller" }
    };
  }
}