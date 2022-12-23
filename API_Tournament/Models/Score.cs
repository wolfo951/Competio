using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class Score
    {
        public int PkScoreId { get; set; }
        public int? Score1 { get; set; }
        public int FkGameGroupId { get; set; }
        public int? FkPlayerId { get; set; }

        public virtual GameGroup? FkGameGroup { get; set; } = null!;
        public virtual Player? FkPlayer { get; set; }
    }
}
