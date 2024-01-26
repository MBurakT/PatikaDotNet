using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Author
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ICollection<Book>? Books { get; set; }

    public Author(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
}