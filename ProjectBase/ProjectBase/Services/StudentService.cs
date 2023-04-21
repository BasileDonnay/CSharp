using System.Text.Json;
namespace ProjectBase.Services;
public class StudentService
{
    
   
    public StudentService()
    { }


    public async Task SetUsersJSONfile()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StudentServer", "StudentService");


        using FileStream fileStream = File.Create(filePath);

        await JsonSerializer.SerializeAsync(fileStream, Globals.MyList);
        await fileStream.DisposeAsync();

    }


    public async Task<List<CourseModel>> GetStudents()
    {
        List<CourseModel> students=new();

        using var stream = await FileSystem.OpenAppPackageFileAsync("students.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        students = JsonSerializer.Deserialize<List<CourseModel>>(contents); 
        return students;
    }

}