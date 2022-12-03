namespace BotTests;

public class JoinedResponseMakerTests
{
    private readonly JoinedResponseMaker _service;
    private Mock<IJoinedRequestParser> _joinedRequestParserMock;

    public JoinedResponseMakerTests()
    {
        _joinedRequestParserMock = new Mock<IJoinedRequestParser>();
        _service = new JoinedResponseMaker(_joinedRequestParserMock.Object);
    }

    [Fact]
    public void GetResponse_Calls_RequestParser()
    {
        _service.GetResponse("");
        
        _joinedRequestParserMock.Verify(x => x.GetUserName(It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public void GetResponse_Calls_RequestParser_CorrectContent()
    {
        var content = "hello";
        _service.GetResponse(content);
        
        _joinedRequestParserMock.Verify(x => x.GetUserName(content), Times.Once);
    }
}