namespace Common.Services;

public class UserRepository : IUserRepository
{
    public UserRepository()
    {
        context = new DynamoDBContext(new AmazonDynamoDBClient(new AmazonDynamoDBConfig()
            { ServiceURL = Environment.GetEnvironmentVariable("ServiceURL"), UseHttp = true }));

    }
    private readonly DynamoDBContext context;

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