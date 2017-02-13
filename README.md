Consolas is a console application framework that simplyfies the creation of everything from simple throw away apps to bigger, more complex tools with lots of arguments.

#Features
- Convention over configuration
- Small fingerprint
- Testable
- Mustache view engine
- Razor view engine (plugable)

##How to get it

Simply create a new Console Application and install the Nuget package [Consolas](https://www.nuget.org/packages/Consolas/) or run the following command in the Package Manager Console

<pre>
PM> Install-Package Consolas
</pre>

##Simple example

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

public class HelpCommand : Command<HelpCommand>
{
    public string Execute(HelpArgs args)
    {
        return "Using: Program.exe ...";
    }
}
```

###Running the above program from a console

<pre>
C> program.exe -Help
Using: Program.exe ...
</pre>

##Unit testing
###Unit tests
Unit testing Consolas Commands is easy:
```csharp
[TestFixture]
public class GrepCommandTests
{
    [Test]
    public void Execute_ValidArgument_ReturnsGrepedText()
    {
        var command = new GrepCommand();

        var result = command.Execute(new GrepArgs
        {
            FileName = "doc.txt",
            Regex = "foo"
        });

        StringAssert.Contains("foo bar baz", result);
    }
}
```

###End to end tests
The following is a [sample](https://github.com/rickardn/Consolas/blob/master/Source/UnitTests/Samples/Samples.Grep.Tests/EndToEndTests.cs) testing a console application from end to end:

```csharp
[TestFixture]
public class EndToEndTests
{
    private StringBuilder _consoleOut;
    private TextWriter _outWriter;

    [SetUp]
    public void Setup()
    {
        _outWriter = Console.Out;
        _consoleOut = new StringBuilder();
        Console.SetOut(new StringWriter(_consoleOut));
    }

    [TearDown]
    public void TearDown()
    {
        Console.SetOut(_outWriter);
    }

    [Test]
    public void Version()
    {
        Program.Main(new []{ "-version"});
        StringAssert.Contains("2.4.2", _consoleOut.ToString());
    }
}
```


##Advanced examples
- [Classic UNIX grep sample](https://github.com/rickardn/Consolas/tree/master/Source/Samples/Samples.Grep)
- [Ping a network address](https://github.com/rickardn/Consolas/tree/master/Source/Samples/Samples.Ping)

##License

[BSD 2-Clause License](https://github.com/rickardn/Consolas/blob/master/LICENCE.md)

##Blog articles

[Introducing Consolas - a console application framework for .NET](http://www.rickardnilsson.net/?tag=/consolas)

##Acknowledgments

Consolas makes use of the following OSS projects:

- SimpleInjector released under the MIT license: https://simpleinjector.codeplex.com/license
- Nustache released under the MIT license: https://raw.githubusercontent.com/jdiamond/Nustache/master/LICENSE.txt
- RazorEngine released under the Microsoft Public License (Ms-PL): http://razorengine.codeplex.com/license
- NUnit released under the NUnit licence: http://nunit.org/nuget/license.html
- Should released under the Apache 2.0 license: https://github.com/erichexter/Should/blob/master/license.txt
- NSubstitute released under the BSD license: https://raw.githubusercontent.com/nsubstitute/NSubstitute/master/LICENSE.txt
- Cake released under the MIT license: https://github.com/cake-build/cake/blob/develop/LICENSE
