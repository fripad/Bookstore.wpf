using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class KundRecensionsStatistik
{
    public int KundId { get; set; }

    public string Namn { get; set; } = null!;

    public int? AntalRecensioner { get; set; }

    public decimal RabattProcent { get; set; }
}
