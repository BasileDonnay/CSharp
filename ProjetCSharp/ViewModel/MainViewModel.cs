namespace ProjetCSharp.ViewModel;

public partial class MainViewModel : ObservableObject
{

    int count = 0;
    [ObservableProperty]
    string text = "salueteet";

    public MainViewModel()
	{
		
	}

    [RelayCommand]
    async Task GoToDetailsPage (string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }
}