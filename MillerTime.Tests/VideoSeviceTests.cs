using MillerTime.Models.DBModels;
using MillerTime.DAL.Repositories.Interfaces;
using MillerTime.Services;
using MillerTime.Services.Interfaces;
using Moq;
using Xunit;

namespace MillerTime.Tests
{
    public class VideoSeviceTests
    {

        private readonly IVideoService _classBeingTested;
        private Mock<IVideoRepository> _mockVideoRepo;
        private readonly string _validurl = "https://www.youtube.com/watch?v=elevenchars";

        public VideoSeviceTests()
        {
            _mockVideoRepo = new Mock<IVideoRepository>();
            _classBeingTested = new VideoService(_mockVideoRepo.Object);
        }

        [Theory]
        [InlineData("https://www.youtube.com/watch?v=elevenchars")]
        [InlineData("https://youtube.com/watch?v=elevenchars")]
        [InlineData("https://m.youtube.com/watch?v=elevenchars")]
        public void FormatVideoURL_FormatsURL_Successful(string embedURL)
        {
            var actualURL = _classBeingTested.FormatVideoURL(embedURL);
            var expectedURL = "https://www.youtube.com/embed/elevenchars";
            Assert.Equal(expectedURL, actualURL);
        }

        [Fact]
        public void FormatVideoURL_VideoIDNotElevenCharacters_ThrowsException()
        {
            var invalidIdURL = "https://m.youtube.com/watch?v=notelevencharacters";
            Assert.Throws<Exception>(() => _classBeingTested.FormatVideoURL(invalidIdURL));
        }

        [Fact]
        public void FormatVideoURL_EmbedURLIncorrectFormat_ThrowsException()
        {
            var invalidFormatURL = "https://m.notValid.com/watch?v=elevenchars";
            Assert.Throws<Exception>(() => _classBeingTested.FormatVideoURL(invalidFormatURL));
        }

        [Fact]
        public async void AddVideo_PassesFormatting_SavesCorrectly()
        {
            var video = Helpers.CreateVideo(userId: 3, embedUrl: _validurl);
            await _classBeingTested.AddVideo(video);
            var expectedEmbedURL = "https://www.youtube.com/embed/elevenchars";
            var expectedUserId = 3;
            var expectedId = 0;
            var expectedIsApproved = false;

            _mockVideoRepo.Verify(prop => prop.AddVideo(It.Is<Video>(vid =>
                vid.Id == expectedId &&
                vid.EmbedUrl == expectedEmbedURL &&
                vid.UserId == expectedUserId &&
                vid.IsApproved == expectedIsApproved
                )), Times.Once);
        }
    }
}