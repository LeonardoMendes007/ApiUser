
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiUser.Entities;
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
        public IEnumerable<User> FindAll()  
        {
            return _context.Users.ToList();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task SaveAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.CreationDate = DateTime.Now;
            await _context.Users.AddAsync(user);
            return;
        }

        public void Update(User user)
        {
            // Nothing
        }


        public void Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Remove(user);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}