using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class LagerSaldo
{
    public int ButikId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public int Antal { get; set; }

    public virtual Butiker Butik { get; set; } = null!;

    public virtual Böcker Isbn13Navigation { get; set; } = null!;
}
