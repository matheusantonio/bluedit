using bluedit.Models;
using bluedit.Models.Entities;
using bluedit.Services;
using Microsoft.AspNetCore.Mvc;

namespace bluedit.Controllers
{
    [Route("bluedit/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly ReplyService _replyService;

        public ReplyController(ReplyService replyService)
        {
            _replyService = replyService;
        }
    }
}