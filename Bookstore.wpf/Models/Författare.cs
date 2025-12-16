using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class Författare
{
    public int Id { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public DateOnly? Födelsedatum { get; set; }

    public DateOnly? Avlidit { get; set; }

    public virtual ICollection<Böcker> Isbn13s { get; set; } = new List<Böcker>();
}
