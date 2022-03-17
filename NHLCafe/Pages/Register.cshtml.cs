using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Repository;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages;

public class Register : PageModel

    private MySqlConnection Connect()
    {
        return new MySqlConnection("Server=24.132.196.32;Database=alternatief;Uid=DBAdmin;Pwd=Password12345!;Port=3306");
    }
    public void OnGet()
    {
        
    }

    public void OnPost([FromForm] string UserName, [FromForm] string Password)
    {
        StaticUserRepository.AddUser(new CafeUser());
    }
}