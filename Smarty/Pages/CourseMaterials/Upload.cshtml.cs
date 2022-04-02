using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using NToastNotify;
using Smarty.AWSS3Helper;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.ViewModels.CourseMaterialViewModel;

namespace Smarty.Pages.test
{
    [Authorize(Roles = nameof(Roles.Instructor))]

    public class UploadModel : PageModel
    {
        private readonly IUnitOfWork _context;
        private readonly IToastNotification _toastr;
        private readonly UserManager<SmartyUser> _userManager;
        private readonly IInstructorService _instructorService;
        private ServiceConfiguration _serviceConfiguration;

        public UploadModel(IUnitOfWork context,
            UserManager<SmartyUser> userManager,
            IInstructorService instructorService,
            IToastNotification toastr,
            IOptions<ServiceConfiguration> serviceConfiguration)
        {
            _toastr = toastr;
            _context = context;
            _userManager = userManager;
            _instructorService = instructorService;
            _serviceConfiguration = serviceConfiguration.Value;
        }

        [BindProperty]
        public UploadViewModel ViewModel { get; set; }
        public SelectList SelectList;

        public async Task OnGetAsync(int? selectedCourseId)
        {
            var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
            SelectList = await _instructorService.GetCoursesSelectListAsync(instructorId, selectedCourseId);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ViewModel.File == null)
                return BadRequest();

            var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
            var course = await _context.Courses.FindByKeyAsync(ViewModel.CourseId);
            if(course == null || course.InstructorId != instructorId)
                return BadRequest();
            
            var accesskey = _serviceConfiguration.AccessKey;
            var secretkey = _serviceConfiguration.SecretKey;
            var bucketName = _serviceConfiguration.BucketName;
            RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;

            var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);

            var fileTransferUtility = new TransferUtility(s3Client);
            await using var newMemoryStream = new MemoryStream();
            await ViewModel.File.CopyToAsync(newMemoryStream);
            var documentName = ViewModel.File.FileName;
            

            try
            {
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    Key = documentName,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = newMemoryStream,
                };
                fileTransferUtility.UploadAsync(fileTransferUtilityRequest).GetAwaiter().GetResult();
                var existedFile = _context.CourseMaterials.FindByCriteria(a => a.name == ViewModel.File.FileName);
                var IsSameFile = from f in existedFile
                                 where f.CourseId == ViewModel.CourseId
                                 select f;

                if (existedFile == null || IsSameFile == null)
                {
                    _context.CourseMaterials.Add(new CourseMaterial { CourseId = ViewModel.CourseId, name = documentName });
                    _context.SaveChanges();
                }
                _context.CourseMaterials.Add(new CourseMaterial { CourseId = ViewModel.CourseId, name = documentName });
                _context.SaveChanges();
                _toastr.AddSuccessToastMessage("File added succeccfully..");

                return RedirectToPage("/courseMaterials/Index");

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }

}

 





































