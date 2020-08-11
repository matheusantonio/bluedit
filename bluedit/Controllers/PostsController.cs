using bluedit.Models.Entities;
using bluedit.Models.ViewModel.Posts;
using bluedit.Services;

using MongoDB.Bson;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace bluedit.Controllers
{
    [Authorize]
    [Route("bluedit/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postsService;
        private readonly SubForumService _subForumService;
        private readonly UserManager<MongoUser> _userManager;

        public PostsController(PostService postsService,
                               SubForumService subForumService,
                               UserManager<MongoUser> userManager)
        {
            _postsService = postsService;
            _subForumService = subForumService;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<List<Post>> Get() =>
            _postsService.Get();
        
        [HttpGet("{id:length(24)}", Name = "GetPost")]
        public ActionResult<Post> Get(string id)
        {
            var bsonId = ObjectId.Parse(id);

            var post = _postsService.Get(bsonId);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> Create([FromBody]CreatePostViewModel createPost)
        {
            var author = await _userManager.FindByNameAsync(
                HttpContext.User.FindFirstValue("username")
            );

            var subForum = _subForumService.GetByName(createPost.SubForum);

            var post = new Post
            {
                Title = createPost.Title,
                Content = createPost.Content,
                Tags = createPost.Tags,
                AuthorId = author.Id,
                Replies = new List<ObjectId>(),
                Upvotes = 1,
                SubForumId = subForum.Id,
                Time = DateTime.Now
            };

            

            _postsService.Create(post);

            return CreatedAtRoute("GetPost", new {id = post.Id.ToString()}, post);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Post postIn)
        {
            var bsonId = ObjectId.Parse(id);

            var post = _postsService.Get(bsonId);

            if (post == null)
            {
                return NotFound();
            }

            _postsService.Update(bsonId, postIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var bsonId = ObjectId.Parse(id);

            var post = _postsService.Get(bsonId);

            if (post == null)
            {
                return NotFound();
            }

            _postsService.Remove(post.Id);

            return NoContent();
        }
    }
}