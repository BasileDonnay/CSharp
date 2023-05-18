using System.Text.Json;
namespace ProjectBase.Services;
public class CourseService
{


    public CourseService()
    { }


    public async Task SetUsersJSONfile()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CourseServer", "CourseService");


        using FileStream fileStream = File.Create(filePath);

        await JsonSerializer.SerializeAsync(fileStream, Globals.MyList);
        await fileStream.DisposeAsync();

    }


    public async Task<List<CourseModel>> GetCourse()
    {
        List<CourseModel> courses = new();

        using var stream = await FileSystem.OpenAppPackageFileAsync("courses.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        courses = JsonSerializer.Deserialize<List<CourseModel>>(contents);
        return courses;
    }

}