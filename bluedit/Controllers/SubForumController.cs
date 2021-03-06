using bluedit.Models.Entities;
using bluedit.Models.ViewModel.SubForum;
using bluedit.Models.ViewModel.Posts;
using bluedit.Services;

using Microsoft.AspNetCore.Mvc;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using MongoDB.Bson;

using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace bluedit.Controllers
{
    [Route("bluedit/[controller]")]
    [ApiController]
    public class SubForumController : ControllerBase
    {

        private readonly SubForumService _subForumService;
        private readonly PostService _postsService;
        private readonly ReplyService _replyService;
        private readonly UserManager<MongoUser> _userManager;

        public SubForumController(
            SubForumService subForumService,
            PostService postService,
            ReplyService replyService,
            UserManager<MongoUser> userManager)
        {
            _subForumService = subForumService;
            _postsService = postService;
            _replyService = replyService;
            _userManager = userManager;
        }

        [HttpGet("{id:length(24)}", Name = "GetSubForum")]
        public ActionResult<SubForum> Get(string id)
        {
            var subForum = _subForumService.Get(id);

            if (subForum == null)
            {
                return NotFound();
            }

            return subForum;
        }

        [HttpGet]
        public async Task<ActionResult<SubForumHomeViewModel>> GetByName([FromQuery]string name)
        {
            if(name == null) return Ok(_subForumService.Get());

            var subForum = _subForumService.GetByName(name);

            var posts = _postsService.GetBySubForum(subForum.Id);

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

            return new SubForumHomeViewModel{
                Name = subForum.Name,
                Description = subForum.Descrition,
                posts = returnPosts,
                Id = subForum.Id
            };
        }

        [HttpGet("top")]
        public ActionResult<List<string>> TopSubForums()
        {
            return _postsService.GetTopSubForums()
                    .Select((subId) => 
                        {
                            var subForum = _subForumService.Get(subId);
                            if(subForum == null) return "";
                            return subForum.Name;
                        })
                    .ToList();
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SubForum>> Create([FromBody] CreateSubForumViewModel createSubForum)
        {
            var subForum = _subForumService.GetByName(createSubForum.Name);

            if(subForum != null) return Conflict();

            var author = await _userManager.FindByNameAsync(
                HttpContext.User.FindFirstValue("username"));
            
            subForum = new SubForum
            {
                Name = createSubForum.Name,
                Descrition = createSubForum.Description,
                creatorId = author.Id.ToString()
            };

            _subForumService.Create(subForum);

            return CreatedAtRoute("GetSubForum", new {id = subForum.Id}, subForum);
        }
    }
}