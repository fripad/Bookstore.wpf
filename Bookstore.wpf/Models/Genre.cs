using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Namn { get; set; } = null!;

    public string? Beskrivning { get; set; }

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();
}
