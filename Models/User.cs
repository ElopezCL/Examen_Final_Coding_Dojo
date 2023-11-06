using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artsgram.Models;

public class User

{

    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "El campo Nombre es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El campo nombre debe tener un mínimo de 2 caracteres")]
    public string Name { get; set; }



    [Required(ErrorMessage = "El campo Apellido es obligatorio")]
    [EmailAddress(ErrorMessage = "El campo Email no tiene un formato de dirección de correo electrónico válido")] 
    public string Email { get; set; }


    [Required(ErrorMessage = "El campo Apellido es obligatorio")]
    [MinLength(8, ErrorMessage = "La longitud mínima de la contraseña es 8 caracteres")]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [NotMapped]
    [Compare("Password")]

    public string ConfirmPassword { get; set; }

    public DateTime CreatedAt { get; set; } 

    public DateTime UpdateAt { get; set; }
     public List<Post>? ListaPosts { get; set; }
        public List<Like>? Likes { get; set; }

}