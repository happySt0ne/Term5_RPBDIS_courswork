using System;
using System.Collections.Generic;

namespace Term5_RPBDIS_library.models.tables;

public partial class Mark
{
    public int MarkId { get; set; }

    public int? Value { get; set; }

    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
