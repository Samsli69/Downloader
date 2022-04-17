using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projets
{
    internal class Download
    {
        private static readonly HttpClient HttpClient;
        private string path = @"C:\Users\Samsli\Downloads";

        static Download()
        {
            HttpClient = new HttpClient();
        }
        
        //Donwload a file from URL
        public async Task downloadFile(Uri url)
        {
            try
            {
                var stream = await HttpClient.GetStreamAsync(url);
                using (var fileStream = File.Create(Path.Combine(path, GetFileNameFromUrl(url))))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        private string GetFileNameFromUrl(Uri url)
        {
            return Path.GetFileName(url.LocalPath);
        }

        public long GetFileSize(Uri url)
        {

            var result = HttpClient.GetAsync(url.AbsoluteUri, HttpCompletionOption.ResponseHeadersRead).Result;

            if (result.IsSuccessStatusCode)
            {
                return result.Content.Headers.ContentLength.Value;
            }
            else
            {
                return -1;
            }
        }

    }
}
