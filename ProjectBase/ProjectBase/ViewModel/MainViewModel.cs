using System.Collections.ObjectModel;

namespace ProjectBase.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        // Propri�t�s observables
        [ObservableProperty]
        string monTexte = "Go to details";
        [ObservableProperty]
        string activeTarget;
        [ObservableProperty]
        string monCode;

        CourseService myService;
        public ObservableCollection<User> ShownList { get; set; } = new ObservableCollection<User>();
        public ObservableCollection<CourseModel> MyShownList { get; } = new ObservableCollection<CourseModel>();

        DeviceOrientationServices MyDeviceOrientationService;

        public MainViewModel(CourseService myService)
        {
            this.myService = myService;

            this.MyDeviceOrientationService = new DeviceOrientationServices();
            MyDeviceOrientationService.ConfigureScanner();
            MyDeviceOrientationService.SerialBuffer.Changed += SerialBuffer_Changed;

            CreateUserTables MyUserTables = new CreateUserTables();
        }

        private void SerialBuffer_Changed(object sender, EventArgs e)
        {
            DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

            // ID c'est le code QR que l'on va scanner
            string codeQr = myQueue.Dequeue().ToString();

            foreach (var data in Globals.MyList)
            {
                // Le .Code c'est un attribut du course model, si c'est == alors je l'ajoute dans ma liste
                if (codeQr == data.Code)
                    MyShownList.Add(data);
                // MonCode = codeQr;
            }
        }

        [RelayCommand]
        async Task GoToDetailsPage(string data)
        {
            if (!Globals.isConnected)
            {
                await Shell.Current.DisplayAlert("Database", "Not Connected", "OK");
                return;
            }

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
            {
                {"Databc", data }
            });
        }

        [RelayCommand]
        async Task GoToRacePage(string courseCode)
        {

            await Shell.Current.GoToAsync(nameof(RacePage), true, new Dictionary<string, object>
            {
                {"MyCourseCode", courseCode }
            });
        }

        [RelayCommand]
        async Task GoToUserPage(string data)
        {
            await Shell.Current.GoToAsync(nameof(UserPage), true, new Dictionary<string, object>
            {
                {"Databc", data }
            });
        }

        [RelayCommand]
        async Task GoToLoginPage(string data)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage), true, new Dictionary<string, object>
            {
                {"Databc", data }
            });
        }

        public void RefreshPage()
        {
            MyShownList.Clear();

            foreach (var item in Globals.MyList)
            {
                MyShownList.Add(item);
                Debug.WriteLine(item.Nom);
            }
        }

        // methode pour afficher les courses
        [RelayCommand]
        async Task ShowCourses()
        {
            if (!Globals.isConnected)
            {
                await Shell.Current.DisplayAlert("Database", "Not Connected", "OK");
                return;
            }

            if (IsBusy) return;

            CourseService MyService = new CourseService();

            bool test = MyDeviceOrientationService.mySerialPort.IsOpen;
            try
            {
                IsBusy = true;
                Globals.MyList = await MyService.GetCourse();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Students: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

            MyShownList.Clear();

            foreach (CourseModel stu in Globals.MyList)
            {
                MyShownList.Add(stu);
            }
        }

        [RelayCommand]
        public async Task SetUsersJSONfile()
        {
            CourseService MyService = new CourseService();

            myService.SetUsersJSONfile();
        }
    }
}
