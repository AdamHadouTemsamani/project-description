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
                case "--commit-frequency":
                Console.WriteLine("frequency");
                break;

                case "--commit-author": //Don't know why but looks cool
                Console.WriteLine("author");
                break;
            }
        }

        var rep = new Repository(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName);
        rep.Commits.ToList().ForEach(x => Console.WriteLine(x.Author.Name));
    }
    

}





