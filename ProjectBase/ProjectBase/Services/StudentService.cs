using System.Text.Json;
namespace ProjectBase.Services;
public class StudentService
{
    
    public StudentService()
    { }
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