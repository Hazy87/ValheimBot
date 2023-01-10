namespace ValheimBot.Services;

class RandomDeathInsultRepo : IRandomDeathInsultRepo
{
    public string GetInsult()
    {
        return "{0} has died again that makes it {1} times, what a fucking tool";
    }
}