using System;
using System.Collections.Generic;

namespace Eesoft_YP.Models;

public partial class EstateDemand
{
    public int Id { get; set; }

    public string? Address { get; set; }

    public int EstateType { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public int AgentId { get; set; }

    public int ClientId { get; set; }

    public decimal? MinArea { get; set; }

    public decimal? MaxArea { get; set; }

    public int? MinRooms { get; set; }

    public int? MaxRooms { get; set; }

    public int? MinFloor { get; set; }

    public int? MaxFloor { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual EstateType EstateTypeNavigation { get; set; } = null!;

    public virtual ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}
