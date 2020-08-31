using bluedit.Services;
using bluedit.Models.Entities;

using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Threading.Tasks;

namespace bluedit.Controllers
{
    [Route("bluedit/posts/[controller]")]
    public class UpvotesController : ControllerBase
    {

        private readonly UpvoteService _upvoteService;
        private readonly PostService _postService;
        private readonly ReplyService _replyService;
        private readonly UserManager<MongoUser> _userManager;

        public UpvotesController(UpvoteService upvoteService,
                                 PostService postService,
                                 ReplyService replyService,
                                 UserManager<MongoUser> userManager)
        {
            _upvoteService = upvoteService;
            _postService = postService;
            _replyService = replyService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]string postId, bool isUp, bool isReply)
        {
            Postable post;

            if(isReply)
            {
                post = _replyService.Get(postId);

            } else {
                post = _postService.Get(postId);
            }

            if(post == null) return BadRequest();

            var user = await _userManager.FindByNameAsync(
                HttpContext.User.FindFirstValue("username")
            );

            var upvote = _upvoteService.GetByPostAndUser(user.Id.ToString(), postId);

            int postUpvoteCountAdd = 0;

            if(upvote == null)
            {
                var newUpvote = new Upvote
                {
                    UserId = user.Id.ToString(),
                    PostId = postId,
                    IsUpvote = isUp
                };

                upvote = _upvoteService.Create(newUpvote);

                postUpvoteCountAdd = isUp ? 1 : -1;
            }
            else if(upvote != null && upvote.IsUpvote != isUp)
            {
                upvote.IsUpvote = isUp;

                _upvoteService.Update(upvote.Id, upvote);

                postUpvoteCountAdd = isUp ? 1 : -1;
            }

            if(postUpvoteCountAdd != 0)
            {
                post.Upvotes += postUpvoteCountAdd;

                if(isReply)
                {
                    _replyService.Update(post.Id, (Reply) post);
                } else {
                    _postService.Update(post.Id, (Post) post);
                }
            }

            return Ok();
            
        }


    }
}