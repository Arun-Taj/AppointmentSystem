using Microsoft.AspNetCore.Mvc;
using AppointmentSystem.Services;
using AppointmentSystem.Models;

namespace AppointmentSystem.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            var posts = _postService.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _postService.CreatePost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                _postService.UpdatePost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [HttpPost]
        public IActionResult Activate(int id)
        {
            _postService.ActivatePost(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Deactivate(int id)
        {
            _postService.DeactivatePost(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
