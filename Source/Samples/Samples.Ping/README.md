# Ping sample app
Tests the reachability of a host on an IP network.

*The sample app only implements basic functionallity of the ping command.*

*Example output*
```
> ping google.com

Pinging google.com with 32 bytes of data:
Reply from google.com: bytes:32 time=26 TTL=55
Reply from google.com: bytes:32 time=26 TTL=55
Reply from google.com: bytes:32 time=25 TTL=55
Reply from google.com: bytes:32 time=26 TTL=55

```

## Arguments
```C#
[DefaultArguments]
public class PingArgs
{
  public string Host { get; set; }
}
```
Use the ```[DefaultArgument]``` attribute to have it match when there are no argument flag, e.g. ```-host```.

## Commands
```C#
public class PingCommand : Command
{
  public object Execute(PingArgs args)
  { ...
  }
}
```

## Views
### Razor views
 * ```View.cstxt``` - used by the ```PingCommand```

### Mustache views
 * ```Default.template``` - the default view which is displayed when no argument matches

