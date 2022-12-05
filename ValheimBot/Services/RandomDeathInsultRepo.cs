namespace ValheimBot.Services;

class RandomDeathInsultRepo : IRandomDeathInsultRepo
{
    public string GetInsult()
    {
        return "{0} has died, what a fucking tool";
    }
}