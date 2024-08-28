using CodingChallenge.Model;

namespace CodingChallenge.service
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<Story>> GetNewestStoriesAsync();
        Task<Story> GetStoryByIdAsync(int id);
    }

}
