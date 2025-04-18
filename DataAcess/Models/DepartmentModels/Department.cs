﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.SharedModels;

namespace DataAccess.Models.DepartmentModels
{
    public class Department : BaseEntity
    {
        public required string Name { get; set; }
        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();
    }
}
