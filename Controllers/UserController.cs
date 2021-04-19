using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using ApiUser.Models;
using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;
using AutoMapper;
using ApiUser.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using ApiUser.Exceptions;

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
        public ActionResult<IEnumerable<User>> getAllUsers()
        {

            IEnumerable<User> users = _userService.findAll();
            _logger.LogInformation($"GET:{Request.Path} - 200 OK at {DateTime.Now}");        

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));

        }

        [HttpGet("{id}", Name = "getUserById")]
        public ActionResult<UserReadDto> getUserById(int id)
        {
            User user = _userService.findById(id);

            if (user == null)
            {
                _logger.LogWarning($"GET:{Request.Path} - 404 NotFound at {DateTime.Now}");
                return NotFound(CustomErrors.NotFound($"There is no resource with id = {id}", Request));
            }
            _logger.LogInformation($"GET:{Request.Path} - 200 OK at {DateTime.Now}");
            return Ok(_mapper.Map<UserReadDto>(user));

        }

        [HttpPost]
        public ActionResult<UserReadDto> createUser(UserCreateDto userDto)
        {

            User user = _mapper.Map<User>(userDto);

            if (userDto == null)
            {
                _logger.LogWarning($"POST:{Request.Path} (nome={user.Name};surname={user.Surname};age={user.Age}) - 400 BadRequest at {DateTime.Now}");
                return BadRequest(CustomErrors.BadRequest($"transaction not successful", Request));
            }

            _userService.save(user);
            _userService.saveChanges();

            UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

            _logger.LogInformation($"POST:{Request.Path} (nome={user.Name};surname={user.Surname};age={user.Age}) - 201 Created at {DateTime.Now}");

            return CreatedAtRoute(nameof(getUserById), new { Id = userReadDto.Id }, userReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult updateUser(int id, UserUpdateDto userDto)
        {

            User userModelFromRepo = _userService.findById(id);

            if (userModelFromRepo == null)
            {
                _logger.LogWarning($"PUT:{Request.Path}(nome={userDto.Name};surname={userDto.Surname};age={userDto.Age}) - 404 NotFound at {DateTime.Now}");
                return NotFound(CustomErrors.NotFound($"Resource with id = {id}", Request));
            }

            _mapper.Map(userDto, userModelFromRepo);

            _userService.update(userModelFromRepo);

            _userService.saveChanges();

            _logger.LogInformation($"PUT:{Request.Path} (nome={userDto.Name};surname={userDto.Surname};age={userDto.Age}) - 200 OK at {DateTime.Now}");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult deleteUser(int id)
        {

            User userModelFromRepo = _userService.findById(id);
            if (userModelFromRepo == null)
            {
                _logger.LogWarning($"DELETE:{Request.Path} - 404 NotFound at {DateTime.Now}");
                return NotFound(CustomErrors.NotFound($"Resource with id = {id}", Request));
            }
            _userService.delete(userModelFromRepo);
            _userService.saveChanges();

            _logger.LogWarning($"DELETE:{Request.Path} - 204 NoContent at {DateTime.Now}");

            return NoContent();

        }


    }
}