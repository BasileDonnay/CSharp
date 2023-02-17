using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCSharp.Model
{
    public class StudentModel
    {
        public StudentModel(string id, string surname, string name, string email, string foto)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.email = email;
            this.foto = foto;
        }

        public string id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
    }
}
