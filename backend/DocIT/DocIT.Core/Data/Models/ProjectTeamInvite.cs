using System;
namespace DocIT.Core.Data.Models
{
    public class ProjectTeamInvite : DbModel<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public DateTime InvitedAt { get; set; }
        public bool Accepted { get; set; }


    }



}
