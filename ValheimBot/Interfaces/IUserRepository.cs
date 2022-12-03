namespace ValheimBot.Interfaces;

public interface IUserRepository
{
    Task AddDeath(string username);
    Task<int> GetDeaths(string username);
    Task<List<User>> LoadUsers();
    void SaveDeaths(List<User> users);
}