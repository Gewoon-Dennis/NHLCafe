using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages;

public class Overzicht : PageModel
{
    [BindProperty] public string TafelSelect { get; set; }
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
    public void OnPostMinusOne()
    {
        bool minusOne = new OrderRepository().MinusOne(ProductID, IndexModel.TafelString);
        if (minusOne)
        {
            besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);
            TafelLijst = new OrderRepository().GetTafels();
        }
    }

    public void OnPostPlusOne()
    {
        bool plusOne = new OrderRepository().PlusOne(ProductID, IndexModel.TafelString);
        if (plusOne)
        {
            besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);
            TafelLijst = new OrderRepository().GetTafels();
        }
    }
}