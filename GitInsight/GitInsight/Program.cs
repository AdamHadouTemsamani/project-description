using LibGit2Sharp;

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
        Console.WriteLine("Enter path to git repository");
        var path = Console.ReadLine();
    }
    

}





