using System;
using System.Collections.Generic;

namespace Bookstore.wpf;

public partial class OrderHuvud
{
    public int OrderId { get; set; }

    public int KundId { get; set; }

    public int? ButikId { get; set; }

    public DateOnly Orderdatum { get; set; }

    public virtual Butiker? Butik { get; set; }

    public virtual Kunder Kund { get; set; } = null!;

    public virtual ICollection<OrderRader> OrderRaders { get; set; } = new List<OrderRader>();
}
