using System.ComponentModel.DataAnnotations;

namespace SampleConsoleApplication.Commands
{
    public class DeployArgs
    {
        [Required]
        public string Service { get; set; }

        [Required]
        public string Target { get; set; }
        
        public string SourceFiles { get; set; }
        public string Package { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string Backup { get; set; }
        public string Config { get; set; }
        public bool   Verbose { get; set; }

    }
}