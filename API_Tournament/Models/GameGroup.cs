using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class GameGroup
    {
        public GameGroup()
        {
            InverseFkGroupParent = new HashSet<GameGroup>();
            Scores = new HashSet<Score>();
        }

        public int PkGroupId { get; set; }
        public int? FkGroupParentId { get; set; }
        public int? NextStagePlayerCount { get; set; }
        public string GroupName { get; set; } = null!;
        public bool? IsFinished { get; set; }
        public int FkGameId { get; set; }

        public virtual Game? FkGame { get; set; } = null!;
        public virtual GameGroup? FkGroupParent { get; set; }
        public virtual ICollection<GameGroup> InverseFkGroupParent { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
