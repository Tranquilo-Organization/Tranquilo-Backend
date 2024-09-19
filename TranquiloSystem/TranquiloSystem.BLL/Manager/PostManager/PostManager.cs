using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.PostRepo;
using TranquiloSystem.BLL.AutoMapper;
using TranquiloSystem.BLL.Dtos.PostDto;
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
        public PostReadDto GetById(int id)
        {
            return _mapper.Map<PostReadDto>(_postRepository.GetByIdAsync(id));
        }

        public async Task Add(PostAddDto postAddDto)
        {
            await _postRepository.AddAsync(_mapper.Map<Post>(postAddDto));
            await _postRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var postModel = await _postRepository.GetByIdAsync(id);
            await _postRepository.DeleteAsync(postModel);
            await _postRepository.SaveChangesAsync();
        }

        public async Task Update(PostUpdateDto postUpdateDto)
        {

            await _postRepository.UpdateAsync(_mapper.Map<PostUpdateDto, Post>(postUpdateDto, await _postRepository.GetByIdAsync(postUpdateDto.Id)));
            await _postRepository.SaveChangesAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _postRepository.SaveChangesAsync();
        }
    }
}
