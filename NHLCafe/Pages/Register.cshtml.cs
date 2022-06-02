using System.Data;
using System.Reflection.Metadata;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;
using NHLCafe.Pages.Models;
using MySql.Data.MySqlClient;
using Dapper;
namespace NHLCafe.Pages;

public class Register : PageModel{
    
    [BindProperty] public CafeUser NewCafeUser { get; set; }
    [BindProperty] public string Password { get; set; }
    public void OnGet()
    {
        
    }

    public IActionResult OnPostRegister()
    {
        NewCafeUser.UniqueGuid = Guid.NewGuid();
        NewCafeUser.Date = DateTime.Today;
        NewCafeUser.Password = Password;
        
        var addCafeUser = new UserRepository().AddCafeUser(NewCafeUser);
            return new RedirectToPageResult("Login");

    }
}