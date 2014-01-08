namespace Samples.Grep.Commands
{
    public class VersionCommand
    {
        public string Execute(VersionArgs args)
        {
            return 
@"grep (GNU grep) 2.4.2

Copyright 1988, 1992-1999, 2000 Free Software Foundation, Inc.
This is free software; see the source for copying conditions. There is NO
warranty; not even for MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
";
        }
    }
}