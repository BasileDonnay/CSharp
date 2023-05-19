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
    [RelayCommand]
    private async void NewCourse()
    {
        string nom = NomEntry;
        string code = CodeEntry;
        string localite = LocaliteEntry;
        string[] indices = IndiceEntry.Split(',');

        // Récupérer le numéro de la course actuel à partir des préférences
        int currentCourseNumber = Preferences.Get("currentCourseNumber", 0);

        // Incrémenter le numéro de la course actuel
        currentCourseNumber++;

        CourseModel nouvelleCourse = new CourseModel()
        {
            Nom = nom,
            Code = code,
            Localite = localite,
            NumCourse = currentCourseNumber
        };

        foreach (string indice in indices)
        {
            int currentIndiceNumber = Preferences.Get("currentIndiceNumber", 0);
            currentIndiceNumber++;

            IndiceModel newIndice = new IndiceModel()
            {
                Name = indice.Trim(), //trimming any leading or trailing spaces
                Number = currentIndiceNumber
            };

            nouvelleCourse.Indices.Add(newIndice);

            // Mettre à jour les préférences de l'application avec la nouvelle valeur du compteur d'indice
            Preferences.Set("currentIndiceNumber", currentIndiceNumber);
        }

        try
        {
            Globals.MyList.Add(nouvelleCourse);

            // Mettre à jour les préférences de l'application avec la nouvelle valeur du compteur
            Preferences.Set("currentCourseNumber", currentCourseNumber);

            // Mettre à jour le label pour afficher le numéro de la course actuel
            //NumCourseLabel.Text = $"Numéro de la course: {currentCourseNumber}";

            await Shell.Current.DisplayAlert("Success", "Data saved successfully", "OK");
        }
        catch (Exception ex) // Change from InvalidOperationException to Exception
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
    }	
}