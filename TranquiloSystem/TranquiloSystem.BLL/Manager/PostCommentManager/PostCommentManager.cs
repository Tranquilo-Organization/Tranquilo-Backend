using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.AccountRepo;
using Tranquilo.DAL.Repositories.CommentRepo;
using Tranquilo.DAL.Repositories.PostRepo;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;

namespace TranquiloSystem.BLL.Manager.PostCommentManager
{
	public class PostCommentManager : IPostCommentManager
	{
		private readonly IPostCommentRepository _postCommentRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPostRepository _postRepository;

		public PostCommentManager(IPostCommentRepository postCommentRepository, IMapper mapper, UserManager<ApplicationUser> userManager = null, IPostRepository postRepository = null)
		{
			_postCommentRepository = postCommentRepository;
			_mapper = mapper;
			_userManager = userManager;
			_postRepository = postRepository;
		}

		//return null if no comments yet //done
		public async Task<GeneralResponseDto> GetAllAsync()
		{
			var Comments = await _postCommentRepository.GetAllAsync();
			if (Comments == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No comments Yet",
					StatusCode = 404
				};
			}

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = _mapper.Map<IEnumerable<PostCommentReadDto>>(Comments),
				StatusCode = 200
			};
		}

		//return null if no comment with this id //done
		public async Task<GeneralResponseDto> GetByIdAsync(int id)
		{
			var Comment = await _postCommentRepository.GetByIdAsync(id);
			if (Comment == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No comment with this id",
					StatusCode = 404
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = _mapper.Map<PostCommentReadDto>(Comment),
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

			var Comments = await _postCommentRepository.GetByUserIdAsync(userId);
			if (Comments == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "this user has no comments",
					StatusCode = 404
				};
			}

			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = _mapper.Map<IEnumerable<PostCommentReadDto>>(Comments),
				StatusCode = 200
			};
		}
		public async Task<GeneralResponseDto> GetByPostIdAsync(int postId)
		{
			var postExists = await _postRepository.GetByIdAsync(postId);
			if (postExists == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No post with this id",
					StatusCode = 400
				};
			}
			var Comments = await _postCommentRepository.GetByPostIdAsync(postId);
			if(Comments == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No Comment in this post",
					StatusCode = 404
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = _mapper.Map<IEnumerable<PostCommentReadDto>>(Comments),
				StatusCode = 200
			};
		}

		public async Task<GeneralResponseDto> AddAsync(PostCommentAddDto postCommentAddDto)
		{
			var user = await _userManager.FindByEmailAsync(postCommentAddDto.UserEmail);
            if (user == null)
            {
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No user with this email",
					StatusCode = 400
				};
			}
            var postExists = await _postRepository.GetByIdAsync(postCommentAddDto.PostID);
			if (postExists == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No post with this id",
					StatusCode = 400
				};
			}
			if (string.IsNullOrWhiteSpace(postCommentAddDto.CommentText))
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Can't add empty comment",
					StatusCode = 400 //badReq
				};
			}
			var Comment = _mapper.Map<PostComment>(postCommentAddDto);
			Comment.UserId = user.Id;

			var commentId = await _postCommentRepository.AddAsync(Comment);
			if(commentId == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Failed to add comment. Please try again.",
					StatusCode = 500 //server
				};
			}
			
			var notificatioMessage = $"{user.UserName} commented on your post";
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Model = commentId,
				Message = "Your comment has been added successfully.",
				StatusCode = 201, //ok
				Notifcation = notificatioMessage,
				Object = user.ProfilePicture
			};
		}
		public async Task<GeneralResponseDto> DeleteAsync(int id)
		 {
			var Comment = await _postCommentRepository.GetByIdAsync(id);
			if (Comment == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No comment with this id",
					StatusCode = 404
				};
			}
			
			var deleted = await _postCommentRepository.DeleteAsync(Comment);

			if (!deleted)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Failed to delete comment. Please try again.",
					StatusCode = 500 //server
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Message = "Comment deleted successully.",
				StatusCode = 200 
			};
		 }
		public async Task<GeneralResponseDto> UpdateAsync(PostCommentUpdateDto postCommentUpdateDto)
		{
			var Comment = await _postCommentRepository.GetByIdAsync(postCommentUpdateDto.Id);
			if (Comment == null)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "No comment with this id",
					StatusCode = 404
				};
			}
			
			Comment.CommentText = postCommentUpdateDto.CommentText;

			var updated = await _postCommentRepository.UpdateAsync(Comment);
			if (!updated)
			{
				return new GeneralResponseDto
				{
					IsSucceeded = false,
					Message = "Failed to update comment. Please try again.",
					StatusCode = 500 //server
				};
			}
			return new GeneralResponseDto
			{
				IsSucceeded = true,
				Message = "Comment update successully.",
				Model = _mapper.Map<PostCommentReadDto>(Comment),
				StatusCode = 200
			};
		}
	}
}
