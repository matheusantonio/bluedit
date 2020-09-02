using bluedit.Services;
using bluedit.Models.Entities;
using bluedit.Models.ViewModel.Upvotes;

using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace bluedit.Controllers
{
    [Route("bluedit/posts/[controller]")]
    [ApiController]
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
        [HttpGet("{id:length(24)}")]
        public async Task<bool?> Get(string id)
        {            
            var currentUser = await _userManager.FindByNameAsync(
                HttpContext.User.FindFirstValue("username")
            );

            if(currentUser == null) return null;

            var upvote = _upvoteService.GetByPostAndUser(currentUser.Id.ToString(), id);

            if(upvote == null) return null;

            if(upvote.IsUpvote) return true;
            else return false;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UpdateUpvotesViewModel>> Create([FromBody] CreateUpvoteViewModel createdUpvote)
        {
            Postable post;

            if(createdUpvote.isReply)
            {
                post = _replyService.Get(createdUpvote.postId);

            } else {
                post = _postService.Get(createdUpvote.postId);
            }

            if(post == null) return BadRequest();

            var user = await _userManager.FindByNameAsync(
                HttpContext.User.FindFirstValue("username")
            );

            var upvote = _upvoteService.GetByPostAndUser(user.Id.ToString(), createdUpvote.postId);

            int postUpvoteCountAdd = 0;

            bool? upResult = createdUpvote.isUp;

            if(upvote == null)
            {
                var newUpvote = new Upvote
                {
                    UserId = user.Id.ToString(),
                    PostId = createdUpvote.postId,
                    IsUpvote = createdUpvote.isUp
                };

                upvote = _upvoteService.Create(newUpvote);

                postUpvoteCountAdd = createdUpvote.isUp ? 1 : -1;
            }
            else if(upvote != null && upvote.IsUpvote != createdUpvote.isUp)
            {
                upvote.IsUpvote = createdUpvote.isUp;

                _upvoteService.Update(upvote.Id, upvote);

                postUpvoteCountAdd = createdUpvote.isUp ? 2 : -2;
            }
            else if(upvote != null && upvote.IsUpvote == createdUpvote.isUp)
            {
                _upvoteService.Remove(upvote);
                
                postUpvoteCountAdd = createdUpvote.isUp ? -1 : 1;
                upResult = null;
            }

            if(postUpvoteCountAdd != 0)
            {
                post.Upvotes += postUpvoteCountAdd;

                if(createdUpvote.isReply)
                {
                    _replyService.Update(post.Id, (Reply) post);
                } else {
                    _postService.Update(post.Id, (Post) post);
                }
            }

            return Ok(new UpdateUpvotesViewModel
                {
                    UpdatedCount = post.Upvotes,
                    IsUp = upResult
                });
            
        }

    }
}