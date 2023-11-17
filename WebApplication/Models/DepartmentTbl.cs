using System;
using System.Collections.Generic;

namespace WebApplication.Models;

public partial class DepartmentTbl
{
    public int Did { get; set; }

    public string? DepartmentName { get; set; }

    public virtual ICollection<EmployeeTbl> EmployeeTbls { get; set; } = new List<EmployeeTbl>();
}
