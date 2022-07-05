using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using NHLCafe.Pages.Models;
using Org.BouncyCastle.Crypto;

namespace NHLCafe.Pages.Repository;

public class OrderRepository
{
    public string connectionString = "Server=192.168.178.35;" +
                                     "Database=nhlcafe;" +
                                     "Uid=admin;Pwd=password;" +
                                     "Port=3306";
    /*public string connectionString = "Server=192.168.68.109;" +
                                     "Database=nhlcafe;" +
                                     "Uid=admin;Pwd=password;" +
                                     "Port=3306";*/
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
        using var connection = Connect();
        int Plus = connection.Execute(@"update bestelling
                    set hoeveelheid = hoeveelheid + 1
                    where ProductId = @product and tafelnummer = @TafelNummer", new {@product = bestelling.ProductId, @TafelNummer = bestelling.tafelnummer});
        if (Plus != 0)
        {
            return true;
        }
        int addedToOrder = connection.Execute(@"Insert Into bestelling (tafelnummer, ProductId, alBetaald, hoeveelheid, aantalBetalen) values (@tafelnummer, @ProductId, @alBetaald, @hoeveelheid, @aantalBetalen)", bestelling);
        if (addedToOrder != 0)
        {
            return true;
        }
        return false;
    }

    public bool MinusOne(string ProductID, string Tafelnummer)
    {
        using var connection = Connect();
        int Minus = connection.Execute(@"update bestelling
                    set hoeveelheid = hoeveelheid - 1
                    where ProductId = @product and tafelnummer = @TafelNummer", new {@product = ProductID, @TafelNummer = Tafelnummer});
        if (Minus != 0)
        {
            int check = connection.Execute(@"delete from bestelling where productId = @product and hoeveelheid = '0'",new {@product = ProductID});
            return true;
        }
        return false;
    }
    public bool PlusOne(string ProductID, string Tafelnummer)
    {
        using var connection = Connect();
        int Plus = connection.Execute(@"update bestelling
                    set hoeveelheid = hoeveelheid + 1
                    where ProductId = @product and tafelnummer = @TafelNummer", new {@product = ProductID, @TafelNummer = Tafelnummer});
        if (Plus != 0)
        {
            return true;
        }
        return false;
    }

    public IEnumerable<Bestelling> GetOrder(string Tafel)
    {
        using var connection = Connect();
        return connection.Query<Bestelling>(@"select tafelnummer, bestelling.ProductId, alBetaald, hoeveelheid, product.Name, product.Price, aantalBetalen from bestelling
                        INNER JOIN product using (ProductId) where tafelnummer = @tafelnummer", new{@tafelnummer = Tafel});
    }

    public bool PayAll(string tafel)
    {
        using var connection = Connect();
        int Pay = connection.Execute(@"update bestelling set alBetaald = hoeveelheid where tafelnummer = @tafelNummer", new{@tafelNummer = tafel});
        if (Pay != 0)
        {
            return true;
        }
        return false;
    }
    
    public bool PaySeperate(string tafel)
    {
        using var connection = Connect();
        int Pay = connection.Execute(@"update bestelling set alBetaald = alBetaald + aantalBetalen where tafelnummer = @tafelNummer", new{@tafelNummer = tafel});
        if (Pay != 0)
        {
            int Clear = connection.Execute(@"update bestelling set aantalBetalen = 0 where tafelnummer = @tafelNummer", new{@tafelNummer = tafel});
            if (Clear != 0)
            {
                return true;
            }
        }
        return false;
    }
    
    public bool Delete(string Tafelnummer)
    {
        using var connection = Connect();
        int Minus = connection.Execute(@"delete from bestelling where tafelnummer = @TafelNummer", new {@TafelNummer = Tafelnummer});
        if (Minus != 0)
        {
            return true;
        }
        return false;
    }
    
    public bool MinBetalen(string ProductID, string Tafelnummer)
    {
        using var connection = Connect();
        int Minus = connection.Execute(@"update bestelling
                    set aantalBetalen = aantalBetalen - 1
                    where ProductId = @product and tafelnummer = @TafelNummer", new {@product = ProductID, @TafelNummer = Tafelnummer});
        if (Minus != 0)
        {
            int check = connection.Execute(@"delete from bestelling where productId = @product and hoeveelheid = '0'",new {@product = ProductID});
            return true;
        }
        return false;
    }
    public bool PlusBetalen(string ProductID, string Tafelnummer)
    {
        using var connection = Connect();
        int Plus = connection.Execute(@"update bestelling
                    set aantalBetalen = aantalBetalen + 1
                    where ProductId = @product and tafelnummer = @TafelNummer", new {@product = ProductID, @TafelNummer = Tafelnummer});
        if (Plus != 0)
        {
            return true;
        }
        return false;
    }

}