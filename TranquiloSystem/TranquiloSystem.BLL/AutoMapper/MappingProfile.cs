using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.RoutineRepo;
using TranquiloSystem.BLL.Dtos;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;
using TranquiloSystem.BLL.Dtos.ProfileDto;
using TranquiloSystem.BLL.Dtos.RoutineDto;
using TranquiloSystem.DAL.Data.Models;

namespace TranquiloSystem.BLL.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<PostReadDto, Post>().ReverseMap()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
			CreateMap<Post, PostUpdateDto>().ReverseMap();
			CreateMap<PostAddDto, Post>().ReverseMap();

			CreateMap<PostCommentReadDto,PostComment>().ReverseMap()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
			CreateMap<PostCommentAddDto, PostComment>();
			CreateMap<PostCommentUpdateDto, PostComment>();

			

			CreateMap<ProfileReadDto , ApplicationUser>().ReverseMap()
				.ForMember(dest => dest.LevelName, opt => opt.MapFrom(src => src.Level.Name));
			CreateMap<ProfileUpdateDto, ApplicationUser>().ReverseMap();

			CreateMap<Routine, RoutineReadDto>()
				.ForMember(dest => dest.LevelName, opt => opt.MapFrom(src => src.Level.Name))
				.AfterMap((src, dest) =>
				{
					dest.Steps = src.Steps?.Split('\n').Select(s => s.Trim()).ToList();
				})
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
		}
	}
}
