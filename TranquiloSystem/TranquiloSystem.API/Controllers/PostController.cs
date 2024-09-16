using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranquiloSystem.BLL.Manager.PostManager;

namespace TranquiloSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IPostManager _postManager;

		public PostController(IPostManager postManager)
		{
			_postManager = postManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPosts()
		{
			var result = await _postManager.GetAllAsync();
			return Ok(result);
		}
	}
}
