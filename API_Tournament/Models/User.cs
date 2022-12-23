using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class User
    {
        public User()
        {
            Players = new HashSet<Player>();
            Tournaments = new HashSet<Tournament>();
        }

        public int PkUserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? IsSuspended { get; set; }
        public string Role { get; set; } = null!;

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
