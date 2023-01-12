using Amazon.DynamoDBv2.Model;

namespace Common.Services;

public class UserRepository : IUserRepository
{
    public UserRepository()
    {
        _amazonDynamoDbClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig()
            { ServiceURL = Environment.GetEnvironmentVariable("ServiceURL"), UseHttp = true });
        context = new DynamoDBContext(_amazonDynamoDbClient);
    }
    private readonly DynamoDBContext context;
    private AmazonDynamoDBClient _amazonDynamoDbClient;

    public async Task<List<User>> GetUsers()
    {
        var scanAsync = await context.ScanAsync<User>(new List<ScanCondition>()).GetNextSetAsync();
        return scanAsync;
    }
    public async Task<User> GetUser(string userid)
    {
        var queryFilter = new QueryFilter();
        queryFilter.AddCondition("UserId", QueryOperator.Equal, userid);
        var users = await context.FromQueryAsync<User>(new QueryOperationConfig() { Filter = queryFilter }).GetNextSetAsync();
        if (users.Count == 0)
        {
            return new User { UserId = userid, Deaths = new List<string>() };
        }

        return users.First();
    }
    public async Task AddDeath(string username)
    {
        await _amazonDynamoDbClient.UpdateItemAsync(new UpdateItemRequest()
        {
            TableName = "valheimbot-stats",
            Key = new Dictionary<string, AttributeValue>() { { "UserId", new AttributeValue(username) } },
            AttributeUpdates = new Dictionary<string, AttributeValueUpdate>()
            {
                {
                    "DeathCount", new AttributeValueUpdate() { Action = "ADD", Value = new AttributeValue("1") }
                }
            }
        });
        var user = await GetUser(username);
        if (user.Deaths == null)
            user.Deaths = new List<string>();
        user.Deaths.Add(DateTime.Now.ToString());
        await SaveUser(user);
    }

    public async Task<int> GetDeaths(string username)
    {
        return (await GetUser(username)).DeathCount;
    }


    public async Task SaveUser(User user)
    {
        await context.SaveAsync(user);
    }
}