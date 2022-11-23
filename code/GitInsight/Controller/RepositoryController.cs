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
    public async Task<IEnumerable<(int commitFrequency, DateTime commitDate)>> PullRepository(string username, string repository)
    {
        var path = CloneRepository.GetDirectory(repository);
        var repo = CloneRepository.CreateRepository(username, repository);
        
        await _gitInsight.AddRepository(repo);
        Console.WriteLine(Repository.IsValid(path));
        return await _gitInsight.GetCommitsPerDay(repo);
    }

    [HttpGet]
    [Route("{username}/{repository}/forks")]
    public int GetAllForks(string username, string repository)
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

        using (StreamReader r = new StreamReader("./Controller/forks.json"))
        {
            string json = r.ReadToEnd();
            var forks = JArray.Parse(json);
            return forks.Count();
        } 
    }
    
}