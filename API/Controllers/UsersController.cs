using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseAPIController
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
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

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO)
        {
            var username = User.GetUsername();

            var user = await _userRepository.GetUserByUsernameAsync(username);
            if(user == null) return NotFound();

            _mapper.Map(memberUpdateDTO, user);

            if(await _userRepository.SaveAllASync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
        {
            var user = await  _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };
            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }
            else
            {
                photo.IsMain = false;
            }

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllASync())
            {
                return CreatedAtAction(nameof(GetUserByUsername), new {username = user.UserName}, _mapper.Map<PhotoDTO>(photo));
            }

            return BadRequest("Problem adding photo");
        } 
    }
}
