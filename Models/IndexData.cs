using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Artsgram.Models;

public class IndexData

{

    public User Register{get;set;} = new User();
    public UserDTO Login {get;set;} = new UserDTO();

}