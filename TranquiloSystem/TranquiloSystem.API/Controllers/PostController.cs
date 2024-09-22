using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TranquiloSystem.BLL.Dtos;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Dtos.PostDto;
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
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _postManager.GetByIdAsync(id);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}

		[HttpGet("User{userId}")]
		public async Task<IActionResult> GetByUserId(string userId)
		{
			var result = await _postManager.GetByUserIdAsync(userId);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}
		

		[HttpPost]
		public async Task<IActionResult> AddPost(PostAddDto postAddDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}
			var result = await _postManager.AddAsync(postAddDto);
			if (result.IsSucceeded == false)
			{
				return BadRequest(new { result.Message, result.StatusCode });
			}
			return Ok(new { PostId = result.Model, result.Message, result.Notifcation, ProfilePicture = result.Object,  result.StatusCode });
		}

		[HttpPatch]
		public async Task<IActionResult> UpdatePost(PostUpdateDto postUpdateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}
			if (postUpdateDto == null || postUpdateDto.Id <= 0)
			{
				return BadRequest(new { message = "Invalid data", statusCode = 400 });
			}

			var result = await _postManager.UpdateAsync(postUpdateDto);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.Message, result.StatusCode });
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}

			var result = await _postManager.DeleteAsync(id);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Message, result.StatusCode });
		}

		[HttpPost("vote/")]
		public async Task<IActionResult> Vote([FromBody] VoteDto voteDto)
		{
			var result =  await _postManager.VoteAsync(voteDto);
			if (!result.IsSucceeded)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.Notifcation, ProfilePicture = result.Object, result.StatusCode });

		}

	}
}
