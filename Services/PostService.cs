using System.Collections.Generic;
using System.Linq;
using AppointmentSystem.Data;
using AppointmentSystem.Models;

namespace AppointmentSystem.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Find(id);
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }

        public void ActivatePost(int id)
        {
            var post = _context.Posts.Find(id);
            post.IsActive = true;
            _context.SaveChanges();
        }

        public void DeactivatePost(int id)
        {
            var post = _context.Posts.Find(id);
            var activeOfficers = _context.Officers.Any(o => o.PostId == id && o.IsActive);
            if (!activeOfficers)
            {
                post.IsActive = false;
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Cannot deactivate post with active officers.");
            }
        }
    }
}
