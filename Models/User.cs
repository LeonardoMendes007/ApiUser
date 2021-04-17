using System;
using System.ComponentModel.DataAnnotations;

namespace ApiUser.Models
{

    public class User
    {
        public User()
        {
        }
        public User(int id, string name, string surname, int age, DateTime creationDate)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Age = age;
            this.CreationDate = creationDate;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
        public DateTime CreationDate { get; set; } 
    }
}