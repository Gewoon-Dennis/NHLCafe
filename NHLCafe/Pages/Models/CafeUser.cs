using System;

namespace NHLCafe.Pages.Models;

public class CafeUser
{
    public Guid UniqueGuid { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public string Location { get; set; }
    public DateTime Date { get; set; }
}