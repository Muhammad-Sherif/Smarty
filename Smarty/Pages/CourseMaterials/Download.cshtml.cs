using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using NToastNotify;
using Smarty.AWSS3Helper;

namespace Smarty.Pages.test
{
    [Authorize]

    public class DownloadModel : PageModel
    {
        private readonly IToastNotification _toastr;
        private ServiceConfiguration _serviceConfiguration;
        public DownloadModel(IOptions<ServiceConfiguration> serviceConfiguration, IToastNotification toastr)
        {
            _toastr = toastr;
            _serviceConfiguration = serviceConfiguration.Value;
        }

        public async Task<IActionResult> OnGetAsync(string fileName)
        {
            if (fileName == null)
            {
                _toastr.AddErrorToastMessage("Error!!");
                return Page();
            }

            var accesskey = _serviceConfiguration.AccessKey;
            var secretkey = _serviceConfiguration.SecretKey;
            var bucketName = _serviceConfiguration.BucketName;
            RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;

            var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);


            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };
                MemoryStream ms = null;
                using (var response = await s3Client.GetObjectAsync(getObjectRequest))
                {
                    using (ms = new MemoryStream())
                    {
                        await response.ResponseStream.CopyToAsync(ms);
                    }
                }

                return File(ms.ToArray(), "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                _toastr.AddErrorToastMessage("Try again!!");
                return Page();
            }

        }

        public void OnPost()
        {
        }

    }
}
