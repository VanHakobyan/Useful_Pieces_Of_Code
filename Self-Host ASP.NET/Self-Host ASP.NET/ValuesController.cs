using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Web;
using System.Web.Http;

namespace Self_Host_ASP.NET
{
    public class ValuesController : ApiController
    {
       
        [HttpPost]
        public IHttpActionResult Get([FromBody] Model commandRequestApi, [FromUri] string token)
        {
           
            try
            {
                Console.WriteLine(commandRequestApi.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"{e}");
            }
            return Ok();
        }

    }

    public class Model
    {
        public int Id { get; set; }
    }


    //public class CompressFilter : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        HttpRequestBase request = filterContext.HttpContext.Request;

    //        string acceptEncoding = request.Headers["Accept-Encoding"];

    //        if (string.IsNullOrEmpty(acceptEncoding)) return;

    //        acceptEncoding = acceptEncoding.ToUpperInvariant();

    //        HttpResponseBase response = filterContext.HttpContext.Response;

    //        if (acceptEncoding.Contains("GZIP"))
    //        {
    //            response.AppendHeader("Content-encoding", "gzip");
    //            response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
    //        }
    //        else if (acceptEncoding.Contains("DEFLATE"))
    //        {
    //            response.AppendHeader("Content-encoding", "deflate");
    //            response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
    //        }
    //    }

    //}


}
