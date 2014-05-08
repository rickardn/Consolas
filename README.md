Consolas is a console application framework that simplyfies the creation of everything from simple throw away apps to bigger, more complex tools with lots of arguments.

#Features
- Convention over configuration
- Small fingerprint
- Testable
- Mustache view engine
- Razor view engine (optional)

#How to get it

Simply create a new Console Application and install the Nuget package [Consolas](https://www.nuget.org/packages/Consolas/) or run the following command in the Package Manager Console

<pre>
PM> Install-Package Consolas
</pre>

#Simple example

```csharp
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
```

##Running the above program from a console

<pre>
C> program.exe -Help
Using: Program.exe ...
</pre>

#Advanced examples
- [Classic UNIX grep sample](https://github.com/rickardn/Consolas/tree/master/Source/Samples/Samples.Grep)
- [Ping a network address](https://github.com/rickardn/Consolas/tree/master/Source/Samples/Samples.Ping)

#License

[BSD 2-Clause License](http://opensource.org/licenses/BSD-2-Clause)

#Blog articles

[Introducing Consolas - a console application framework for .NET](http://www.rickardnilsson.net/?tag=/consolas)
