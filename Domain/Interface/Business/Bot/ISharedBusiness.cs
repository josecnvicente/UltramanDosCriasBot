namespace Domain.Interface.Business.Bot;

public interface ISharedBusiness
{
    Task<bool> MounthJokeCache(string user, DateTime date);
    Task<bool> DailyCache(DateTime date);
}
