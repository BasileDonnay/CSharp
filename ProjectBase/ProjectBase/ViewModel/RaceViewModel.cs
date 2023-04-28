namespace ProjectBase.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class RaceViewModel : ObservableObject
{
    [ObservableProperty]
    string monTxt;

    public RaceViewModel()
    {


    }


}