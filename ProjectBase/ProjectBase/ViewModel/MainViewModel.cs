
using System.Collections.ObjectModel;

namespace ProjectBase.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    string monTexte = " Go to details";
    [ObservableProperty]
    string activeTarget;
    [ObservableProperty]
    string monCode;


    StudentService myService;
    public ObservableCollection<CourseModel> MyShownList { get; } = new();

    DeviceOrientationServices MyDeviceOrientationService;
    public MainViewModel(StudentService myService)
    {
        this.myService = myService;

        this.MyDeviceOrientationService = new DeviceOrientationServices();
        MyDeviceOrientationService.ConfigureScanner();
        MyDeviceOrientationService.SerialBuffer.Changed += SerialBuffer_Changed;

    }


    private void SerialBuffer_Changed(object sender, EventArgs e)
    {

        DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

        //ID c'est la code QR que l'on va scanner
        string codeQr = myQueue.Dequeue().ToString();

        foreach (var data in Globals.MyList)
        {
            //le .Code c'est un c'est attribut du course model, si c'est == alors je l'ajoute dans ma liste
            if (codeQr == data.Code) MyShownList.Add(data);
            MonCode = codeQr;
        }
    }

    [RelayCommand]
    async Task GoToDetailsPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }
    //ici c'est on est renvoyer vers la page home

    [RelayCommand]
    async Task GoToHomePage(string data)
    {
        await Shell.Current.GoToAsync(nameof(Hom), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }


  
    public void RefreshPage()
    {
        MyShownList.Clear();

        foreach ( var item in Globals.MyList )
        {
            MyShownList.Add(item);
        }
    }





    [RelayCommand]
    async Task GetObject()
    {

        if (IsBusy) return;
        StudentService MyService = new();

        bool test = MyDeviceOrientationService.mySerialPort.IsOpen;
        try
        {
            IsBusy = true;
            Globals.MyList = await MyService.GetStudents();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally { IsBusy = false; }

        MyShownList.Clear();

        


        foreach (CourseModel stu in Globals.MyList)
        {
            MyShownList.Add(stu);
        }
    }
}