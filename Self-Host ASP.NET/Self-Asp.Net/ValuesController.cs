using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Self_Asp.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            try
            {
                var s = File.ReadAllText(@"D:\Response.log");
                return Ok(s);
            }
            catch (Exception e)
            {
                return BadRequest($"{e}");
            }

        }

    }


}
