using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages;

public class Register : PageModel
{
    public void OnGet()
    {
        
    }

    public void OnPost([FromForm] string UserName, [FromForm] string Password)
    {
        StaticUserRepository.AddUser(new CafeUser());
    }
}