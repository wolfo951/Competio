using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class GameProperty
    {
        public int PkPropertyId { get; set; }
        public int FkGameId { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyValue { get; set; }
        public string? PropertyType { get; set; }

        public virtual Game? FkGame { get; set; } = null!;
    }
}
