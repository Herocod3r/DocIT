using System;
namespace DocIT.Core.Data.Models
{
    public class ProjectTeamInvite
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime InvitedAt { get; set; }
        public bool Accepted { get; set; }


    }



}
