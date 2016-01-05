using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace versusBackend.Controllers.api
{
    public class MapsApiController : ApiController
    {
        [HttpGet]
        [Route("api/maps")]
        public IHttpActionResult GetRoute()
        {
            Double lat1 = 21.877744, long1 = -102.298450, lat2 = 21.872567, long2 = -102.293837;
            string route = "http://maps.googleapis.com/maps/api/directions/json?origin="
                + lat1 + "," + long1 + "&destination="
                + lat2 + "," + long2;

            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(route);

            var httpResponse = (HttpWebResponse)request.GetResponse();
            /*var results = JsonConvert.DeserializeObject<dynamic>(new StreamReader(httpResponse.GetResponseStream()));
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
            }*/

            return Ok();
        }
    }
}
