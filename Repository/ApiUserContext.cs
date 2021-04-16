using ApiUser.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUser.Repository
{

    public class ApiUserContext : DbContext
    {
        public ApiUserContext(DbContextOptions<ApiUserContext> options): base(options){

        }

        public DbSet<User> Users {get; set;}
    }
}