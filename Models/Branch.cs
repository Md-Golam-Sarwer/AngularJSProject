using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class Branch
    {
       
        public int? BranchID { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        [Required]
        [Display(Name = "Branch Location")]
        public string BranchLocation { get; set; }
        [Required]
        [Display(Name = "Division")]
        public string Division { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
       
    }
}
