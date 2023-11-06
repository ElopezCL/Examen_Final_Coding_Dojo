using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Artsgram.Models;


namespace Artsgram.Controllers;

public class LikesController : Controller
{
    private readonly ArtsContext _context;

    public LikesController(ArtsContext context)
    {
        _context = context;
    }
    [HttpPost("Likes/AddLike/")]
    public IActionResult AgregarLike(Like NuevoLike) 
    {
        
        int? UserID = HttpContext.Session.GetInt32("UserId");


        if(UserID == null || NuevoLike.UserId !=  UserID){

            return RedirectToAction("Index", "Posts");
        }

        if (ModelState.IsValid)
        {

        
         
            _context.Likes.Add(NuevoLike);
            _context.SaveChanges();

            
            return RedirectToAction("Index","Posts");

        }


        return View("Index", "Posts");

    }


    }


