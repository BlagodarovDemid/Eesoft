using System;
using System.Collections.Generic;

namespace Eesoft_YP.Models;

public partial class Estate
{
    public int Id { get; set; }

    public string? AddressCity { get; set; }

    public int? DistrictId { get; set; }

    public int? AddressHouse { get; set; }

    public int? AddressNumber { get; set; }

    public int EstateType { get; set; }

    public int? CoordinateLatitude { get; set; }

    public int? CoordinateLongitude { get; set; }

    public decimal? TotalArea { get; set; }

    public int? TotalFloors { get; set; }

    public int? Rooms { get; set; }

    public int? Floor { get; set; }

    public virtual District? District { get; set; }

    public virtual EstateType EstateTypeNavigation { get; set; } = null!;
}
