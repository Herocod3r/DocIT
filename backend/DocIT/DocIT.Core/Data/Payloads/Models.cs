using System;
using System.ComponentModel.DataAnnotations;

namespace DocIT.Core.Data.Payloads
{
    public class UserAccountPayload
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public class UpdateAccountPayload
    {
        [Required]
        public string Name { get; set; }

    }

    public class GitConfigPayload
    {
        [Required]
        public string AccountName { get; set; }

        [Required]
        public string PersonalToken { get; set; }

        [Required]
        public string Type { get; set; } = "Github";
    }

    public class ProjectPayload
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SwaggerUrl { get; set; }
        public Guid ParentId { get; set; }
        
        
    }

    public class GitTokenPayload
    {
        [Required]
        public string GitRepositoryName { get; set; }
        [Required]
        public string Branch { get; set; }
        [Required]
        public string GitPathToFile { get; set; }
        public Guid GitConfigId { get; set; }
    }

    public class UpdateProjectPayload
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SwaggerUrl { get; set; }
    }

    public class InvitePayload
    {
        public Guid ProjectId { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
    }

    public class DeleteInvitePayload
    {
        public Guid ProjectId { get; set; }
        public string Email { get; set; }
    }



}
