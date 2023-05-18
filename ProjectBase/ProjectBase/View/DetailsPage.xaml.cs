
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

    // Ajoutez un champ pour stocker le num�ro de la course actuel
    private int currentCourseNumber = 0;
    private async void OnAjouterClicked(object sender, EventArgs e)
    {
        string nom = NomEntry.Text;
        string code = CodeEntry.Text;
        string localite = LocaliteEntry.Text;
        string indice = IndiceEntry.Text;

        // R�cup�rer le num�ro de la course actuel � partir des pr�f�rences
        int currentCourseNumber = Preferences.Get("currentCourseNumber", 0);

        // Incr�menter le num�ro de la course actuel
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

            // Mettre � jour les pr�f�rences de l'application avec la nouvelle valeur du compteur
            Preferences.Set("currentCourseNumber", currentCourseNumber);

            // Mettre � jour le label pour afficher le num�ro de la course actuel
            NumCourseLabel.Text = $"Num�ro de la course: {currentCourseNumber}";

            await DisplayAlert("Success", "Data saved successfully", "OK");
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }




}