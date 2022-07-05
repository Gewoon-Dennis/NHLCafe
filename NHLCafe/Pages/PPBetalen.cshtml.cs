using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;

namespace NHLCafe.Pages;

public class PPBetalen : PageModel
{
    [BindProperty] public string TafelSelect { get; set; }
    [BindProperty] public decimal AllesTotaal { get; set; }
    [BindProperty] public decimal PPTotaal { get; set; }
    public static bool BetalingSucces = false;

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

    public void OnPostPlusPP()
    {
        bool Plus = new OrderRepository().PlusBetalen(ProductID, IndexModel.TafelString);
        if (Plus)
        {
            besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);
            TafelLijst = new OrderRepository().GetTafels();
        }
    }

    public void OnPostMinPP()
    {
        bool Min = new OrderRepository().MinBetalen(ProductID, IndexModel.TafelString);
        if (Min)
        {
            besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);
            TafelLijst = new OrderRepository().GetTafels();
        }
    }
    public void OnPostBetalen()
    {
        BetalingSucces = new OrderRepository().PaySeperate(IndexModel.TafelString);
        besteldeItems = new OrderRepository().GetOrder(IndexModel.TafelString);
        TafelLijst = new OrderRepository().GetTafels();
    }

    public RedirectToPageResult OnPostBestellen()
    {
        bool Delete = new OrderRepository().Delete(IndexModel.TafelString);
        if (Delete == true)
        {
            IndexModel.TafelString = "Selecteer Tafel";
            BetalingSucces = false;
            return new RedirectToPageResult("Index");
        }

        return null;
    }
}