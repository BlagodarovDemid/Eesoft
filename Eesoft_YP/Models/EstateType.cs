using System;
using System.Collections.Generic;

namespace Eesoft_YP.Models;

public partial class EstateType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<EstateDemand> EstateDemands { get; set; } = new List<EstateDemand>();

    public virtual ICollection<Estate> Estates { get; set; } = new List<Estate>();
}
