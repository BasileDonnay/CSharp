

namespace ProjectBase.ViewModel
{
    // D�corateurs de propri�t�s pour la liaison de requ�te
    [QueryProperty(nameof(MonTxt), "Databc")]

    // variable que j'ai importer de ma methode GoToRacePage de ma mainViewModel
    [QueryProperty(nameof(MaCourse), "MyCourseCode")]
    public partial class RaceViewModel : ObservableObject
    {
        DeviceOrientationServices MyDeviceOrientationService;

        // Varibake que je d�clare qui vont me servir pour que je puisse y mettre mes valeurs a afficher 
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

        [ObservableProperty]
        int scanTime;




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
                // Si l'ouverture du port COM �choue, simuler un scan de code QR
              
            }
        
        }

        private void SerialBuffer_Changed(object sender, EventArgs e)
        {
            DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

            // ID c'est le code QR que l'on va scanner
            string codeQr = myQueue.Dequeue().ToString();

            foreach (var data in Globals.MyList)
            {
                // Le .Code c'est un attribut du course model, si c'est == alors je l'ajoute dans ma variable 
                if (codeQr == data.Code)
                    // variable que j'ai vais afficher 
                    CourseIndiceName = string.Join(", ", data.Indices.Select(indice => indice.Name));
                // Capture le temps actuel lors du scan
                ScanTime = Chronometre;

            }
        }


        // cette methode va etre appeler quand je vais cliquer sur une course et que le code de cette course va 
        // correspondre avec le code d'une de mes course pr�sente dans ma liste de course
        // elle va mettre les valeurs de ma course dans mes variables Observable
        // puis les afficher avec mon Xaml
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