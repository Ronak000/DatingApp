using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//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
namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public UsersController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _uow.UserRepository.GetMembersAsync();

            return Ok(users);
        }

        //[Authorize(Roles = "Member")]
        [HttpGet("{username}")]
        //[HttpPost]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _uow.UserRepository.GetMemberAsync(username);
        }


    }
}