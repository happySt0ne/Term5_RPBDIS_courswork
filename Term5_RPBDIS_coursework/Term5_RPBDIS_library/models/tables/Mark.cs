using System;
using System.Collections.Generic;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_library.models.tables;

public partial class Mark : ISqlTable
{
    public int MarkId { get; set; }

    public int? Value { get; set; }

    public virtual ICollection<Division> Divisions { get; set; } = new List<Division>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public int ID => MarkId;
}
