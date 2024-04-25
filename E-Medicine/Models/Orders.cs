using System;
using System.Collections.Generic;

namespace E_Medicine_Backend.Models;

public partial class Orders
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? OrderNumber { get; set; }

    public decimal? OrderTotal { get; set; }

    public String? OrderStatus { get; set; }

    public virtual ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

    public virtual Users? User { get; set; }
}
