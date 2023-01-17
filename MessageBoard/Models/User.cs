using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models 
{
  public class User 
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public int UserId { get; set; }

  }

}