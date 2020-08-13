using bluedit.Services;

using Microsoft.AspNetCore.Mvc;

namespace bluedit.Controllers
{
    public class UpvotesController : ControllerBase
    {

        private readonly UpvoteService _upvoteService;

        public UpvotesController(UpvoteService upvoteService)
        {
            _upvoteService = upvoteService;
        }

    }
}