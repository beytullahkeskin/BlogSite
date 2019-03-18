using BlogSite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogSite.Controllers
{
    public class PostController : ApiController
    {
        BlogSiteEntities db;
        public PostController()
        {
            db = new BlogSiteEntities();
        }
        [HttpPost]
        public IHttpActionResult Add(Post post)
        {
            if(string.IsNullOrWhiteSpace(post.Content))
            {
                return Json("Post name cannot be empty!");
            }
            db.Post.Add(post);
            db.SaveChanges();
            return Json("post added");
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Post deletedPost = db.Post.Find(id);
                db.Post.Remove(deletedPost);
                db.SaveChanges();
                return Json("Post deleted");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                
            }
        }
        [HttpPut]
        public IHttpActionResult Update(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Content))
            {
                return Json("Post name cannot be empty!");
            }
            Post updatedPost = db.Post.Find(post.PostId);
            updatedPost.Content = post.Content;
            updatedPost.İmage = post.İmage;
            db.SaveChanges();
            return Json("Post update");
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Post getPost = db.Post.Find(id);
                if (getPost == null)
                {
                    return Json("Post not found");
                }
                PostDto p = new PostDto();
                p.City = getPost.City.CityName;
                p.Content = getPost.Content;
                p.Image = getPost.İmage;
                return Json(p);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
          
        }

        public IHttpActionResult GetAll()
        {
            try
            {
                List<PostDto> posts = new List<PostDto>();
                foreach (Post item in db.Post)
                {
                    PostDto P = new PostDto();
                    P.City = item.City.CityName;
                    P.Content = item.Content;
                    P.Image = item.İmage;
                    posts.Add(p);
                }
                return Json(posts);
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
           
            
        }
    }
}
