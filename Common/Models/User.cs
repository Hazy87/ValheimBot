namespace Common.Models;

[DynamoDBTable("valheimbot-stats")]
public class User
{
    [DynamoDBHashKey]
    public string UserId { get; set; }
    public DateTime LastLogin { get; set; }
    public List<string> Deaths { get; set; } = new List<string>();
}