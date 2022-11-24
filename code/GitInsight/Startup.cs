using Microsoft.AspNetCore.Hosting;

namespace GitInsight;

public class Startup 
{  
    private IConfiguration _configuration;
    private IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    } 

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DBContext>(options => 
                        options.UseSqlite("Data Source=GitInsight.db"));

        services.AddTransient<IRepositoryRepository, RepostitoryRepository>();
        services.AddTransient<ICommitRepository, CommitRepository>();
        services.AddTransient<IGitInsight, GitInsight>();

        //Add services to the container
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRouting(options => options.LowercaseUrls = true);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment enviroment)
    {
        var builder = new ConfigurationBuilder()
                            .SetBasePath(enviroment.ContentRootPath);
        
        builder.AddUserSecrets<AppSecretConfig>();

        _configuration = builder.Build();

        if(enviroment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        
        app.UseEndpoints(b => b.MapControllerRoute("default", "{username}/{repository}"));
        
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DBContext>();
        context.Database.Migrate();

    }
}