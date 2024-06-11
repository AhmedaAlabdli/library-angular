using System;
using System.Collections.Generic;

namespace Library1.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishedAt { get; set; }
        public byte[]? Image { get; set; }
        public byte[]? FilePdf { get; set; }
        public string? Discription { get; set; }
        public int? CategoryId { get; set; }
    }
}
