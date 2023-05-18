namespace ProjectBase.Model;

public class IndiceModel
{
    public string Name { get; set; }
    public int Number { get; set; }
}

public class CourseModel
{
    public string Nom { get; set; }
    public string Localite { get; set; }
    public string Code { get; set; }
    public List<IndiceModel> Indices { get; set; } = new List<IndiceModel>();
    public int NumCourse { get; set; }
}