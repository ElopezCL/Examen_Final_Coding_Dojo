using Artsgram.Filters;
using Artsgram.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

namespace Artsgram.Controllers;
[SessionCheck]
public class PostsController : Controller
{


    private readonly ArtsContext _context;

    public PostsController(ArtsContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        int? userID = HttpContext.Session.GetInt32("UserId");
        User user = _context.Users.FirstOrDefault(a => a.UserId == userID);



        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }



       List<Post> posts = _context.Posts
    .Include(post => post.User)
    .Include(post => post.Likes) // Incluir la colección de likes para cada post
    .ToList();

var TodosLosPosts = posts
    .Select(post => new
    {
        Post = post,
        UserName = post.User.Name,
        UserId = post.User.UserId,
        UserSession = userID,
        TotalLikes = post.Likes.Count  // Obtener la cantidad de likes para este post
    })
    .OrderByDescending(item => item.Post.CreatedAt) // Ordenar por la fecha de creación
    .ToList();



        return View("Index", TodosLosPosts);
    }


    [HttpGet("Posts/AddPost")]

    public ViewResult Add()
    {

        return View("AddPosts");
    }


    [HttpPost("Posts/AddPost/NewPost")]
    public IActionResult AgregarPost([Bind(Prefix = "Articulo")] Post NuevoPost)
    {

        int? UserID = HttpContext.Session.GetInt32("UserId");
        User? user = _context.Users.FirstOrDefault(a => a.UserId == UserID);


        if (ModelState.IsValid && User != null)
        {

            NuevoPost.User = user;
            NuevoPost.CreatedAt = DateTime.Now;
            NuevoPost.UpdateAt = DateTime.Now;
            _context.Posts.Add(NuevoPost);
            _context.SaveChanges();


            return RedirectToAction("Index");

        }

        PostUser postUser = new PostUser();
        postUser.Articulo = NuevoPost;

        return View("AddPosts", postUser);

    }


    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }


    [HttpPost("delete/{PostId}")]
    public IActionResult EliminarPost(int PostId)
    {
        Post BorrarPost = _context.Posts.SingleOrDefault(d => d.PostId == PostId);
        if (BorrarPost != null)
        {
            _context.Posts.Remove(BorrarPost);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }


    [HttpGet("Post/{PostId}")]
    public IActionResult ViewPost(int PostId)

    {
        int? UserID = HttpContext.Session.GetInt32("UserId");
        User? user = _context.Users.FirstOrDefault(a => a.UserId == UserID);

        var singlePost = _context.Posts
            .Include(post => post.User)
                .Where(post => post.PostId == PostId)
                    .Select(post => new
                    {
                        Post = post,
                        UserName = post.User.Name,
                        UserId = post.User.UserId,
                        UserSession = UserID
                    })
                        .FirstOrDefault();



        return View("ViewPost", singlePost);
    }






    [HttpGet("Post/edit/{PostId}")]
    public IActionResult EditPost(int PostId)
    {
        Post ObtenerPost = _context.Posts.SingleOrDefault(d => d.PostId == PostId);
        PostUser Item = new PostUser();
        Item.Articulo = ObtenerPost;


        return View("NewPost", Item);
    }


    [HttpPost("Post/update/{PostId}")]


    public IActionResult Update([Bind(Prefix = "Articulo")] Post post, int PostId)
    {
        Console.WriteLine("estoy aquiiiii!" + post.Title);



        Post? updtPost = _context.Posts.FirstOrDefault(d => d.PostId == PostId);

        if (updtPost == null)
        {

            return RedirectToAction("Index");

        }

        if (ModelState.IsValid)
        {
            updtPost.URL = post.URL;
            updtPost.Title = post.Title;
            updtPost.Medium = post.Medium;
            updtPost.ForSale = post.ForSale;
            updtPost.UpdateAt = DateTime.Now;
            _context.Update(updtPost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        PostUser postUser = new PostUser();
        postUser.Articulo = updtPost;
        return View("AddPosts", postUser);
    }



}




