using Microsoft.AspNetCore.Hosting;

namespace GitInsight;

public class Startup 
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DBContext>(o => o.UseSqlite("Data Source=GitInsight.db"));
        services.AddTransient<IRepositoryRepository, RepostitoryRepository>();
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<ICommitRepository, CommitRepository>();

        //Add services to the container
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddRouting(options => options.LowercaseUrls = true);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment enviroment)
    {
        if(enviroment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        
        app.UseEndpoints(b => b.MapControllerRoute("default", "{username}/{repository}"));

        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DBContext>();
        context.Database.Migrate();

    }
}