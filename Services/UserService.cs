
using System;
using System.Collections.Generic;
using System.Linq;
using ApiUser.Models;
using ApiUser.Repository;

namespace ApiUser.Services
{

    public class UserService : IUserService
    {

       private readonly ApiUserContext _context;

       public UserService(ApiUserContext context){

           this._context = context;

       }
        public IEnumerable<User> findAll()
        {
            return _context.Users.ToList();
        }

        public User findById(int id)
        {
            return _context.Users.Find(id);
        }

        public User save(User user)
        {
            throw new System.NotImplementedException();
        }

        public void update(int id, User user)
        {
            throw new System.NotImplementedException();
        }


        public void delete(int id)
        {
            throw new System.NotImplementedException();
        }


    }
}