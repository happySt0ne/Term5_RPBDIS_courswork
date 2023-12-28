using System.Text.Json.Serialization;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_library.models.tables;

public partial class Achievement : ISqlTable {
    public int AchievementId { get; set; }

    public string? Text { get; set; }

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public int ID => AchievementId;
}
