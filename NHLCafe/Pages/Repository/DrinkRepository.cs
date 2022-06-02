using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages.Repository;

public class DrinkRepository
{
    public string connectionString = "Server=192.168.178.35;" +
                                     "Database=nhlcafe;" +
                                     "Uid=admin;Pwd=password;" +
                                     "Port=3306";
    private IDbConnection Connect()
    {
        return new MySqlConnection(connectionString);
    }

    public IEnumerable<Drankje> GetFrisdranken()
    {
        using var connection = Connect();
        return Connect().Query<Drankje>(@"select ProductId, product.Name, Price from product
        INNER JOIN category using (CategoryId)
        where category.Name = 'Frisdranken';");
    }
    public IEnumerable<Drankje> GetWarmeDranken()
    {
        using var connection = Connect();
        return Connect().Query<Drankje>(@"select ProductId, product.Name, Price from product
        INNER JOIN category using (CategoryId)
        where category.Name = 'Warme dranken';");
    }
    public IEnumerable<Drankje> GetWijnen()
    {
        using var connection = Connect();
        return Connect().Query<Drankje>(@"select ProductId, product.Name, Price from product
        INNER JOIN category using (CategoryId)
        where category.Name = 'Wijnen en aperitieven';");
    }
    public IEnumerable<Drankje> GetBier()
    {
        using var connection = Connect();
        return Connect().Query<Drankje>(@"select ProductId, product.Name, Price from product
        INNER JOIN category using (CategoryId)
        where category.Name = 'Bier';");
    }
    public IEnumerable<Drankje> GetSpeciaalBier()
    {
        using var connection = Connect();
        return Connect().Query<Drankje>(@"select ProductId, product.Name, Price from product
        INNER JOIN category using (CategoryId)
        where category.Name = 'Speciaal bier';");
    }
}