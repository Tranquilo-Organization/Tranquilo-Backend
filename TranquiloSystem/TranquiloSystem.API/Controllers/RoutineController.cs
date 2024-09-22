using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using TranquiloSystem.BLL.Dtos.RoutineDto;
using TranquiloSystem.BLL.Manager.RoutineManager;

namespace TranquiloSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class RoutineController : ControllerBase
	{
		private readonly IRoutineManager _routineManager;

		public RoutineController(IRoutineManager routineManager)
		{
			_routineManager = routineManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _routineManager.GetAllAsync();
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			
			return Ok(new {result.Model, result.StatusCode});
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result =await _routineManager.GetByIdAsync(id);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}

			return Ok(new { result.Model, result.StatusCode });
		}

		[HttpGet("LevelId/{LevelId}")]
		public async Task<IActionResult> GetByLevelIdAll(int LevelId)
		{
			var result =await _routineManager.GetByLevelIdAsync(LevelId);
			if (result.IsSucceeded == false)
			{
				return NotFound(new { result.Message, result.StatusCode });
			}
			return Ok(new { result.Model, result.StatusCode });
		}
	}
}
