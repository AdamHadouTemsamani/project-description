using Newtonsoft.Json;
using System;
using System.Text.Json;

namespace GitInsight;

public class author {
    public string? name { get; set; }
    public List<(int commitFrequency, DateTime commitDate)>? commits { get; set; }
}

[ApiController]
public class RepositoryController : Controller
{
    private readonly IGitInsight _gitInsight;    
    private readonly IConfiguration _config;
    public RepositoryController(IGitInsight gitInsight, IConfiguration config)
    {
        _gitInsight = gitInsight;
        _config = config;
    }
    
    [HttpGet]
    [Route("{username}/{repository}")]
    public async Task<IActionResult> PullRepository(string username, string repository)
    {
        var path = CloneRepository.GetDirectory(repository);
        var repo = CloneRepository.CreateRepository(username, repository);
        
        await _gitInsight.AddRepository(repo);
        var repoanalysis = await _gitInsight.GetCommitsPerAuthorAsync(repo);
        var authors = new List<author>();
        foreach(var auth in repoanalysis){
            authors.Add(new author{name = auth.Key, commits = auth.Value});
        }
        return Json(new{authors}, new JsonSerializerOptions{IncludeFields = true});
    }

    [HttpGet]
    [Route("{username}/{repository}/forks")]
    public async Task<IActionResult> GetAllForks(string username, string repository)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://api.github.com");
        var token = Environment.GetEnvironmentVariable("accesstoken");

        
        client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

        var json = await client.GetAsync($"/repos/{username}/{repository}/forks");
        var forks = json.Content.ReadAsStringAsync().Result;

        var forksCount = JArray.Parse(forks);
        return Json(new {forksCount.Count});
    }
    
}