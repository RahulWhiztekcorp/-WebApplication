using System;
using System.Collections.Generic;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication.Models;

public partial class EmployeeTbl
{
    public int Eid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public long? MobileNumber { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public int? DepartmentId { get; set; }

    public decimal? Salary { get; set; }

    public bool? IsFlag { get; set; }

    public virtual DepartmentTbl? Department { get; set; }
}
