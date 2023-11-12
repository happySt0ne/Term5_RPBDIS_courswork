using System;
using System.Collections.Generic;

namespace Term5_RPBDIS_library.models.tables;

public partial class Date
{
    public int DateId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<PlannedEfficiency> PlannedEfficiencies { get; set; } = new List<PlannedEfficiency>();

    public virtual ICollection<RealEfficiency> RealEfficiencies { get; set; } = new List<RealEfficiency>();
}
