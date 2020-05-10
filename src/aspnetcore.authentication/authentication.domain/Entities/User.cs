using System;
using System.Collections.Generic;

namespace authentication.domain.Entities
{
    /// <summary>
    /// Usuário domínio. 
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogin { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
    }
}
