using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Repositories.PostRepo;
using TranquiloSystem.BLL.AutoMapper;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;

namespace TranquiloSystem.BLL.Manager.PostManager
{
	public class PostManager : IPostManager
	{
		private readonly IPostRepository _postRepository;
		private readonly IMapper _mapper;
		public PostManager(IPostRepository postRepository, IMapper mapper)
		{
			_postRepository = postRepository;
			_mapper = mapper;
		}

		public async Task<ICollection<PostReadDto>> GetAllAsync()
		{
			var Posts = await _postRepository.GetAllAsync();
			return _mapper.Map<ICollection<PostReadDto>>(Posts);
		}
	}
}
