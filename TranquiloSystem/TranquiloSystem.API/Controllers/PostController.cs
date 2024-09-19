using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
			return Ok(result);
		}

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = _postManager.GetById(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostAddDto postAddDto)
        {
            await _postManager.Add(postAddDto);
            return NoContent();
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update(int Id, PostUpdateDto postUpdateDto)
        {
            if (Id != postUpdateDto.Id)
            {
                return BadRequest();
            }
            await _postManager.Update(postUpdateDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {

            await _postManager.Delete(Id);
            return NoContent();
        }

    }
}
