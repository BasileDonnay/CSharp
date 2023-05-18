
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.ViewModel;

public partial class LoginViewModel: BaseViewModel
{
    UserManagementServices MyDBService;
    public LoginViewModel(UserManagementServices myDBService)
    {
        MyDBService = myDBService;
        MyDBService.ConfigTools();
    }

    public async Task LoginUser(string nom, string mdp)
    {
        IsBusy = true;
        try
        {
            await MyDBService.LoginDB(nom, mdp);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        IsBusy = false;
    }
}
