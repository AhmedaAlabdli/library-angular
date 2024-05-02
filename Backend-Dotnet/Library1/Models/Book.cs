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
    }
}
