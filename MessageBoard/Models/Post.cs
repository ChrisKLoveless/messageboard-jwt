using System.ComponentModel.DataAnnotations;

namespace MessageBoard.Models 
{
  public class Post 
  {
    public string Body { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int ThreadsId { get; set; }
    [Required]
    public int PostId { get; set; }
  }

}