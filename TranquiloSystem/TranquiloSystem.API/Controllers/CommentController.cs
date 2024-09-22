using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Manager.PostCommentManager;

namespace TranquiloSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class CommentController : ControllerBase
	{
		private readonly IPostCommentManager _postCommentManager;
		public CommentController(IPostCommentManager postCommentManager)
		{
			_postCommentManager = postCommentManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllComment()
		{
			var result = await _postCommentManager.GetAllAsync();
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model,result.StatusCode });
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _postCommentManager.GetByIdAsync(id);
			if (result.IsSucceeded == false)
			{
				return NotFound(new {result.Message, result.StatusCode});
			}
			return Ok(new { result.Model, result.StatusCode });
		}

		[HttpGet("User/{userId}")]
		public async Task<IActionResult> GetByUserId(string userId)
		{
			var result = await _postCommentManager.GetByUserIdAsync(userId);
			if (result.IsSucceeded == false)
			{
				return NotFound(new {result.Message, result.StatusCode});
			}
			return Ok(new {result.Model, result.StatusCode });
		}

		[HttpGet("Post/{postId}")]
		public async Task<IActionResult> GetByPostId(int postId)
		{
			var result = await _postCommentManager.GetByPostIdAsync(postId);
			if (result.StatusCode == 404)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			if (result.StatusCode == 400)
			{
				return BadRequest(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}

		[HttpPost]
		public async Task<IActionResult> AddPost(PostCommentAddDto postCommentAddDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}
			var result = await _postCommentManager.AddAsync(postCommentAddDto);
			if(result.IsSucceeded == false)
			{
				return BadRequest(new { result.Message, result.StatusCode });
			}


			return Ok(new {CommentId = result.Model, result.Message, result.Notifcation, ProfilePicture = result.Object, result.StatusCode});
		}

		[HttpPatch]
		public async Task<IActionResult> UpdatePost(PostCommentUpdateDto postCommentUpdateDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}
			if (postCommentUpdateDto == null || postCommentUpdateDto.Id <= 0)
			{
				return BadRequest(new { message = "Invalid data", statusCode = 400 });
			}
			var result = await _postCommentManager.UpdateAsync(postCommentUpdateDto);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, statusCode = 404 });
			}
			return Ok(new { result.Model, result.Message, statusCode = 200 });
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { ModelState, StatusCode = 400 });
			}

			var result = await _postCommentManager.DeleteAsync(id);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message,result.StatusCode });
			}
			return Ok(new { result.Message,result.StatusCode });
		}
	}
}
