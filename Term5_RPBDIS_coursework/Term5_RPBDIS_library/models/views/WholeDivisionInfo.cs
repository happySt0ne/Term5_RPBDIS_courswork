using System;
using System.Collections.Generic;

namespace Term5_RPBDIS_library.models.views;

public partial class WholeDivisionInfo
{
    public string? DivisionName { get; set; }

    public int? Mark { get; set; }

    public int? PlannedEfficiency { get; set; }

    public int? RealEfficiency { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
