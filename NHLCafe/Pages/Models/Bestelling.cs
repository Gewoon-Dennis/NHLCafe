namespace NHLCafe.Pages.Models;

public class Bestelling
{
    public string tafelnummer { get; set; }
    public string ProductId { get; set; }
    public int alBetaald { get; set; } = 0;
    public string Name { get; set; }
}