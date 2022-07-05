using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHLCafe.Pages.Models;
using NHLCafe.Pages.Repository;


namespace NHLCafe.Pages;

public class IndexModel : PageModel
{
    [BindProperty] public string TafelSelect { get; set; }
    [BindProperty] public string ProductID { get; set; }
    [BindProperty] public Bestelling bestelling { get; set; }

    public static string TafelString = "Selecteer Tafel";

    #region Database Inhoud Lijsten
    public IEnumerable<Drankje> Frisdranken { get; set; }
    public IEnumerable<Drankje> WarmeDranken { get; set; }
    public IEnumerable<Drankje> Wijnen { get; set; }
    public IEnumerable<Drankje> Bieren { get; set; }
    public IEnumerable<Drankje> SpeciaalBieren { get; set; }

    #endregion
    
    public IEnumerable<Tafel> TafelLijst { get; set; }
    
    public IEnumerable<Bestelling> besteldeItems { get; set; }

    public void OnGet()
    {
        Frisdranken = new DrinkRepository().GetFrisdranken();
        WarmeDranken = new DrinkRepository().GetWarmeDranken();
        Wijnen = new DrinkRepository().GetWijnen();
        Bieren = new DrinkRepository().GetBier();
        SpeciaalBieren = new DrinkRepository().GetSpeciaalBier();
        TafelLijst = new OrderRepository().GetTafels();
        if (TafelString != "Selecteer Tafel")
        {
            besteldeItems = new OrderRepository().GetOrder(TafelString);  
        }
    }

    #region Bestellen pagina

    public async void OnPostOrder()
    {
        bestelling.ProductId = ProductID;
        bestelling.tafelnummer = TafelString;
        bestelling.hoeveelheid = 1;
        bestelling.aantalBetalen = 0;
        bool AddToORder = new OrderRepository().AddToORder(bestelling);
        if (AddToORder)
        {
            besteldeItems = new OrderRepository().GetOrder(TafelString);
            TafelLijst = new OrderRepository().GetTafels();
            Frisdranken = new DrinkRepository().GetFrisdranken();
            WarmeDranken = new DrinkRepository().GetWarmeDranken();
            Wijnen = new DrinkRepository().GetWijnen();
            Bieren = new DrinkRepository().GetBier();
            SpeciaalBieren = new DrinkRepository().GetSpeciaalBier();
        }
    }

    public void OnPostMinusOne()
    {
        bool minusOne = new OrderRepository().MinusOne(ProductID, TafelString);
        if (minusOne)
        {
            besteldeItems = new OrderRepository().GetOrder(TafelString);
            TafelLijst = new OrderRepository().GetTafels();
            Frisdranken = new DrinkRepository().GetFrisdranken();
            WarmeDranken = new DrinkRepository().GetWarmeDranken();
            Wijnen = new DrinkRepository().GetWijnen();
            Bieren = new DrinkRepository().GetBier();
            SpeciaalBieren = new DrinkRepository().GetSpeciaalBier();
        }
    }
    public void OnPostPlusOne()
    {
        bool plusOne = new OrderRepository().PlusOne(ProductID, TafelString);
        if (plusOne)
        {
            besteldeItems = new OrderRepository().GetOrder(TafelString);
            TafelLijst = new OrderRepository().GetTafels();
            Frisdranken = new DrinkRepository().GetFrisdranken();
            WarmeDranken = new DrinkRepository().GetWarmeDranken();
            Wijnen = new DrinkRepository().GetWijnen();
            Bieren = new DrinkRepository().GetBier();
            SpeciaalBieren = new DrinkRepository().GetSpeciaalBier();
        }
    }
    public void OnPostTable()
    {
        TafelString = TafelSelect;
        
        besteldeItems = new OrderRepository().GetOrder(TafelString);
        TafelLijst = new OrderRepository().GetTafels();
        Frisdranken = new DrinkRepository().GetFrisdranken();
        WarmeDranken = new DrinkRepository().GetWarmeDranken();
        Wijnen = new DrinkRepository().GetWijnen();
        Bieren = new DrinkRepository().GetBier();
        SpeciaalBieren = new DrinkRepository().GetSpeciaalBier();
    }
    
    
    

    #endregion

}
//private readonly ILogger<IndexModel> _logger;

//public IndexModel(ILogger<IndexModel> logger)
//{
//    _logger = logger;
//}
