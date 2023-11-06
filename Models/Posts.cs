using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artsgram.Models;

public class Post

{

    [Key]
    public int PostId { get; set; }


    public string URL {get;set;}

    [Required(ErrorMessage = "El titulo es obligatorio")]
    public string Title {get;set;}

    [Required(ErrorMessage = "El campo Medium es obligatorio")]
    public string Medium {get;set;}

    public Boolean ForSale{get;set;}
    


    public DateTime CreatedAt { get; set; } 

    public DateTime UpdateAt { get; set; }

    public User? User { get; set; }

    public List<Like>? Likes { get; set; }
   

   

}