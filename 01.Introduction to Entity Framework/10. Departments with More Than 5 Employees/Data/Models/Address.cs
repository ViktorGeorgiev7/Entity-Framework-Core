﻿using System;
using System.Collections.Generic;

namespace _10._Departments_with_More_Than_5_Employees.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            Employees = new HashSet<Employee>();
        }

        public int AddressId { get; set; }
        public string AddressText { get; set; } = null!;
        public int? TownId { get; set; }

        public virtual Town? Town { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
