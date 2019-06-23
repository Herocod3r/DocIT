using System;
using System.Collections.Generic;

namespace DocIT.Core.Data.Models
{
    public class InviteItem
    {
        public string ProjectName { get; set; }
        public Guid ProjectId { get; set; }
        public List<Invite> Invites { get; set; }
        public string InviterName { get; set; }
    }
}
