using System;
using System.Collections.Generic;

namespace Eesoft_YP.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<EstateDemand> EstateDemands { get; set; } = new List<EstateDemand>();

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
