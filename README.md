Consolas is a console application framework that simplyfies the creation of averything from simple throw away apps to bigger, more complex tools with lots of arguments.

#Simple example

<pre>
class Program : ConsoleApp
{
    static void Main(string[] args)
    {
        Match(args);
    }
}

public class HelpArgs
{
    public bool Help { get; set; }
}

public class HelpCommand : Command
{
    public string Execute(HelpArgs args)
    {
        return "Using: Program.exe ...";
    }
}
</pre>

#Running the above program from a console

<pre>
C> program.exe -Help
Using: Program.exe ...
</pre>

#License

[BSD 2-Clause License](http://opensource.org/licenses/BSD-2-Clause)

#Blog articles

[Introducing Consolas - a console application framework for .NET](http://www.rickardnilsson.net/?tag=/consolas)
