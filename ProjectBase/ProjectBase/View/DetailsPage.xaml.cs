using Microsoft.UI.Xaml.Controls;
using ProjectBase.Model;

namespace ProjectBase.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;

		
	}

    private async void OnAjouterClicked(object sender, EventArgs e)
    {
        string nom = NomEntry.Text;
        string code = CodeEntry.Text;
        string localite = LocaliteEntry.Text;
        try
        {
            CourseModel nouvelleCourse = new CourseModel()
            {
                Nom = nom,
                Code = code,
                Localite = localite
            };
           

            StudentService studentService = new StudentService();
            List<CourseModel> courseModels = await studentService.GetStudents();
            courseModels.Add(nouvelleCourse);

            string studentJsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "students.json");
            using (var stream = new FileStream(studentJsonFilePath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(stream, courseModels);
            }

            await DisplayAlert("Success", "Data saved successfully", "OK");
        }
        catch (InvalidOperationException ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

}