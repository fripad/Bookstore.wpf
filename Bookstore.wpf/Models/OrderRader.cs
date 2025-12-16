using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class OrderRader
{
    public int OrderRadId { get; set; }

    public int OrderId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public int Antal { get; set; }

    public decimal Pris { get; set; }

    public virtual Böcker Isbn13Navigation { get; set; } = null!;

    public virtual OrderHuvud Order { get; set; } = null!;
}
