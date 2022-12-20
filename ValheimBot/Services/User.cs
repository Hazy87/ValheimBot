using Amazon.DynamoDBv2.DataModel;

namespace ValheimBot.Services;

[DynamoDBTable("valheimbot-stats")]
public class User
{
    [DynamoDBHashKey]
    public string UserId { get; set; }
    public DateTime LastLogin { get; set; }
    public List<string> Deaths { get; set; }
}