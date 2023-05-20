namespace ProjectBase.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
[QueryProperty(nameof(MaCourse), "MyCourseCode")]
public partial class RaceViewModel : ObservableObject
{
    [ObservableProperty]
    int chronometre = 0;
    [ObservableProperty]
    String monTxt;

    [ObservableProperty]
    string maCourse;

    [ObservableProperty]
    string courseName;

    [ObservableProperty]
    string courseLocalite;

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
        //MainThread.BeginInvokeOnMainThread();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //await Shell.Current.DisplayAlert("test", monTxt, "OK");
            Chronometre += 1;
        });
    }
}