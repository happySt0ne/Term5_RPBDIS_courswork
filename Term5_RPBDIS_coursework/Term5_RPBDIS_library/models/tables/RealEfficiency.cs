using System.Text.Json.Serialization;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_library.models.tables;

public partial class RealEfficiency : ISqlTable {
    public int RealEfficiencyId { get; set; }

    public int? DateId { get; set; }

    public int? Efficiecy { get; set; }

    public virtual Date? Date { get; set; }

    [JsonIgnore]
    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();

    public int ID => RealEfficiencyId;
}
