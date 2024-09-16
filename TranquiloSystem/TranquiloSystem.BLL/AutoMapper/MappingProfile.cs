using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;

namespace TranquiloSystem.BLL.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<PostReadDto, Post>().ReverseMap();
		}
	}
}
