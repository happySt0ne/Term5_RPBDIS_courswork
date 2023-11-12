using System;
using System.Collections.Generic;

namespace Term5_RPBDIS_library.models.tables;

public partial class RealEfficiency
{
    public int RealEfficiencyId { get; set; }

    public int? DateId { get; set; }

    public int? Efficiecy { get; set; }

    public virtual Date? Date { get; set; }

    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();
}
