using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUpdateFromGit.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            var msg = $"ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
            msg += $"\r\nFoohere: {Environment.GetEnvironmentVariable("Foohere")}";
            ViewData["msg"] = msg;
        }
    }
}
