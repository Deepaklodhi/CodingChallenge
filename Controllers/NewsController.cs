using CodingChallenge.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public NewsController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("newest")]
        public async Task<IActionResult> GetNewestStories()
        {
            try
            {
                var stories = await _hackerNewsService.GetNewestStoriesAsync();
                return Ok(stories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching stories.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoryById(int id)
        {
            try
            {
                var story = await _hackerNewsService.GetStoryByIdAsync(id);
                if (story == null)
                {
                    return NotFound($"Story with ID {id} not found.");
                }
                return Ok(story);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the story.");
            }
        }
    }

}
