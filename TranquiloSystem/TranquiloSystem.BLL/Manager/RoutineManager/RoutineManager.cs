using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.RoutineRepo;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.RoutineDto;

namespace TranquiloSystem.BLL.Manager.RoutineManager
{
	public class RoutineManager : IRoutineManager
	{
		private readonly IRoutineRepository _routineRepository;
		private readonly IMapper _mapper;

		public RoutineManager(IRoutineRepository routineRepository, IMapper mapper)
		{
			_routineRepository = routineRepository;
			_mapper = mapper;
		}

		public async Task<GeneralResponseDto> GetAllAsync()
		{
			var routinesModel = await _routineRepository.GetAllAsync();
			//var routineDtos = _mapper.Map<IEnumerable<RoutineReadDto>>(routinesModel);
			if (routinesModel == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No routines",
					StatusCode = 404
				};
			}
			var routineDtos = routinesModel.Select(r => new RoutineReadDto
			{
				Id = r.Id,
				Name = r.Name,
				Description = r.Description,
				LevelName = r.Level.Name,
				Steps = r.Steps?.Split('\n').Select(s => s.Trim()).ToList(),
				Type = r.Type,
			}).ToList();

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = routineDtos,
				StatusCode = 200
			};
		}


		public async Task<GeneralResponseDto> GetByIdAsync(int id)
		{

			var routineModel = await _routineRepository.GetByIdAsync(id);
			if(routineModel == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No routine with this id",
					StatusCode = 404
				};
			}
			var routineDto = new RoutineReadDto
			{
				Id = routineModel.Id,
				Name = routineModel.Name,
				Description = routineModel.Description,
				LevelName = routineModel.Level.Name,
				Steps = routineModel.Steps?.Split('\n').Select(s => s.Trim()).ToList(),
				Type = routineModel.Type,
			};
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = routineDto,
				StatusCode = 200
			};
		}

		public async Task<GeneralResponseDto> GetByLevelIdAsync(int levelId)
		{

			var routinesModel = await _routineRepository.GetByLevelIdAsync(levelId);
			if (routinesModel == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No Level with this id",
					StatusCode = 404
				};
			}
			var routinesDto = routinesModel.Select(r => new RoutineReadDto
			{
				Id = r.Id,
				Name = r.Name,
				Description = r.Description,
				LevelName = r.Level.Name,
				Steps = r.Steps?.Split('\n').Select(s => s.Trim()).ToList(),
				Type = r.Type,
			}).ToList();

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = routinesDto,
				StatusCode = 200
			};
		}
	}
}
