namespace ProjectBase.Model;

public class StudentModel
{
    public StudentModel(int id, string name, string email, string surname, string photo)
    {
        Id = id;
        Name = name;
        Email = email;
        Surname = surname;
        Photo = photo;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Surname { get; set; }
    public string Photo { get; set; }


}