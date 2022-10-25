using LibGit2Sharp;
using System.Linq;
using System;

public class Program 
{
    public static void Main(string[] args)
    {
        foreach(var arg in args)
        {
            arg.ToLower();
            switch(arg)
            {
                case "--frequency":
                var rep = new Repository(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName);
                rep.Commits.GroupBy(x => x.Author.When.ToString("yyyy-MM-dd")).ToList().ForEach(x => Console.WriteLine(x.Count() + " " + x.Key));
                break;

                case "--author": //Don't know why but looks cool
                Console.WriteLine("author");
                break;
            }
        }
    }
}





