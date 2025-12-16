using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class Kunder
{
    public int KundId { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public string Epost { get; set; } = null!;

    public string? Telefon { get; set; }

    public string? Gatuadress { get; set; }

    public string? Postnummer { get; set; }

    public string? Stad { get; set; }

    public string? Land { get; set; }

    public virtual ICollection<OrderHuvud> OrderHuvuds { get; set; } = new List<OrderHuvud>();

    public virtual ICollection<Recensioner> Recensioners { get; set; } = new List<Recensioner>();
}
