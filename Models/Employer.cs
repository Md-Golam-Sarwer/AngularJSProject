using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class Employer
    {
        [Key]
        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
        public string ContactAddress { get; set; }
        public string MobileNo { get; set; }

        public DateTime DOB { get; set; }
        public bool IsActive { get; set; }


        public int BranchID { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
