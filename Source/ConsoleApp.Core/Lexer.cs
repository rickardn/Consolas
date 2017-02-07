namespace Consolas.Core
{
    public class Lexer
    {
        private readonly StringTokenizer _stringTokenizer = 
            new StringTokenizer();

        public Queue<string> Tokenize(string[] arguments)
        {
            var queue = new Queue<string>();
            arguments = arguments ?? new string[0];

            var i = 0;
            foreach (var argument in arguments)
            {
                foreach (var token in _stringTokenizer.Tokenize(argument))
                {
                    queue.Enqueue(token);
                }

                if (i < arguments.Length - 1)
                {
                    queue.Enqueue(" ");
                }
                i++;
            }
            
            return queue;
        }
    }
}