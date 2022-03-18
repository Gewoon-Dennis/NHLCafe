using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages.Repository;

public class SqlBestand
{
    public string connectionString = "Server=192.168.178.35;" +
                                     "Database=nhlcafe;" +
                                     "Uid=admin;Pwd=password;" +
                                     "Port=3306";
    private IDbConnection Connect()
    {
        return new MySqlConnection(connectionString);
    }

    public bool AddCafeUser(CafeUser cafeUser)
    {
       using var connection = Connect();
        int numRowEffected = connection.Execute(@"INSERT INTO cafeuser(CafeUser_id, CafeUser_name, CafeUser_password, location, datum) 
            VALUES (@UniqueGuid, @UserName, @Password, @Location, @Date)",cafeUser);

        return numRowEffected == 1;
    }

    public IEnumerable<CafeUser> GetAll()
    {
        using var connection = Connect();
        return connection.Query<CafeUser>("SELECT FROM cafeuser * WHERE CafeUser_id = LAST_INSERT_ID");
    }

    public List<CafeUser> GetCafeUser(string username, string password)
    {
        using var connection = Connect();
        var User= connection.Query(
            @"SELECT * FROM cafeuser WHERE CafeUser_name = @UserName AND CafeUser_password = @Password"
            , new {UserName = username, Password = password});
        return User as List<CafeUser>;

    }
}