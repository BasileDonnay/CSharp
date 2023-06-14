using System.Text.Json;
namespace ProjectBase.Services;
public class CourseService
{


    public CourseService()
    { }

 // methode pour ecrire dans le fichier Json
    public async Task SetUsersJSONfile()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "CourseService.json");


        using FileStream fileStream = File.Create(filePath);

        await JsonSerializer.SerializeAsync(fileStream, Globals.MyList);
        await fileStream.DisposeAsync();

    }

    // methode pour afficher les infos du fichier Json
    public async Task<List<CourseModel>> GetCourse()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "CourseService.json");
        List<CourseModel> courses = null;

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            courses = JsonSerializer.Deserialize<List<CourseModel>>(json);
        }

        return courses ?? new List<CourseModel>();
    }


}