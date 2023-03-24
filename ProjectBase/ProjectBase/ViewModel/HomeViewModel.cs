namespace ProjectBase.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
   public string monTxt;
    public HomeViewModel()
	{
       
       
	}
}