using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artsgram.Models;

public class UserDTO

{



    [Required(ErrorMessage = "El campo Apellido es obligatorio")]
    public string Email { get; set; }


    [Required(ErrorMessage = "El campo Apellido es obligatorio")]
   
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }



}