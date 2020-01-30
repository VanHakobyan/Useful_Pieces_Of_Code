using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Host_ASP.NET
{
    public static class Extension
    {
        public static MemoryStream ToGZip(this string content)
        {
            var byteArray = Encoding.UTF8.GetBytes(content);

            using (MemoryStream outStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(outStream, CompressionMode.Compress))
                using (MemoryStream srcStream = new MemoryStream(byteArray))
                    srcStream.CopyTo(gzipStream);
                return new MemoryStream(outStream.ToArray());
            }
        }
    }
}
