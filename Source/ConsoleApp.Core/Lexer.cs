namespace ConsoleApp.Core
{
    public class Lexer
    {
        private readonly StringTokenizer _stringTokenizer = new StringTokenizer
        {
            Operators = new[] { " ", "--", "-", "/", "+", ":", "=" }
        };

        public Queue<string> Tokenize(string[] args)
        {
            var queue = new Queue<string>();

            var text = string.Join(" ", args ?? new string[0]);

            foreach (var token in _stringTokenizer.Tokenize(text))
            {
                queue.Enqueue(token);
            }

            return queue;
        }
    }
}