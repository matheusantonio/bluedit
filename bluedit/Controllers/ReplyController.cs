using bluedit.Models.Entities;
using bluedit.Services;
using bluedit.Models.ViewModel.Reply;

using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System;

namespace bluedit.Controllers
{
    [Route("bluedit/posts/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly ReplyService _replyService;
        private readonly PostService _postService;
        private readonly UserManager<MongoUser> _userManager;

        public ReplyController(ReplyService replyService,
                               PostService postService,
                               UserManager<MongoUser> userManager)
        {
            _replyService = replyService;
            _postService = postService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Reply>> Create([FromBody]NewReplyViewModel newReplyViewModel)
        {
            if(newReplyViewModel.PostId != null &&
                newReplyViewModel.ReplyId != null)
            {
                return BadRequest();
            }

            Postable post;
            
            if(newReplyViewModel.PostId != null)
            {
                post = _postService.Get(newReplyViewModel.PostId);
            } else if(newReplyViewModel.ReplyId != null)
            {
                post = _replyService.Get(newReplyViewModel.ReplyId);
            } else
            {
                return BadRequest();
            }

            var author = await _userManager.FindByNameAsync(
                HttpContext.User.FindFirstValue("username")
            );

            var reply = new Reply
            {
                AuthorId = author.Id.ToString(),
                Content = newReplyViewModel.Content,
                Replies = new List<string>(),
                Time = DateTime.Now,
                Upvotes = 1
            };

            var newReply = _replyService.Create(reply);

            post.Replies.Add(newReply.Id);

            if(newReplyViewModel.PostId != null) _postService.Update(post.Id, (Post) post);
            else _replyService.Update(post.Id, (Reply) post);

            return newReply;
        }

    }
}