using System;
using System.Collections.Generic;

namespace Term5_RPBDIS_library.models.tables;

public partial class Achievement
{
    public int AchievementId { get; set; }

    public string? Text { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
