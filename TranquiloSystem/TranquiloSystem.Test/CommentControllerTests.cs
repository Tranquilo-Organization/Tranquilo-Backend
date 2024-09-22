using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.API.Controllers;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Manager.PostCommentManager;

namespace TranquiloSystem.Test
{
	public class CommentControllerTests
	{
		private readonly Mock<IPostCommentManager> _mockPostCommentManager;
		private readonly CommentController _controller;


		public CommentControllerTests()
		{
			_mockPostCommentManager = new Mock<IPostCommentManager>();
			_controller = new CommentController(_mockPostCommentManager.Object);
		}

		
	}
}