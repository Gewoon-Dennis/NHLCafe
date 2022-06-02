using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;


namespace NHLCafe.Pages;

public class Login : PageModel
{
    [BindProperty] public string Email { get; set; }
    [BindProperty] public string Password { get; set; }
    public static bool LoggedIn = false;

    public RedirectToPageResult OnGet()
    {
        string LoginCheck = HttpContext.Session.GetString("LogIn");
        if (LoginCheck != null)
        {
            LoggedIn = true;
            return new RedirectToPageResult("Account");
        }
        else
        {
            LoggedIn = false;
        }
        return null;
    }
    public RedirectToPageResult OnPostLogin()
    {
        string login  = new UserRepository().GetCafeUser(Email, Password);
        if (login != null)
        {
            LoggedIn = true;
            HttpContext.Session.SetString("LogIn", login);
            return new RedirectToPageResult("AccountOverview",false);
        }
        return null;
    }
}