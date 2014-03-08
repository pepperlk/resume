using LP_Resume.Repos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LP_Resume.Controllers
{
    public class ResumeController : ApiController
    {
        private IResumeRepo _repo;
        public ResumeController(IResumeRepo repo)
        {
            _repo = repo;
        }
        [Route("api/resume")]
        public HttpResponseMessage GetResume()
        {

            return Request.CreateResponse(HttpStatusCode.OK,  _repo.GetResume());
        }


    }
}