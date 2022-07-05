using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages;

public class PPBetalen : PageModel
{
    [BindProperty] public string TafelSelect { get; set; }
    [BindProperty] public decimal AllesTotaal { get; set; }

    [BindProperty] public string ProductID { get; set; }
    public IEnumerable<Tafel> TafelLijst { get; set; }
    
    public IEnumerable<Bestelling> besteldeItems { get; set; }
    
    public void OnGet()
    {
        TafelLijst = new OrderRepository().GetTafels();
        if (@IndexModel.TafelString != "Selecteer Tafel")
        {
            besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);  
        }
    }
    public void OnPostTable()
    {
        IndexModel.TafelString = TafelSelect;
        
        besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);
        TafelLijst = new OrderRepository().GetTafels();
        
    }

    public void PlusPP()
    {
        
    }

    public void MinPP()
    {
        
    }
}