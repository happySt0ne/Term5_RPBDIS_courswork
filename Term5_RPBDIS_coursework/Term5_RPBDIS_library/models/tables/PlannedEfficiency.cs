using System;
using System.Collections.Generic;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_library.models.tables;

public partial class PlannedEfficiency : ISqlTable
{
    public int PlannedEfficiencyId { get; set; }

    public int? DateId { get; set; }

    public int? Efficiecy { get; set; }

    public virtual Date? Date { get; set; }

    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();

    public int ID => PlannedEfficiencyId;
}
