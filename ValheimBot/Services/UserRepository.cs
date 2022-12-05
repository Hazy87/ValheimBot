namespace ValheimBot.Services;

public class UserRepository : IUserRepository
{
    private string _fileName = "/app/users.json";

    public async Task AddDeath(string username)
    {
        var users = await LoadUsers();
        if (users.Any(x => x.Username == username))
            users.Single(x => x.Username == username).Deaths.Add(DateTime.Now);
        else 
            users.Add(new User(){Username = username, Deaths = new List<DateTime>() { DateTime.Now }, LastLogin = DateTime.Now});
        SaveDeaths(users);
    }

    public async Task<int> GetDeaths(string username)
    {
        return (await LoadUsers()).Single(x => x.Username == username).Deaths.Count;
    }

    public async Task<List<User>> LoadUsers()
    {
        if (File.Exists(_fileName))
        {
            var json = await File.ReadAllTextAsync(_fileName);
            return JsonSerializer.Deserialize<List<User>>(json);
        }

        return new List<User>();
    }

    public void SaveDeaths(List<User> users)
    {
        var json = JsonSerializer.Serialize(users);
        File.WriteAllTextAsync(_fileName, json);
    }
}