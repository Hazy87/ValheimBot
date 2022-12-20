using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace ValheimBot.Services;

public class UserRepository : IUserRepository
{
    public UserRepository()
    {
        context = new DynamoDBContext(new AmazonDynamoDBClient(new AmazonDynamoDBConfig()
            { ServiceURL = "http://192.168.10.4:8000", UseHttp = true }));

    }
    private readonly DynamoDBContext context;

    public async Task<User> GetUser(string userid)
    {
        var queryFilter = new QueryFilter();
        queryFilter.AddCondition("UserId", QueryOperator.Equal, userid);
        var users = await context.FromQueryAsync<User>(new QueryOperationConfig() { Filter = queryFilter }).GetNextSetAsync();
        if (users.Count == 0)
            return new User { UserId = userid, Deaths = new List<string>()};
        return users.First();
    }
    public async Task AddDeath(string username)
    {
        var user = await GetUser(username);
        if (user.Deaths == null)
            user.Deaths = new List<string>();
        user.Deaths.Add(DateTime.Now.ToString());
        await SaveUser(user);
    }

    public async Task<int> GetDeaths(string username)
    {
        return (await GetUser(username)).Deaths.Count;
    }


    public async Task SaveUser(User user)
    {
        await context.SaveAsync(user);
    }
}