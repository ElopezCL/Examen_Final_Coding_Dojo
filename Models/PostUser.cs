using System.ComponentModel.DataAnnotations;
namespace Artsgram.Models;

public class PostUser

{

    public User Usuario{get;set;} = new User();
    public Post Articulo { get; set; } = new Post();
    public List<Post>? Listado { get; set; }
   


}