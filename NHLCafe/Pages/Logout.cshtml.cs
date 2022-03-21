using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages;
using Org.BouncyCastle.Asn1.Cms;

namespace NHLCafe.Pages;

public class Logout : PageModel
{
    public void OnGet()
    {
        HttpContext.Session.Remove("LogIn");
        Login.LoggedIn = false;
    }
    
}