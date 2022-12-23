using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class GameType
    {
        public GameType()
        {
            Games = new HashSet<Game>();
        }

        public int PkGameTypeId { get; set; }
        public string GameTypeName { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
    }
}
