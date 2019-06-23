﻿using System;
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
        public string PathToFile { get; set; }
        public Guid ParentId { get; set; }
        public Guid GitConfigId { get; set; }
        public string GitRepositoryName { get; set; }
        public string GitPathToFile { get; set; }
    }

    public class UpdateProjectPayload
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public string PathToFile { get; set; }
        public Guid GitConfigId { get; set; }
        public string GitRepositoryName { get; set; }
        public string GitPathToFile { get; set; }
    }

}