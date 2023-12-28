using System.Text.Json.Serialization;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_library.models.tables;

public partial class Division : ISqlTable {
    public int DivisionId { get; set; }

    public string? Name { get; set; }

    public int? MarkId { get; set; }

    public int? PlannedEfficiencyId { get; set; }

    public int? RealEfficiencyId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Mark? Mark { get; set; }

    public virtual PlannedEfficiency? PlannedEfficiency { get; set; }

    public virtual RealEfficiency? RealEfficiency { get; set; }

    [JsonIgnore]
    public int ID => DivisionId;
}
