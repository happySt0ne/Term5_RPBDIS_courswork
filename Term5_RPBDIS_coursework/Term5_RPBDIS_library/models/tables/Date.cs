using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_library.models.tables;

public partial class Date : ISqlTable {
    public int DateId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<PlannedEfficiency> PlannedEfficiencies { get; set; } = new List<PlannedEfficiency>();

    public virtual ICollection<RealEfficiency> RealEfficiencies { get; set; } = new List<RealEfficiency>();

    public int ID => DateId;
}
