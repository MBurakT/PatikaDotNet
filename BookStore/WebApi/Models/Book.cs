using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Models;

public class Book
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public int GenreId { get; set; }

    public Book(string title, int pageCount, DateTime publishDate, int genreId)
    {
        Title = title;
        PageCount = pageCount;
        PublishDate = publishDate;
        GenreId = genreId;
    }
}