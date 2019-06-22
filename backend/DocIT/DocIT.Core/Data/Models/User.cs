using System;
namespace DocIT.Core.Data.Models
{
    public class User : DbModel<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
