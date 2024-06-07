using System;
using System.Collections.Generic;

namespace Eesoft_YP.Models;

public partial class District
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Area { get; set; }

    public virtual ICollection<Estate> Estates { get; set; } = new List<Estate>();
}
