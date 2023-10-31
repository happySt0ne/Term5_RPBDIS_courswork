using System;
using System.Collections.Generic;

namespace Term5_RPBDIS_library.models.views;

public partial class CompareRealPlannedEfficiency
{
    public string? DivisionName { get; set; }

    public int? RealEfficiecy { get; set; }

    public int? PlannedEfficiecy { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
