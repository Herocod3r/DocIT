using System;
namespace DocIT.Core.Data.Models
{
    public class ProjectTeamInviteItem : ProjectTeamInvite
    {
        public Guid InvitedUserId { get; set; }
        public string InvitedUserName { get; set; }
        public string InviteeUserName { get; set; }
        public string ProjectName { get; set; }
    }
}
