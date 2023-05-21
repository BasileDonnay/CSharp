

namespace ProjectBase.ViewModel
{
    // Décorateurs de propriétés pour la liaison de requête
    [QueryProperty(nameof(MonTxt), "Databc")]
    [QueryProperty(nameof(MaCourse), "MyCourseCode")]
    public partial class RaceViewModel : ObservableObject
    {
        DeviceOrientationServices MyDeviceOrientationService;

        // Propriétés observables
        [ObservableProperty]
        int chronometre = 0;
        [ObservableProperty]
        string monTxt;

        [ObservableProperty]
        string maCourse;

        [ObservableProperty]
        string courseName;

        [ObservableProperty]
        string courseLocalite;

        [ObservableProperty]
        string courseIndiceName;

        [ObservableProperty]
        List<string> courseIndices;

        private string courseIndicesAsString;
      
        public string CourseIndicesAsString
        {
            get => courseIndicesAsString;
            set => SetProperty(ref courseIndicesAsString, value);
        }

        public RaceViewModel()
        {
            var timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => UpdateTimer();
            timer.Start();

         
            this.MyDeviceOrientationService = new DeviceOrientationServices();
            try
            {
                // Essayer de configurer le scanner
                MyDeviceOrientationService.ConfigureScanner();
                MyDeviceOrientationService.mySerialPort.Open();
                MyDeviceOrientationService.SerialBuffer.Changed += SerialBuffer_Changed;
            }
            catch (Exception)
            {
                // Si l'ouverture du port COM échoue, simuler un scan de code QR
              
            }
        
        }

        private void SerialBuffer_Changed(object sender, EventArgs e)
        {
            DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

            // ID c'est le code QR que l'on va scanner
            string codeQr = myQueue.Dequeue().ToString();

            foreach (var data in Globals.MyList)
            {
                // Le .Code c'est un attribut du course model, si c'est == alors je l'ajoute dans ma liste que je vais afficher dans les details de la course
                if (codeQr == data.Code)

                    CourseIndiceName = string.Join(", ", data.Indices.Select(indice => indice.Name));
                // MonCode = codeQr;
            }
        }



        partial void OnMaCourseChanged(string? value)
        {
            foreach (var course in Globals.MyList)
            {
                if (course.Code == MaCourse)
                {
                    CourseName = course.Nom;
                    CourseLocalite = course.Localite;
                    CourseIndices = course.Indices.Select(indice => indice.Name).ToList();
                    CourseIndicesAsString = string.Join(", ", CourseIndices);

                    break;
                }
            }
        }

        void UpdateTimer()
        {
            // MainThread.BeginInvokeOnMainThread();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                //await Shell.Current.DisplayAlert("test", monTxt, "OK");
                Chronometre += 1;
            });
        }
    }
}