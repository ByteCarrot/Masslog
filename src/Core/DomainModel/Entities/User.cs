using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class User : Entity
    {
        public User()
        {
            Privileges = new List<Privileges>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAdministrator { get; set; }

        public List<Privileges> Privileges { get; set; }
    }
}