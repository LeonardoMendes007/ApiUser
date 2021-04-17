
using System.Collections.Generic;
using ApiUser.Models;

namespace ApiUser.Services{

    public interface IUserService{

        IEnumerable<User> findAll();
        User findById(int id);
        void save(User user);
        void delete(User user);    
        void update(User user);

        public bool saveChanges();
        
        
    }
}