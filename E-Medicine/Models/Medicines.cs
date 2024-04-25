using System;
using System.Collections.Generic;

namespace E_Medicine_Backend.Models;

public partial class Medicines
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Manufacturer { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? Discount { get; set; }

    public int? Quantity { get; set; }

    public string? Diseaces { get; set; }

    public DateTime? ExpDate { get; set; }

    public string? ImageUrl { get; set; }

    public int? Status { get; set; }

    public string? Type { get; set; }
}
