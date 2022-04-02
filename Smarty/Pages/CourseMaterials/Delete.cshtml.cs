using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using NToastNotify;
using Smarty.AWSS3Helper;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;

namespace Smarty.Pages.test
{
    [Authorize(Roles = nameof(Roles.Instructor))]

    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<SmartyUser> _userManager;
        private readonly IToastNotification _toastr;
        private ServiceConfiguration _serviceConfiguration;

        public DeleteModel(IUnitOfWork context,
            UserManager<SmartyUser> userManager,
            IToastNotification toastr,
            IOptions<ServiceConfiguration> serviceConfiguration)
        {

            _context = context;
            _toastr = toastr;
            _userManager = userManager;
            _serviceConfiguration = serviceConfiguration.Value;
        }


        public async Task<IActionResult> OnGetAsync(string fileName)
        {

            if (fileName == null)
                return BadRequest();

            var existedFile = await _context.CourseMaterials.FirstOrDefaultAsync(a => a.name == fileName);
            if(existedFile == null)
                return BadRequest();

            var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
            var course = await _context.Courses.FindByKeyAsync(existedFile.CourseId);
            if(course == null || course.InstructorId != instructorId)
                return BadRequest();
            

            var accesskey = _serviceConfiguration.AccessKey;
            var secretkey = _serviceConfiguration.SecretKey;
            var bucketName = _serviceConfiguration.BucketName;
            RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
            var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);

            ListVersionsResponse listVersions = await s3Client.ListVersionsAsync(bucketName);
            List<string> keys = listVersions.Versions.Select(c => c.Key).ToList();
            var key = keys.FirstOrDefault(k => k == fileName);

            if (key == null)
                return BadRequest();
            

            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName,

                };
                var response = s3Client.DeleteObjectAsync(request).Result;
                _context.CourseMaterials.Delete(existedFile);
                _context.SaveChanges();
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}