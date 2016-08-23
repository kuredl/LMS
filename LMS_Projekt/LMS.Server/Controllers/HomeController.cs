using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LMS_Server.Controllers
{
    public class HomeController : ApiController
    {        
        [HttpGet, Authorize]
        public HttpResponseMessage Get() {
            var response = new HttpResponseMessage();
            response.Content = new StringContent("<html><body>Welcome to Api</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
