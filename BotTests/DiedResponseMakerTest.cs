using Assert = NUnit.Framework.Assert;

namespace BotTests;

public class DiedResponseMakerTest
{
    private readonly DiedResponseMaker _service;
    private Mock<IRandomDeathInsultRepo> _insultRepoMock;
    private Mock<IDiedRequestParser> _parserMock;

    public DiedResponseMakerTest()
    {
        _insultRepoMock = new Mock<IRandomDeathInsultRepo>();
        _parserMock = new Mock<IDiedRequestParser>();
        _service = new DiedResponseMaker(_parserMock.Object, _insultRepoMock.Object, new Mock<IUserRepository>().Object);
    }

    [Fact]
    public async Task Returns_MergedTemplate()
    {
        _parserMock.Setup(x => x.GetUserName(It.IsAny<string>())).Returns("alex");
        _insultRepoMock.Setup(x => x.GetInsult()).Returns("{0} is cool");

        var response = await _service.GetResponse("");
        
        Assert.AreEqual("alex is cool", response);
    }
}