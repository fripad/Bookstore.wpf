using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class Recensioner
{
    public int RecensionId { get; set; }

    public int KundId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public int Betyg { get; set; }

    public string? Kommentar { get; set; }

    public DateOnly SkapadDatum { get; set; }

    public virtual Böcker Isbn13Navigation { get; set; } = null!;

    public virtual Kunder Kund { get; set; } = null!;
}
