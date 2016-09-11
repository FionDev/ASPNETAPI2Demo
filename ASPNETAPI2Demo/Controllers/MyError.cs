using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASPNETAPI2Demo.Controllers
{
    public class MyError
    {
        public string SubStatusCode { get; set; }

        public string Error_Message { get; set; }

        public MyError()
        {
        }
    }
}
