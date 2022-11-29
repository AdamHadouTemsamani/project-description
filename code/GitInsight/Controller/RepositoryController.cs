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

        

        /*
        string accesstoken;
        using (StreamReader r = new StreamReader("./Controller/token.json"))
        {
            string json = r.ReadToEnd();
            var deserialized = (JObject)JsonConvert.DeserializeObject(json);
            accesstoken = deserialized["access-token"].Value<string>();
            
        } 
        */
        var accesstoken = Environment.GetEnvironmentVariable("accesstoken");
        Console.WriteLine(accesstoken);
        using (StreamReader r = new StreamReader("./Controller/forks.json"))
        {
            string json = await r.ReadToEndAsync();
            var forks = JArray.Parse(json);
            //return forks.Count();
            return Json(new {forks.Count, accesstoken} );
        } 
    }
    
}