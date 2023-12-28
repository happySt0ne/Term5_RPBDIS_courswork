﻿using System.Text.Json.Serialization;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_library.models.tables;

public partial class Employee : ISqlTable {
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public int? DivisionId { get; set; }

    public DateTime? HireDate { get; set; }

    public int? AchievementId { get; set; }

    public int? MarkId { get; set; }

    public virtual Achievement? Achievement { get; set; }

    [JsonIgnore]
    public virtual Division? Division { get; set; }

    public virtual Mark? Mark { get; set; }

    [JsonIgnore]
    public int ID => EmployeeId;
}
