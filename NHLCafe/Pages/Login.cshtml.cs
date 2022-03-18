using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages;

public class Login : PageModel
{
    [BindProperty] public string UserName { get; set; }
    [BindProperty] public string Password { get; set; }
    [BindProperty] public List<CafeUser> Ingelogde { get; set; }

    public void OnGet()
    {
        Ingelogde = new SqlBestand().GetCafeUser("Test", "123456");
    }
    public void OnPostLogin()
    {
        Ingelogde = new SqlBestand().GetCafeUser(UserName, Password);
        
    }
    
}