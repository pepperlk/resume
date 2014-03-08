using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LP_Resume.Models
{
    public class Resume
    {
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Full address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Contact email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Contact Email
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Work summary
        /// </summary>
        public string Summary { get; set; }

        public string Title { get; set; }

        public string Headshot { get; set; }

        public IEnumerable<Employment> Experience { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

    }
}