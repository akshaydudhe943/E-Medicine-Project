using System;
using System.Collections.Generic;

namespace E_Medicine_Backend.Models;

public partial class Users
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? MobileNo { get; set; }

    public DateTime? Dob { get; set; }

    public decimal? Fund { get; set; }

    public string? Type { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
