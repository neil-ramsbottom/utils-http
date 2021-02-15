using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RamsbottomNeil.Utils.Http
{
    public class FileDownload
    {
        public async Task<string> Download(Uri url)
        {
            var http = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            // we don't use GetTempFileName because of the limit of 65535 files
            var temporaryFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            using (var fs = File.OpenWrite(temporaryFile))
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                await contentStream.CopyToAsync(fs);
            }

            return temporaryFile;
        }
    }
}
