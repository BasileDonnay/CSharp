global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;
global using ProjectBase.ViewModel;
global using ProjectBase.View;
global using ProjectBase.Model;
global using ProjectBase.Services;

global using System.Text.Json;
global using System.Diagnostics;
global using System.Data;

public class Globals
{
    public static List<CourseModel> MyList = new List<CourseModel>();
    public static DataSet UserSet = new();
    public static bool isConnected = false;
    public static bool isAdmin = false;
}