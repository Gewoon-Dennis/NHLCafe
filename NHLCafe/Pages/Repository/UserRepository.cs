using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using NHLCafe.Pages.Models;

namespace NHLCafe.Pages.Repository;

public class UserRepository
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
        int numRowEffected = connection.Execute(@"INSERT INTO cafeuser(Ober_ID, Ober_Mail, Ober_Name, Ober_password, Workplace, CreateDate) 
            VALUES (@UniqueGuid, @Email, @UserName, @Password, @Location, @Date)",cafeUser);
        
        if (numRowEffected == 1)
        {
            return true;
        }

        return false;
    }
    
    public string GetCafeUser(string Email, string password)
    {
        using var connection = Connect();
        string UserId= connection.QuerySingleOrDefault<string>(
            @"SELECT Ober_ID FROM cafeuser WHERE Ober_Mail = @EMail AND Ober_password = @Password"
            , new {Email = Email, Password = password});
        return UserId;
    }
    public string CafeUsername(string UserId)
    {
        using var connection = Connect();
        string UserName= connection.QuerySingleOrDefault<string>(
            @"SELECT Ober_Name FROM cafeuser WHERE Ober_ID = @userId"
            , new {userId = UserId});
        return UserName;
    }
    public string CafeUserWorkplace(string UserId)
    {
        using var connection = Connect();
        string location= connection.QuerySingleOrDefault<string>(
            @"SELECT Workplace FROM cafeuser WHERE Ober_ID = @userId"
            , new {userId = UserId});
        return location;
    }
}