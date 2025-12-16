using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class Böcker
{
    public string Isbn13 { get; set; } = null!;

    public string Titel { get; set; } = null!;

    public string Språk { get; set; } = null!;

    public decimal Pris { get; set; }

    public DateOnly? Utgivningsdatum { get; set; }

    public int AntalSidor { get; set; }

    public int? GenreId { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();

    public virtual ICollection<OrderRader> OrderRaders { get; set; } = new List<OrderRader>();

    public virtual ICollection<Recensioner> Recensioners { get; set; } = new List<Recensioner>();

    public virtual ICollection<Författare> Författares { get; set; } = new List<Författare>();
}
