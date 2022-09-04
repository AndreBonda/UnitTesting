using Moq;
using NUnit.Framework;
using TestNinjaCore.Mocking;
using TestNinjaCore.Mocking.VideoServiceExample;

namespace Ninja.UnitTests.VideoServiceTest;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _videoService;
    private Mock<IFileReader> _fileReader;
    private Mock<IConverter<Video>> _vConverter;

    [SetUp]
    public void SetUp()
    {
        _fileReader = new Mock<IFileReader>();
        _vConverter = new Mock<IConverter<Video>>();
        _videoService = new VideoService(_fileReader.Object, _vConverter.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
    {
        _fileReader
            .Setup(fr => fr.ReadFileContent(It.IsAny<string>()))
            .Returns("");

        string message = _videoService.ReadVideoTitle("video.txt");
        
        Assert.That(message, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void ReadVideoTitle_ExistingFile_ReturnVideoTitle()
    {
        _fileReader
            .Setup(fr => fr.ReadFileContent(It.IsAny<string>()))
            .Returns("");
        _vConverter
            .Setup(vc => vc.Deserialize(It.IsAny<string>()))
            .Returns(new Video
            {
                Title = "title_a"
            });

        string title = _videoService.ReadVideoTitle("video.txt");
        
        Assert.That(title, Is.EqualTo("title_a").IgnoreCase);
    }
}