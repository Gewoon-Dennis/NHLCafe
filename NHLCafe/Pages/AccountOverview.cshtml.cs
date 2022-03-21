using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;


namespace NHLCafe.Pages;

public class AccountOverview : PageModel
{
    
    [BindProperty] public string OberId { get; set; }
    [BindProperty] public string OberName { get; set; }
    [BindProperty] public string OberWorkplace { get; set; }

    public void OnGet()
    {
        OberId = HttpContext.Session.GetString("LogIn");
        OberName = new SqlBestand().CafeUsername(OberId);
        OberWorkplace = new SqlBestand().CafeUserWorkplace(OberId);
    }

    public void OnPostPrint()
    {
       
    }
    
}