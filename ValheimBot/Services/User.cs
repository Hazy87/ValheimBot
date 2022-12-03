namespace ValheimBot.Services;

public class User
{
    public string Username { get; set; }
    public DateTime LastLogin { get; set; }
    public List<DateTime> Deaths { get; set; }
}