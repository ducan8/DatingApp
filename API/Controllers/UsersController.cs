using API.Data;
using API.DTOs;
using API.Entities;
using API.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseAPIController
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetAllUser()
        {
            //var users = await _userRepository.GetUsersASync();
            //var usersToReturn = _mapper.Map<IEnumerable<MemberDTO>>(users);
            //return Ok(usersToReturn);
            return Ok(await _userRepository.GetMembersAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserApp>> GetUser(int id)
        {
            return Ok(await _userRepository.GetUserById(id));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUserByUsername(string username)
        {
            //var user = await _userRepository.GetUserByUsernameAsync(username);
            //return Ok(_mapper.Map<MemberDTO>(user));
            return Ok(await _userRepository.GetMemberAsync(username));
        }
    }
}
