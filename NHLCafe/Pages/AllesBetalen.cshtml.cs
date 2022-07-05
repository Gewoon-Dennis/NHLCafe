using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;
using Org.BouncyCastle.Asn1;

namespace NHLCafe.Pages;

public class AllesBetalen : PageModel
{
    [BindProperty] public string TafelSelect { get; set; }
    [BindProperty] public string ProductID { get; set; }
    [BindProperty] public decimal AllesTotaal { get; set; }
    [BindProperty] public bool BetalingSucces { get; set; } = false;
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

    public void OnPostBetalen()
    {
        BetalingSucces = new OrderRepository().PayAll(IndexModel.TafelString);
        besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);
        TafelLijst = new OrderRepository().GetTafels();
    }

    public RedirectToPageResult OnPostBestellen()
    {
        bool Delete = new OrderRepository().Delete(IndexModel.TafelString);
        if (Delete == true)
        {
            IndexModel.TafelString = "Selecteer Tafel";
            PPBetalen.BetalingSucces = false;
            return new RedirectToPageResult("Index");
        }

        return null;
    }
}