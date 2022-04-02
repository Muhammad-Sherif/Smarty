using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
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

namespace Smarty.Pages.test
{
	[Authorize(Roles = nameof(Roles.Instructor))]
	public class IndexModel : PageModel
    {
		private readonly IUnitOfWork _context;
		private readonly IInstructorService _instructorService;
		private readonly UserManager<SmartyUser> _userManager;
		private readonly ServiceConfiguration _serviceConfiguration; 

		public IndexModel(IUnitOfWork context,
			UserManager<SmartyUser> userManager,
			IInstructorService instructorService,
			IOptions<ServiceConfiguration> serviceConfiguration)
		{
			_context = context;
			_userManager = userManager;
			_instructorService = instructorService;
			_serviceConfiguration = serviceConfiguration.Value;
		}

		public IEnumerable<string> fileNames { get; set; }
		public IEnumerable<string> Keys { get; set; }
		public SelectList SelectList { get; set; }

		public async Task OnGetAsync(int? selectedCourseId)
		{
			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			SelectList = await _instructorService.GetCoursesSelectListAsync(instructorId, selectedCourseId);
		}
		

		public async Task<IActionResult> OnGetMaterialsAsync(int? courseId)
		
		{
			if (courseId == null)
				return BadRequest();

			var instructorId = _userManager.GetUserAsync(User).Result.MemberId;
			var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId && c.InstructorId == instructorId);

			if (course == null)
				return NotFound();

			var accesskey = _serviceConfiguration.AccessKey;
			var secretkey = _serviceConfiguration.SecretKey;
			var bucketName = _serviceConfiguration.BucketName;
			RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;

			var s3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);
			ListVersionsResponse listVersions = await s3Client.ListVersionsAsync(bucketName);
			Keys = listVersions.Versions.Select(k => k.Key).ToList();

			var materials = await _context.CourseMaterials.FindByCriteriaAsync(c => c.CourseId == courseId);
			
			fileNames = (from k in Keys
							 from m in materials
							 where k == m.name
							 select k);

			return new JsonResult(fileNames);

		}
		

    }
}

