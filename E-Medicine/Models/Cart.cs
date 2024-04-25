using System;
using System.Collections.Generic;

namespace E_Medicine_Backend.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? MedicineId { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? Discount { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Users? User { get; set; }
}
