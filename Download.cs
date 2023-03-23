using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projets
{
    /*internal class Download
    {
        private static readonly HttpClient HttpClient;
        private readonly string path = @"C:\Users\Samsli\Downloads";

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
                using (var fileStream = File.Create(Path.Combine(path, GetFileNameFromUrl(url)+GetFileExtensionFromUrl(url))))
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

        private string GetFileExtensionFromUrl(Uri url)
        {
            string valRen = url.AbsoluteUri.Split('?')[0];
            valRen = valRen.Split('/').Last();
            return valRen.Contains('.') ? valRen.Substring(valRen.LastIndexOf('.')) : "";
        }

    }*/

    public class HttpClientDownloadWithProgress : IDisposable
    {
        private readonly string _downloadUrl;
        private readonly string _destinationFilePath = @"C:\Users\Samsli\Downloads\";

        private HttpClient _httpClient;

        public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);

        public event ProgressChangedHandler ProgressChanged;

        public HttpClientDownloadWithProgress(Uri downloadUrl)
        {
            _downloadUrl = downloadUrl.AbsoluteUri;
        }

        public async Task StartDownload()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

            using (var response = await _httpClient.GetAsync(_downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                await DownloadFileFromHttpResponseMessage(response);
        }

        private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength;

            using (var contentStream = await response.Content.ReadAsStreamAsync())
                await ProcessContentStream(totalBytes, contentStream);
        }

        private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
        {
            var totalBytesRead = 0L;
            var readCount = 0L;
            var buffer = new byte[8192];
            var isMoreToRead = true;

            using (var fileStream = new FileStream(_destinationFilePath+GetFileNameFromUrl(new(_downloadUrl))+GetFileExtensionFromUrl(new(_downloadUrl)), FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                do
                {
                    var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                        continue;
                    }

                    await fileStream.WriteAsync(buffer, 0, bytesRead);

                    totalBytesRead += bytesRead;
                    readCount += 1;

                    if (readCount % 100 == 0)
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                }
                while (isMoreToRead);
            }
        }

        private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
        {
            if (ProgressChanged == null)
                return;

            double? progressPercentage = null;
            if (totalDownloadSize.HasValue)
                progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);

            ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        private string GetFileNameFromUrl(Uri url)
        {
            return Path.GetFileName(url.LocalPath);
        }

        private string GetFileExtensionFromUrl(Uri url)
        {
            string valRen = url.AbsoluteUri.Split('?')[0];
            valRen = valRen.Split('/').Last();
            return valRen.Contains('.') ? valRen.Substring(valRen.LastIndexOf('.')) : "";
        }

    
    }
}
