using System.Collections.Generic;
using ApiUser.Models;
using Microsoft.AspNetCore.Mvc;
using ApiUser.Services;

namespace ApiUser.Controllers{

    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase{

       private readonly IUserService _userService;

       public UserController(IUserService userService){
           this._userService = userService;
       }

       [HttpGet]
       public ActionResult<IEnumerable<User>> getAllUsers(){
           
           return Ok(_userService.findAll());

       }

       [HttpGet("{id}")]
       public ActionResult<User> getUserById(int id){
           return Ok(_userService.findById(id));
       }

    }
}