using System.Net.Http;

namespace Infrastructure.Api.Services;

public class GithubService : IGithubService
{
    private readonly HttpClient _httpClient;

    public GithubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    async Task<string> IGithubService.GetGithubUser(string username)
    {
        return await _httpClient.GetStringAsync($"users/{username}");
    }
}