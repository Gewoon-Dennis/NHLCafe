using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages.Repository;

public class OrderRepository
{
    /*public string connectionString = "Server=192.168.178.35;" +
                                     "Database=nhlcafe;" +
                                     "Uid=admin;Pwd=password;" +
                                     "Port=3306";*/
    public string connectionString = "Server=192.168.68.109;" +
                                     "Database=nhlcafe;" +
                                     "Uid=admin;Pwd=password;" +
                                     "Port=3306";
    private IDbConnection Connect()
    {
        return new MySqlConnection(connectionString);
    }

    public IEnumerable<Tafel> GetTafels()
    {
        using var connection = Connect();
        return connection.Query<Tafel>(
            @"SELECT * FROM tafels order by tafelnummer ASC");
    }
    
    public bool AddToORder(Bestelling bestelling)
    {
        bool betaald = false;
        using var connection = Connect();
        int addedToOrder = connection.Execute(@"Insert Into bestelling (tafelnummer, ProductId, alBetaald) values (@tafelnummer, @ProductId, @alBetaald)", bestelling);
        return true;
    }

    public IEnumerable<Bestelling> GetOrder(string Tafel)
    {
        using var connection = Connect();
        return connection.Query<Bestelling>(@"select tafelnummer, bestelling.ProductId, alBetaald, product.Name from bestelling
                        INNER JOIN product using (ProductId) where tafelnummer = @tafelnummer", new{@tafelnummer = Tafel});
    }

}