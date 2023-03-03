
using System.Collections.ObjectModel;

namespace ProjectBase.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    string monTexte = " Go to details";

    DeviceOrientationServices MyDeviceOrientationService;
    StudentService myService;
    public ObservableCollection<StudentModel> MyShownList { get; } = new();


    public MainViewModel(StudentService myService)
    {
        this.myService = myService;
        this.MyDeviceOrientationService = new DeviceOrientationServices();

        MyDeviceOrientationService.ConfigureScanner();

    }
    [RelayCommand]
    async Task GoToDetailsPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    async Task GetObject()
    {
        try
        {
            Globals.MyList = await myService.GetStudents();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        MyShownList.Clear();

        foreach (StudentModel stu in Globals.MyList)
        {
            MyShownList.Add(stu);
        }
    }
}