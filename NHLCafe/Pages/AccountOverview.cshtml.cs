using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages;

public class AccountOverview : PageModel
{
    [BindProperty] public CafeUser Ingelogde { get; set; }
    public void OnGet()
    {
        
    }
}