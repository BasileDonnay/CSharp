using System.Text.Json;
namespace ProjectBase.Services;
public class StudentService
{
    List<StudentModel> students;
    public StudentService()
    { }
    public async Task<List<StudentModel>> GetStudents()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("students.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        students = JsonSerializer.Deserialize<List<StudentModel>>(contents); 
        return students;
    }
}