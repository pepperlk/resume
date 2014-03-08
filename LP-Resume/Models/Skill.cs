using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LP_Resume.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public DateTime StartDate { get; set; }

        public bool Current { get; set; }


    }
}
