using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

using Tranquilo.DAL.Repositories.PostRepo;
using TranquiloSystem.BLL.AutoMapper;
using TranquiloSystem.BLL.Dtos;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;

namespace TranquiloSystem.BLL.Manager.PostManager
{
	public class PostManager : IPostManager
	{
		private readonly IPostRepository _postRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		public PostManager(IPostRepository postRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
		{
			_postRepository = postRepository;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<GeneralResponseDto> GetAllAsync()
		{
			var posts = await _postRepository.GetAllAsync();
			if (posts == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No Posts Yet",
					StatusCode = 404
				};
			}

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = _mapper.Map<IEnumerable<PostReadDto>>(posts),
				StatusCode = 200
			};
		}

		public async Task<GeneralResponseDto> GetByIdAsync(int id)
		{
			var post = await _postRepository.GetByIdAsync(id);
			if (post == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No post with this id",
					StatusCode = 404
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = _mapper.Map<PostReadDto>(post),
				StatusCode = 200
			};
		}

		public async Task<GeneralResponseDto> GetByUserIdAsync(string userId)
		{
			var userExist = await _userManager.FindByIdAsync(userId);
			if (userExist == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No user with this id",
					StatusCode = 400
				};
			}

			var posts = await _postRepository.GetByUserIdAsync(userId);
			if (posts == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "this user has no posts",
					StatusCode = 404
				};
			}

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = _mapper.Map<IEnumerable<PostReadDto>>(posts),
				StatusCode = 200
			};
		}
		public async Task<GeneralResponseDto> AddAsync(PostAddDto postAddDto)
		{
			var userExist = await _userManager.FindByEmailAsync(postAddDto.UserEmail);
			if (userExist == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No user with this email",
					StatusCode = 400
				};
			}
			if (string.IsNullOrWhiteSpace(postAddDto.PostText))
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Can't add empty post",
					StatusCode = 400 //badReq
				};
			}
			
			var post = _mapper.Map<Post>(postAddDto);
			post.UserId = userExist.Id;

			var postId = await _postRepository.AddAsync(post);
			if (postId == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Failed to add post. Please try again.",
					StatusCode = 500 //server
				};
			}
			var notificatioMessage = $"{userExist.UserName} add a new post";

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = postId,
				Message = "Your post has been added successfully.",
				StatusCode = 201 ,//created
				Notifcation = notificatioMessage,
				Object = userExist.ProfilePicture
			};
		}

		public async Task<GeneralResponseDto> DeleteAsync(int id)
		{
			var post = await _postRepository.GetByIdAsync(id);
			if (post == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No post with this id",
					StatusCode = 404
				};
			}

			var deleted = await _postRepository.DeleteAsync(post);

			if (!deleted)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Failed to delete post. Please try again.",
					StatusCode = 500 //server
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Message = "Post deleted successully.",
				StatusCode = 200
			};
		}

		public async Task<GeneralResponseDto> UpdateAsync(PostUpdateDto postUpdateDto)
		{
			var post = await _postRepository.GetByIdAsync(postUpdateDto.Id);
			if (post == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No post with this id",
					StatusCode = 404
				};
			}

			post.PostText = postUpdateDto.PostText;

			var updated = await _postRepository.UpdateAsync(post);
			if (!updated)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Failed to update post. Please try again.",
					StatusCode = 400 //server
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Message = "Post update successully.",
				Model = _mapper.Map<PostReadDto>(post),
				StatusCode = 200
			};
		}

		public async Task<GeneralResponseDto> VoteAsync(VoteDto voteDto)
		{
			var userExist = await _userManager.FindByIdAsync(voteDto.UserId);
			if (userExist == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "User not found",
					StatusCode = 404
				};
			}
			var post = await _postRepository.GetByIdAsync(voteDto.PostId);
			if(post == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Post not found",
					StatusCode = 404
				};
			}

			if (voteDto.IsUpVote)
			{
				if (!post.UpVoteCount.Contains(voteDto.UserId))
				{
					post.UpVoteCount.Add(voteDto.UserId);
				}
				if (post.DownVoteCount.Contains(voteDto.UserId))
				{
					post.DownVoteCount.Remove(voteDto.UserId);
				}
			}
			else
			{
				if (!post.DownVoteCount.Contains(voteDto.UserId))
				{
					post.DownVoteCount.Add(voteDto.UserId); 
				}

				if (post.UpVoteCount.Contains(voteDto.UserId))
				{
					post.UpVoteCount.Remove(voteDto.UserId);
				}
			}
			await _postRepository.UpdateAsync(post);

			var notificatioMessage = $"{userExist.UserName} voted to your post";

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				StatusCode = 200,
				Model = new { post.UpVoteCountLength, post.DownVoteCountLength },
				Notifcation = notificatioMessage,
				Object = userExist.ProfilePicture
			};
		}
	}
}
