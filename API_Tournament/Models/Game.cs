using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class Game
    {
        public Game()
        {
            GameGroups = new HashSet<GameGroup>();
            GameProperties = new HashSet<GameProperty>();
        }

        public int PkGameId { get; set; }
        public string GameTitle { get; set; } = null!;
        public int? PointsForScore { get; set; }
        public int? PointsForFirst { get; set; }
        public int? PointsForLast { get; set; }
        public bool? IsTemplate { get; set; }
        public int? FkTournamentId { get; set; }
        public int FkGameTypeId { get; set; }

        public virtual GameType? FkGameType { get; set; } = null!;
        public virtual Tournament? FkTournament { get; set; }
        public virtual ICollection<GameGroup> GameGroups { get; set; }
        public virtual ICollection<GameProperty> GameProperties { get; set; }
    }
}
