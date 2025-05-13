using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProxyControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RulesController : ControllerBase
    {
        /*private readonly string badWordsPath = "C:/Users/ASUS/Desktop/sexto/APD/ApeProyecto/squid-proxy-control/bad_words.txt"; //"/etc/squid/bad_words.txt"; 
        private readonly string blockedSitesPath = "C:/Users/ASUS/Desktop/sexto/APD/ApeProyecto/squid-proxy-control/blocked_sites.txt";//"/etc/squid/bad_words.txt/blocked_sites.txt";
        */
        private readonly string badWordsPath = "/config/squid/bad_words.txt";
        private readonly string blockedSitesPath = "/config/squid/blocked_sites.txt";


        [HttpPost("block-word")]
        public IActionResult BlockWord([FromBody] string word)
        {
            System.IO.File.AppendAllText(badWordsPath, $"{word}\n");
            RestartSquid();
            return Ok($"Palabra '{word}' bloqueada.");
        }

        [HttpPost("block-domain")]
        public IActionResult BlockDomain([FromBody] string domain)
        {
            System.IO.File.AppendAllText(blockedSitesPath, $"{domain}\n");
            RestartSquid();
            return Ok($"Dominio '{domain}' bloqueado.");
        }

        private void RestartSquid()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = "-c \"squid -k reconfigure\"",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            Process.Start(psi);
        }
    }
}
