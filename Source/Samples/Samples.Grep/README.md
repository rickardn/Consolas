# Grep sample app
A classic Unix command - search plain-text data sets for lines matching a regular expression.

*The sample app only implements basic functionallity of the grep command.*

*Example output*
```
> type fruitlist.txt
apple
apples
pineapple
apple-
apple-fruit
fruit-apple
banana
pear
peach
orange

> grep.exe -regex ^apple$ -filename .\fruitlist.txt
apple

> grep.exe ^apple .\fruitlist.txt
apple
apples
apple-
apple-fruit
```

## Arguments
```C#
[DefaultArguments]
public class GrepArgs
{
  public string Regex { get; set; }
  public string FileName { get; set; }
}
```
Use the ```[DefaultArgument]``` attribute to have it match when there are no argument flags, e.g. ```-regex```.

```C#
public class VersionArgs
{
	public bool Version { get; set; }
}
```

## Commands
```C#
public class GrepCommand : Command
{
  public object Execute(GrepArgs args)
{
```
```C#
public class VersionCommand : Command
{
  public object Execute(VersionArgs args)
{
```

## Views
### Mustache views
  * ```Default.template``` - the default view which is displayed when no argument matches
  * ```Version.template``` - used by the ```VersionCommand```
