using System;

namespace ConsoleApp.Core
{
    public class MatchAttribute : Attribute
    {
        public MatchAttribute(string pattern)
        {
        }
    }
}