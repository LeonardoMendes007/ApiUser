
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

        public UserService(ApiUserContext context)
        {

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

        public void save(User user)
        {

            user.CreationDate = DateTime.Now;
            _context.Users.Add(user);

            //retornar id ou usar para redirect
        }

        public void update(User user)
        {

        }


        public void delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Remove(user);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}