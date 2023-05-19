
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.ViewModel;

public partial class UserViewModel: BaseViewModel
{
    [ObservableProperty]
    string nomEntry;
    [ObservableProperty]
    string mDPEntry;
    UserManagementServices MyDBService;
    List<User> MyUserList = new List<User>();
    public UserViewModel(UserManagementServices myDBService)
    {
        MyDBService = myDBService;
        MyDBService.ConfigTools();
    }

    public ObservableCollection<User> ShownList { get; set; } = new();
    [RelayCommand]
    async void FillUsers()
    {
        if(Globals.isAdmin) {
            IsBusy= true;

            if (Globals.UserSet.Tables["Access"].Rows.Count == 0)
            {
                await MyDBService.ReadFromDB();
            }

            if (Globals.UserSet.Tables["Users"].Rows.Count != 0)
            {
                Globals.UserSet.Tables["Users"].Clear();
            }
            await MyDBService.FillUsersFromDB();

           
            try
            {
                MyUserList = Globals.UserSet.Tables["Users"].AsEnumerable().Select(m => new User()
                {
                    User_ID = m.Field<Int16>("User_ID"),
                    UserName = m.Field<string>("UserName"),
                    UserPassword = m.Field<string>("UserPassword"),
                    UserAccessType = m.Field<Int16>("UserAccessType"),
                }).ToList();
            } catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
            }

            ShownList.Clear();
            foreach(var item in MyUserList)
            {
                ShownList.Add(item);
            }

            IsBusy= false;
        }
        else
        {
            await Shell.Current.DisplayAlert("Database", "Not Connected as Admin", "OK");
        }
    }

    [RelayCommand]
    public async Task InsertUser()
    {
        IsBusy = true;
        try
        {
            await MyDBService.InsertIntoDB(NomEntry, MDPEntry, 2);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task MakeAdministrator(string name)
    {
        IsBusy = true;
        string pass = "";


        foreach(var user in MyUserList)
        {
            if(user.UserName == name)
            {
                pass = user.UserPassword;
            }
        }
        try
        {
            await MyDBService.DeleteIntoDB(name);
            await MyDBService.InsertIntoDB(name, pass, 1);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async Task DeleteUser(string name)
    {
        IsBusy = true;
        try
        {
            await MyDBService.DeleteIntoDB(name);
            FillUsers();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        IsBusy = false;
    }
}
