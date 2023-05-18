
using ProjectBase.Model;
using Microsoft.Maui.Storage;


namespace ProjectBase.View;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(DetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;


    }

    // Ajoutez un champ pour stocker le numéro de la course actuel
    private int currentCourseNumber = 0;
    private async void OnAjouterClicked(object sender, EventArgs e)
    {
        string nom = NomEntry.Text;
        string code = CodeEntry.Text;
        string localite = LocaliteEntry.Text;
        string indice = IndiceEntry.Text;

        // Récupérer le numéro de la course actuel à partir des préférences
        int currentCourseNumber = Preferences.Get("currentCourseNumber", 0);

        // Incrémenter le numéro de la course actuel
        currentCourseNumber++;

        CourseModel nouvelleCourse = new CourseModel()
        {
            Nom = nom,
            Code = code,
            Localite = localite,
            Indice = indice,
            NumCourse = currentCourseNumber
        };

        try
        {
            Globals.MyList.Add(nouvelleCourse);

            // Mettre à jour les préférences de l'application avec la nouvelle valeur du compteur
            Preferences.Set("currentCourseNumber", currentCourseNumber);

            // Mettre à jour le label pour afficher le numéro de la course actuel
            NumCourseLabel.Text = $"Numéro de la course: {currentCourseNumber}";

            await DisplayAlert("Success", "Data saved successfully", "OK");
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }




}