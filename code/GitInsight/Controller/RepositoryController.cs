using Newtonsoft.Json;

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
    public IEnumerable<(int commitFrequency, DateTime commitDate)> PullRepository(string username, string repository)
    {
        var path = CloneRepository.GetDirectory(repository);
        var repo = CloneRepository.CreateRepository(username, repository);
        
        _gitInsight.AddRepository(repo);
        Console.WriteLine(Repository.IsValid(path));
        return _gitInsight.GetCommitsPerDay(repo);
    }

    [HttpGet]
    [Route("{username}/{repository}/forks")]
    public IEnumerable<GitFork> GetAllForks(string username, string repository)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://api.github.com");

        string accesstoken;
        using (StreamReader r = new StreamReader("./Controller/token.json"))
        {
            string json = r.ReadToEnd();
            var deserialized = (JObject)JsonConvert.DeserializeObject(json);
            accesstoken = deserialized["access-token"].Value<string>();
            
        } 

        using (StreamReader r = new StreamReader("./Controller/forks.json"))
        {
            string json = r.ReadToEnd();
            JObject forks = JObject.Parse(json);
            foreach(var f in forks)
            {
                /*
                
                var forkId = f["id"];
                var ownerId = f["owner"]["id"];
                var ownerLogin = f["owner"]["id"];


                yield return new GitFork { forkId = forkId, owner = new GitFork.Owner { Login = ownerLogin, Id = ownerId} };
                */
            }
            yield return new GitFork { };
        } 
    }
    
}