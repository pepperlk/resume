using LP_Resume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LP_Resume.Repos
{
    public interface IResumeRepo : IDep
    {
        Resume GetResume();
    }
}