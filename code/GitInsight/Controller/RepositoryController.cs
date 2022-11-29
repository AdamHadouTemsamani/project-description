using Newtonsoft.Json;
using System;

namespace GitInsight;


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
        Console.WriteLine(LibGit2Sharp.Repository.IsValid(path));
        var repoanalysis = await _gitInsight.GetCommitsPerAuthorAsync(repo);
        var bruh = repoanalysis.First();
        return Json( new{repoanalysis});
        //return Json(new{ repoanalysis});
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
        
        //Json(json.Content.ReadAsStringAsync().ToString());
        /*
        var forks = JArray.Parse(jsonstring!);
        return Json(new {forks.Count});
        */
    }
    
}