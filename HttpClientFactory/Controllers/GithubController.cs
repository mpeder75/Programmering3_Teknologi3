using HttpClientFactory.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly IGithubService _client;

        public GithubController(IGithubService client)
        {
            _client = client;
        }

        [HttpGet]
        [Route("GetProfileInfo/{username}")]
        public async Task<string> GetProfileInfo([FromRoute] string username)
        {
            return await _client.GetProfileInfo(username);
        }
        
        [HttpGet]
        [Route("GetFollowersCount/{username}")]
        public async Task<IActionResult> GetFollowersCount([FromRoute] string username)
        {
            var followerCount = await _client.GetFollowersCount(username);
            return Ok(followerCount);
        }

        [HttpGet]
        [Route("GetRepositoryCount/{username}")]
        public async Task<IActionResult> GetRepositoryCount([FromRoute] string username)
        {
            var repoCount = await _client.GetRepositoryCount(username);
            return Ok(repoCount);
        }

        [HttpGet]
        [Route("GetFollowingCount/{username}")]
        public async Task<IActionResult> GetFollowingCount([FromRoute] string username)
        {
            var followingCount = await _client.GetFollowingCount(username);
            return Ok(followingCount);
        }

        [HttpGet]
        [Route("GetOrganizations/{username}")]
        public async Task<IActionResult> GetOrganizations([FromRoute] string username)
        {
            var organizations = await _client.GetOrganizations(username);
            return Ok(organizations);
        }
    }
}
