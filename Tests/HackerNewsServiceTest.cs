//using CodingChallenge.Model;
//using CodingChallenge.service;
//using Xunit;

//namespace CodingChallenge.Tests
//{
//    public class HackerNewsServiceTest
//    {
//        private readonly IHackerNewsService _httpClientMock;
//        public HackerNewsServiceTest(IHackerNewsService httpClientMock)
//        {
//            _httpClientMock = httpClientMock;
//        }
//        [Fact]
//        public async Task GetStoryByIdAsync_ReturnsStory()
//        {
//            // Arrange
//            var story = new Story { Id = 1, Title = "Story 1", Url = "https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty" };

//            // Mock
//            _httpClientMock.Setup(client => client.GetFromJsonAsync<Story>(Any<string>()))
//                .ReturnsAsync(story);

//            // Act
//            var result = await _httpClientMock.GetStoryByIdAsync(1);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Story 1", result.Title);
//        }

//    }
//}
