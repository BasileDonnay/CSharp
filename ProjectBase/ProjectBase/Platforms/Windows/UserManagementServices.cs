using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Services;

public partial class UserManagementServices
{
    public OleDbConnection Connection = new();
    public OleDbDataAdapter UserAdapter = new();

    internal void ConfigTools()
    {
        Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;" +
            @"Data Source = " +
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UserAccounts.accdb") +
            ";Persist Security Info=False";

        string DeleteCommandTxt = "DELETE FROM DB_Users WHERE UserName=@UserName";
        string UpdateCommandTxt = "UPDATE DB_Users SET UserPassword=@UserPassword, UserAccessType=@UserAccessType WHERE UserName=@UserName";
        string InsertCommandTxt = "INSERT INTO DB_Users(UserName,UserPassword,UserAccessType) VALUES (@UserName,@UserPassword,@UserAccessType)";
        string SelectCommandTxt = "SELECT * FROM DB_Users ORDER BY User_ID";

        OleDbCommand DeleteCommand = new OleDbCommand(DeleteCommandTxt);
        OleDbCommand UpdateCommand = new OleDbCommand(UpdateCommandTxt);
        OleDbCommand InsertCommand = new OleDbCommand(InsertCommandTxt);
        OleDbCommand SelectCommand = new OleDbCommand(SelectCommandTxt);

        UserAdapter.DeleteCommand= DeleteCommand;
        UserAdapter.UpdateCommand= UpdateCommand;
        UserAdapter.InsertCommand= InsertCommand;
        UserAdapter.SelectCommand= SelectCommand;

        UserAdapter.InsertCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UserAdapter.InsertCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UserAdapter.InsertCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");
        UserAdapter.DeleteCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UserAdapter.UpdateCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UserAdapter.UpdateCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UserAdapter.UpdateCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");
    }

    internal async Task FillUsersFromDB()
    {
        try
        {
            Connection.Open();

            UserAdapter.Fill(Globals.UserSet.Tables["Users"]);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connection.Close();
        }
    }
    internal async Task ReadFromDB()
    {
        OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Access;", Connection);
        try
        {
            Connection.Open();

            OleDbDataReader reader = SelectCommand.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    Globals.UserSet.Tables["Access"].Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connection.Close();
        }
    }

    internal async Task InsertIntoDB(string name, string pass, Int32 access)
    {
        try
        {
            Connection.Open();

            UserAdapter.InsertCommand.Parameters[0].Value= name;
            UserAdapter.InsertCommand.Parameters[1].Value= pass;
            UserAdapter.InsertCommand.Parameters[2].Value= access;

            int buffer = UserAdapter.InsertCommand.ExecuteNonQuery();

            if(buffer != 0)
            {
                await Shell.Current.DisplayAlert("Database", "User successfully inserted", "OK");
            } else
            {
                await Shell.Current.DisplayAlert("Database", "User not inserted", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connection.Close();
        }
    }

    internal async Task DeleteIntoDB(string name)
    {
        try
        {
            Connection.Open();

            UserAdapter.InsertCommand.Parameters[0].Value = name;

            int buffer = UserAdapter.DeleteCommand.ExecuteNonQuery();

            if (buffer != 0)
            {
                await Shell.Current.DisplayAlert("Database", "User successfully deleted", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Database", "User not deleted", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connection.Close();
        }
    }

    internal async Task UpdateDB()
    {
        try
        {
            Connection.Open();

            int buffer = UserAdapter.Update(Globals.UserSet.Tables["Users"]);

            if (buffer != 0)
            {
                await Shell.Current.DisplayAlert("Database", "User successfully updated", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Database", "User not updated", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connection.Close();
        }
    }
}
