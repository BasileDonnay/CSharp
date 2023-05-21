
namespace ProjectBase.ViewModel;

public partial class DetailsViewModel : ObservableObject
{
    [ObservableProperty]
    string nomEntry;
    [ObservableProperty]
    string localiteEntry;
    [ObservableProperty]
    string codeEntry;
    [ObservableProperty]
    string indiceEntry;
    // Ajoutez un champ pour stocker le numéro de la course actuel
    private int currentCourseNumber = 0;

    private CourseService myService;

    public DetailsViewModel()
    {
        myService = new CourseService();
    }

    [RelayCommand]
    private async void NewCourse()
    {
        string nom = nomEntry;
        string code = codeEntry;
        string localite = localiteEntry;
        string[] indices = indiceEntry.Split(',');

        int currentCourseNumber = Preferences.Get("currentCourseNumber", 0);
        currentCourseNumber++;

        CourseModel nouvelleCourse = new CourseModel()
        {
            Nom = nom,
            Code = currentCourseNumber.ToString(),
            Localite = localite,
            NumCourse = currentCourseNumber
        };

        foreach (string indice in indices)
        {
            int currentIndiceNumber = Preferences.Get("currentIndiceNumber", 0);
            currentIndiceNumber++;

            IndiceModel newIndice = new IndiceModel()
            {
                Name = indice.Trim(),
                Number = currentIndiceNumber
            };

            nouvelleCourse.Indices.Add(newIndice);

            Preferences.Set("currentIndiceNumber", currentIndiceNumber);
        }

        try
        {
            Globals.MyList.Add(nouvelleCourse);
            Preferences.Set("currentCourseNumber", currentCourseNumber);

            NumCourse = $"Numéro de la course: {currentCourseNumber}";

            await Shell.Current.DisplayAlert("Success", "Data saved successfully", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }

        await myService.SetUsersJSONfile();
    }

    private string numCourse = "Numéro de la course: 0";
    public string NumCourse
    {
        get { return numCourse; }
        set
        {
            if (numCourse != value)
            {
                numCourse = value;
                OnPropertyChanged(nameof(NumCourse));
            }
        }
    }
}

