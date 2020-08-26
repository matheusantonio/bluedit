using bluedit.Models.Entities;
using bluedit.Models.ViewModel.Posts;
using bluedit.Models.ViewModel.Reply;
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
    [Route("bluedit/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postsService;
        private readonly SubForumService _subForumService;
        private readonly ReplyService _replyService;
        private readonly UserManager<MongoUser> _userManager;

        public PostsController(PostService postsService,
                               SubForumService subForumService,
                               ReplyService replyService,
                               UserManager<MongoUser> userManager)
        {
            _postsService = postsService;
            _subForumService = subForumService;
            _replyService = replyService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostPreviewViewModel>>> Get()
        {
            var posts = _postsService.Get();

            var returnPosts = new List<PostPreviewViewModel>();

            foreach(Post post in posts)
            {
                var author = await _userManager.FindByIdAsync(post.AuthorId);

                returnPosts.Add(
                    new PostPreviewViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Tags = post.Tags,
                        Author = author.UserName,
                        Time = post.Time,
                        Upvotes = post.Upvotes
                    }
                );
            }

            return returnPosts;
        }
        
        private async Task<List<ReplyViewModel>> ListReplies(List<string> replyIds)
        {
            var viewReplies = new List<ReplyViewModel>();

            foreach(string id in replyIds)
            {
                var reply = _replyService.Get(id);
                var author = await _userManager.FindByIdAsync(reply.AuthorId);
                var replies = await ListReplies(reply.Replies);

                viewReplies.Add(
                    new ReplyViewModel
                    {
                        Id = reply.Id,
                        Author = author.UserName,
                        Content = reply.Content,
                        Replies = replies,
                        Time = reply.Time,
                        Upvotes = reply.Upvotes
                    });
            }

            return viewReplies;
        }
        
        [HttpGet("{id:length(24)}", Name = "GetPost")]
        public async Task<ActionResult<PostViewModel>> Get(string id)
        {
            var post = _postsService.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            var subForum = _subForumService.Get(post.SubForumId);
            

            var author = await _userManager.FindByIdAsync(post.AuthorId);

            if(subForum == null)
            {
                subForum = new SubForum{
                    Name = author.UserName
                };
            }

            var replies = await ListReplies(post.Replies);

            var posts = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Tags = post.Tags,
                SubForum = subForum.Name,
                Author = author.UserName,
                Content = post.Content,
                Replies = replies,
                Time = post.Time,
                Upvotes = post.Upvotes
            };

            return posts;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Post>> Create([FromBody]CreatePostViewModel createPost)
        {
            var author = await _userManager.FindByNameAsync(
                HttpContext.User.FindFirstValue("username")
            );

            string subForumId = null;

            if(createPost.SubForum != null)
            {
                var subForum = _subForumService.GetByName(createPost.SubForum);

                if(subForum == null)
                {
                    return NotFound(createPost.SubForum);
                }

                subForumId = subForum.Id;
            }

            var post = new Post
            {
                Title = createPost.Title,
                Content = createPost.Content,
                Tags = createPost.Tags,
                AuthorId = author.Id.ToString(),
                SubForumId = subForumId,
                Replies = new List<string>(),
                Upvotes = 1,
                Time = DateTime.Now
            };

            _postsService.Create(post);

            return CreatedAtRoute("GetPost", new {id = post.Id.ToString()}, post);
        }

        [Authorize]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Post postIn)
        {
            var post = _postsService.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            _postsService.Update(id, postIn);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var post = _postsService.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            _postsService.Remove(post.Id);

            return NoContent();
        }
    }
}