using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LP_Resume.Models
{
    public class Employment
    {
        public string Employer { get; set; }

        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool Current { get; set; }

        public bool MayContact { get; set; }

        public string Summary { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }

    }
}
