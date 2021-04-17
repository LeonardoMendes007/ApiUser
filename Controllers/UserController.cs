using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using ApiUser.Models;
using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;
using AutoMapper;
using ApiUser.Dtos;

namespace ApiUser.Controllers{

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase{

       private readonly IUserService _userService;
       private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper){
           this._userService = userService;
           this._mapper = mapper;
       }

       [HttpGet]
       public ActionResult<IEnumerable<User>> getAllUsers(){
           
           IEnumerable<User> users = _userService.findAll();

           return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));

       }

       [HttpGet("{id}", Name="getUserById")]
       public ActionResult<UserReadDto> getUserById(int id){
           
           User user = _userService.findById(id);

           if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserReadDto>(user);
       }

       [HttpPost]
       public ActionResult<UserReadDto> createUser(UserCreateDto userDto){
           
            User user = _mapper.Map<User>(userDto);
            
            _userService.save(user);
            _userService.saveChanges();

            UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

            return CreatedAtRoute(nameof(getUserById), new {Id = userReadDto.Id}, userReadDto);
       }

       [HttpPut("{id}")]
       public ActionResult createUser(int id, UserUpdateDto userDto){
           
            User userModelFromRepo = _userService.findById(id);

            if(userModelFromRepo == null){
               return NotFound();
            }

            _mapper.Map(userDto, userModelFromRepo);

            _userService.update(userModelFromRepo);

            _userService.saveChanges();

            return NoContent();
       }

       [HttpDelete("id")]
       public ActionResult deleteUser(int id){

           User userModelFromRepo = _userService.findById(id);
           if(userModelFromRepo == null){
               return NotFound();
           }
           _userService.delete(userModelFromRepo);
           _userService.saveChanges();

           return NoContent();

       }
        

    }
}