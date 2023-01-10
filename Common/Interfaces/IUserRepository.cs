namespace Common.Interfaces;

public interface IUserRepository
{
    Task AddDeath(string username);
    Task<int> GetDeaths(string username);
    Task SaveUser(User user);
    Task<User> GetUser(string userid);
    Task<List<User>> GetUsers();
}