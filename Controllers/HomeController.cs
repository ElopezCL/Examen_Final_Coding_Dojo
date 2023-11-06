using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Artsgram.Models;


namespace Artsgram.Controllers;

public class HomeController : Controller
{
    private readonly ArtsContext _context;

    public HomeController(ArtsContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        IndexData Data = new IndexData();

        return View(Data);
    }

    [HttpPost("register")]
    public IActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {

            PasswordHasher<User> Hasher = new PasswordHasher<User>();

            user.Password = Hasher.HashPassword(user, user.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdateAt = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        IndexData hola = new IndexData();
        hola.Register = user;
        return View("Index", hola);
    }


    [HttpPost("login")]
    public IActionResult Login([Bind(Prefix = "Login")] UserDTO user)
    {
        if (ModelState.IsValid)
        {
            User? usuario = _context.Users.FirstOrDefault(h => h.Email == user.Email);

            if (usuario == null)
            {
                return RedirectToAction("Index");
            }


            PasswordHasher<UserDTO> Hasher = new PasswordHasher<UserDTO>();
            var result = Hasher.VerifyHashedPassword(user, usuario.Password, user.Password);

            if(result != 0){
             HttpContext.Session.SetInt32("UserId", usuario.UserId);
             return RedirectToAction("Index", "Posts");
            }

           

         
        }
    

        IndexData hola = new IndexData();
        hola.Login = user;
        return View("Index", hola);

    }
}
