namespace ProjectBase.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class RaceViewModel : ObservableObject
{
    [ObservableProperty]
    int chronometre = 0;
    [ObservableProperty]
    String monTxt;

    public RaceViewModel()
    {
        var timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) => UpdateTimer();
        timer.Start();
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