using System;
using System.Collections.Generic;

namespace DocIT.Core.Data.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SwaggerUrl { get; set; }
        public List<string> PreviewLinks { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
