using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Games = new HashSet<Game>();
            Players = new HashSet<Player>();
        }

        public int PkTournamentId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartsAt { get; set; }
        public string? Address { get; set; }
        public bool? IsPrivate { get; set; }
        public int TournamentReferee { get; set; }

        public virtual User? TournamentRefereeNavigation { get; set; } = null!;
        public virtual ICollection<Game>? Games { get; set; }
        public virtual ICollection<Player>? Players { get; set; }
    }
}
