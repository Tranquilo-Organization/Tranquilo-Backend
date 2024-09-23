using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TranquiloSystem.BLL.Dtos.ProfileDto;
using TranquiloSystem.BLL.Manager.ProfileManager;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class ProfileController : ControllerBase
	{
		private readonly IProfileManager _profileManager;

		public ProfileController(IProfileManager profileManager)
		{
			_profileManager = profileManager;
		}

		[HttpGet("UserId/{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _profileManager.GetUsersByIdAsync(id);
			if (!result.IsSucceeded)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}

		[HttpGet("UserEmail/{email}")]
		public async Task<IActionResult> GetByEmail(string email)
		{

			var result = await _profileManager.GetByEmailAsync(email);
			if (!result.IsSucceeded)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] ProfileUpdateDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _profileManager.UpdateAsync(dto);
			if (!result.IsSucceeded)
			{
				return BadRequest(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}

		[HttpPatch("UserLevel/{userId}")]
		public async Task<IActionResult> UpdateUserLevel(string userId, int levelId)
		{
			var result = await _profileManager.UpdateUserLevelAsync(userId, levelId);
			if (!result.IsSucceeded)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}
		[HttpDelete("Email/{email}")]
		public async Task<IActionResult> Delete([FromRoute] string email)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await _profileManager.DeleteAsync(email);
			if (!result.IsSucceeded)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Message, result.StatusCode });
		}

	}
}
