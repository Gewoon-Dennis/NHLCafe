using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;


namespace NHLCafe.Pages;

public class Login : PageModel
{
    [BindProperty] public string UserName { get; set; }
    [BindProperty] public string Password { get; set; }
    public static bool LoggedIn = false;
    

    public RedirectToPageResult OnGet()
    {
       string LoginCheck = HttpContext.Session.GetString("LogIn");
       if (LoginCheck != null)
       {
           LoggedIn = true;
           return new RedirectToPageResult("AccountOverview");
       }

       return null;
    }
    public RedirectToPageResult OnPostLogin()
    {
        string login  = new SqlBestand().GetCafeUser(UserName, Password);
        if (login != null)
        {
            LoggedIn = true;
            HttpContext.Session.SetString("LogIn", login);
            return new RedirectToPageResult("AccountOverview");
        }

        return null;
    }
    
}