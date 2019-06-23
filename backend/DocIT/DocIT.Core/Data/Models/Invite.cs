using System;
namespace DocIT.Core.Data.Models
{
    public class Invite
    {
        public string Email { get; set; }
        public DateTime InvitedAt { get; set; }
        public bool Accepted { get; set; }
    }
}
