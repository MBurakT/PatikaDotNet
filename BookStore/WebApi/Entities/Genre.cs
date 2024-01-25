using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Genre
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Book>? Books { get; set; }

    public Genre(string name)
    {
        Name = name;
    }
}