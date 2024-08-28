using CodingChallenge.Model;
using Microsoft.Extensions.Caching.Memory;

namespace CodingChallenge.service
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<HackerNewsService> _logger;
        private const string NewestStoriesUrl = "https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty";
        private const string StoryUrlTemplate = "https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty";

        public HackerNewsService(HttpClient httpClient, IMemoryCache cache, ILogger<HackerNewsService> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
        }
        public async Task<IEnumerable<Story>> GetNewestStoriesAsync()
        {
            var cacheKey = "newest_stories";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Story> stories))
            {
                try
                {
                    var storyIds = await _httpClient.GetFromJsonAsync<IEnumerable<int>>(NewestStoriesUrl);
                    var tasks = storyIds.Take(200).Select(id => GetStoryAsync(id));
                    stories = await Task.WhenAll(tasks);
                    _cache.Set(cacheKey, stories, TimeSpan.FromMinutes(10));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching stories from Hacker News API");
                    throw;
                }
            }
            return stories;
        }

        public async Task<Story> GetStoryByIdAsync(int id)
        {
            var cacheKey = $"story_{id}";
            if (!_cache.TryGetValue(cacheKey, out Story story))
            {
                try
                {
                    story = await GetStoryAsync(id);

                    _cache.Set(cacheKey, story, TimeSpan.FromMinutes(10));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error fetching story with ID {id} from Hacker News API");
                    throw;
                }
            }
            return story;
        }

        private async Task<Story> GetStoryAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Story>(string.Format(StoryUrlTemplate, id));
        }
    }

}
