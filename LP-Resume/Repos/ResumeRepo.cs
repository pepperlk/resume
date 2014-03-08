using LP_Resume.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LP_Resume.Repos
{
    public class ResumeRepo : IResumeRepo
    {
        public Models.Resume GetResume()
        {
            return JsonConvert.DeserializeObject<Resume>(File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/resume.json")));


           
        }
    }
}