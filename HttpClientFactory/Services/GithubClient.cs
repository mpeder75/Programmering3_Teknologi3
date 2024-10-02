﻿namespace HttpClientFactory.Services;

public class GithubClient : IGithubService
{
    private readonly HttpClient _client;

    public GithubClient(HttpClient client)
    {
        _client = client;
    }

    async Task<int> IGithubService.GetFollowersCount(string username)
    {
        var httpResponseMessage = await _client.GetAsync($"users/{username}/followers");

        var response = await httpResponseMessage.Content.ReadFromJsonAsync<object[]>();

        return response?.Length ?? 0;
    }

    async Task<string> IGithubService.GetProfileInfo(string username)
    {
        return await _client.GetStringAsync($"users/{username}");

        //return await _client.GetStringAsync("users/{mpeder75}");
    }
    

    public async Task<int> GetRepositoryCount(string username)
    {
        var httpResponseMessage = await _client.GetAsync($"users/{username}/repos");

        var response = await httpResponseMessage.Content.ReadFromJsonAsync<object[]>();

        return response?.Length ?? 0;
    }
}