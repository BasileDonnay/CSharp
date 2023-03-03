global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;

global using ProjectBase.ViewModel;
global using ProjectBase.View;
global using ProjectBase.Model;
global using ProjectBase.Services;

global using System.Text.Json;
global using System.Diagnostics;

public class Globals
{
    public static List<StudentModel> MyList = new List<StudentModel>();
    internal static Queue<string> SerialBuffer = new Queue<string>();
}