using System;

namespace ApiUser.Dtos
{

    public class UserReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
    }
}