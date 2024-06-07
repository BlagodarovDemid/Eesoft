using System;
using System.Collections.Generic;

namespace Eesoft_YP.Models;

public partial class Deal
{
    public int Id { get; set; }

    public int? DemandId { get; set; }

    public int? SupplyId { get; set; }

    public virtual EstateDemand? Demand { get; set; }

    public virtual Supply? Supply { get; set; }
}
