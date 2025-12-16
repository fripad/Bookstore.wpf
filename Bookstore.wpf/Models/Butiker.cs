using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class Butiker
{
    public int ButikId { get; set; }

    public string Namn { get; set; } = null!;

    public string Gatuadress { get; set; } = null!;

    public string Postnummer { get; set; } = null!;

    public string Stad { get; set; } = null!;

    public string Land { get; set; } = null!;

    public string? Telefon { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();

    public virtual ICollection<OrderHuvud> OrderHuvuds { get; set; } = new List<OrderHuvud>();
}
