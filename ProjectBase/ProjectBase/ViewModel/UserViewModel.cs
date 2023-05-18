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
    UserManagementServices MyDBService;
    public UserViewModel(UserManagementServices myDBService)
    {
        MyDBService = myDBService;
        MyDBService.ConfigTools();
    }

    public ObservableCollection<User> ShownList { get; set; } = new();
    [RelayCommand]
    async void FillUsers()
    {
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

        List<User> MyList = new List<User>();
        try
        {
            MyList = Globals.UserSet.Tables["Users"].AsEnumerable().Select(m => new User()
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
        foreach(var item in MyList)
        {
            ShownList.Add(item);
        }

        IsBusy= false;
    }

    [RelayCommand]
    async Task InsertUser()
    {
        IsBusy= true;
        try
        {
            //TODO lier les 3 paramètres à un textfield xaml (créer une page connection)
            await MyDBService.InsertIntoDB("Fred", "Bonjour", 1);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        IsBusy = false;
    }

    [RelayCommand]
    async Task DeleteUser()
    {
        IsBusy = true;
        try
        {
            //TODO admin remplit un textfield pour supprimer
            await MyDBService.DeleteIntoDB("Fred");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        IsBusy = false;
    }
    [RelayCommand]
    async Task UpdateDB()
    {
        IsBusy = true;
        try
        {
            await MyDBService.UpdateDB();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        IsBusy = false;
    }
}
