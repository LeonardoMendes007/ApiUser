using System;
using System.ComponentModel.DataAnnotations;

namespace ApiUser.Dtos
{

    public class UserCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
    }
}