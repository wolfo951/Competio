using System;
using System.Collections.Generic;

namespace API_Tournament.Models
{
    public partial class TournamentRequest
    {
        public int RequestId { get; set; }
        public string RequesterName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime TournamentStart { get; set; }
        public DateTime TournamentEnd { get; set; }
        public int CompanySize { get; set; }
        public string? AdditionalNotes { get; set; }
    }
}
