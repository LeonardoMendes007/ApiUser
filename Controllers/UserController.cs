using System.Collections.Generic;
using ApiUser.Entities;
using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;
using AutoMapper;
using ApiUser.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiUser.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                IEnumerable<User> users = _userService.FindAll();
                _logger.LogInformation($"GET:{Request.Path} - 200 OK at {DateTime.Now}");

                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GET:{Request.Path} - 500 InternalServerError at {DateTime.Now}");
                return this.InternalServerError(ex.Message, Request);
            }
        }

        [HttpGet("{id}", Name = "getUserById")]
        public async Task<ActionResult<UserReadDto>> GetUserById(int id)
        {
            try
            {
                User user = await _userService.FindByIdAsync(id);

                if (user == null)
                {
                    _logger.LogWarning($"GET:{Request.Path.Value} - 404 NotFound at {DateTime.Now}");
                    return this.NotFound($"There is no resource with id = {id}", Request);
                }
                _logger.LogInformation($"GET:{Request.Path} - 200 OK at {DateTime.Now}");
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"GET:{Request.Path} - 500 InternalServerError at {DateTime.Now}");
                return this.InternalServerError(ex.Message, Request);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser([FromBody] UserCreateDto userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);

                if (userDto == null)
                {
                    _logger.LogWarning($"POST:{Request.Path} user is null - 400 BadRequest at {DateTime.Now}");
                    return this.BadRequest($"transaction not successful, check the data and try again.", Request);
                }

                await _userService.SaveAsync(user);
                _userService.SaveChanges();

                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

                _logger.LogInformation($"POST:{Request.Path} (nome={user.Name};surname={user.Surname};age={user.Age}) - 201 Created at {DateTime.Now}");

                return CreatedAtRoute(nameof(GetUserById), new { Id = user.Id }, userReadDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"POST:{Request.Path} - 500 InternalServerError at {DateTime.Now}");
                return this.InternalServerError(ex.Message, Request);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            try {

                if (userUpdateDto == null)
                {
                    _logger.LogWarning($"PUT:{Request.Path}(nome={userUpdateDto.Name};surname={userUpdateDto.Surname};age={userUpdateDto.Age}) - 404 NotFound at {DateTime.Now}");
                    return this.NotFound($"Resource with id = {id}", Request);
                }

                User user = await _userService.FindByIdAsync(id);

                _mapper.Map(userUpdateDto, user);

                _userService.Update(user);

                _userService.SaveChanges();

                _logger.LogInformation($"PUT:{Request.Path} (nome={user.Name};surname={user.Surname};age={user.Age}) - 200 OK at {DateTime.Now}");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"PUT:{Request.Path} - 500 InternalServerError at {DateTime.Now}");
                return this.InternalServerError(ex.Message, Request);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                User user = await _userService.FindByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"DELETE:{Request.Path} - 404 NotFound at {DateTime.Now}");
                    return this.NotFound($"Resource with id = {id}", Request);
                }
                _userService.Delete(user);
                _userService.SaveChanges();

                _logger.LogWarning($"DELETE:{Request.Path} - 204 NoContent at {DateTime.Now}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DELETE:{Request.Path} - 500 InternalServerError at {DateTime.Now} - Exception {ex.Message}");
                return this.InternalServerError(ex.Message, Request);
            }
        }
    }
}