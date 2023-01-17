namespace MessageBoard.Models 
{
  public class Post 
  {
    public string Body { get; set; }

    public int UserId { get; set; }

    public int ThreadsId { get; set; }

    public int PostId { get; set; }

  }

}