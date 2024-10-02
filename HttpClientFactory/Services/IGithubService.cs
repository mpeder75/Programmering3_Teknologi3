namespace HttpClientFactory.Services
{
    public interface IGithubService
    {
        Task<string> GetProfileInfo(string username);
        Task<int> GetFollowersCount(string username);
        Task<int> GetRepositoryCount(string username);
    }
}
