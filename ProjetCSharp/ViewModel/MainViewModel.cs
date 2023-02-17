using System.Collections.ObjectModel;

namespace ProjetCSharp.ViewModel;

public partial class MainViewModel : ObservableObject
{

    int count = 0;
    [ObservableProperty]
    string text = "salueteet";

    public ObservableCollection <StudentModel> MyShowList { get; } = new();

    public MainViewModel()
	{
		StudentModel student1 = new StudentModel("12", "ddd", "dded", "ssssss", "csgo_hugs.png");
        StudentModel student2 = new StudentModel("12", "ddd", "dded", "ssssss", "csgo_hugs.png");

        Globals.MyList.Add(student1);
        Globals.MyList.Add(student2);

        foreach(StudentModel student in Globals.MyList) 
        {
            MyShowList.Add(student);
        }
    }

    [RelayCommand]
    async Task GoToDetailsPage (string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    async Task GetObject()
    {
        StudentModel student1 = new StudentModel("12", "ddd", "dded", "ssssss", "csgo_hugs.png");

        Globals.MyList.Add(student1);
        MyShowList.Add(student1);
    }
}