
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiUser.Entities;

namespace ApiUser.Services{

    public interface IUserService{

        IEnumerable<User> FindAll();
        Task<User> FindByIdAsync(int id);
        Task SaveAsync(User user);
        void Delete(User user);
        void Update(User user);
        bool SaveChanges();
        
        
    }
}