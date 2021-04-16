
using System.Collections.Generic;
using ApiUser.Models;

namespace ApiUser.Services{

    public interface IUserService{

        IEnumerable<User> findAll();
        User findById(int id);
        User save(User user);
        void delete(int id);    
        void update(int id, User user);
        
        
    }
}