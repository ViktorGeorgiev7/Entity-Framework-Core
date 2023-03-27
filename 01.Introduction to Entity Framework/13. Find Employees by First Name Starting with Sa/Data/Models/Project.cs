﻿using System;
using System.Collections.Generic;

namespace _13._Find_Employees_by_First_Name_Starting_with_Sa.Data.Models
{
    public partial class Project
    {
        public Project()
        {
            Employees = new HashSet<Employee>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
