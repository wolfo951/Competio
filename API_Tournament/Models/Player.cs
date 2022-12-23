using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class Player
    {
        public Player()
        {
            Scores = new HashSet<Score>();
        }

        public int PkPlayerId { get; set; }
        public int FkUserId { get; set; }
        public int FkTournamentId { get; set; }

        public virtual Tournament? FkTournament { get; set; } = null!;
        public virtual User? FkUser { get; set; } = null!;
        public virtual ICollection<Score> Scores { get; set; }
    }
}
