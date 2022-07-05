using Org.BouncyCastle.Crypto.Macs;

namespace NHLCafe.Pages.Models;

public class Bestelling
{
    public string tafelnummer { get; set; }
    public string ProductId { get; set; }
    public int alBetaald { get; set; }
    public int hoeveelheid { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int aantalBetalen { get; set; }
}