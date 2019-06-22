using System;
namespace DocIT.Core.Data.Models
{
    public class ProjectTeamInviteItem 
    {
        public Project Project { get; set; }
        public User Inviter { get; set; }
        public User Invited { get; set; }
        public ProjectTeamInvite Invite { get; set; }
    }
}
