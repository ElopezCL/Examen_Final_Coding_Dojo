using System.ComponentModel.DataAnnotations;
namespace Artsgram.Models;

public class PostList

{

    public User Usuario{get;set;} = new User();
    public List<Post> ListPost { get; set; } = new List<Post>();


}